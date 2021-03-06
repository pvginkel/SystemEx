﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public static class ResourceManagerUtil
    {
        public static Image GetScaledImage(this ResourceManager self, string name, int size)
        {
            if (DpiScaling.IsDpiScaled)
                size = DpiScaling.Scale(size);

            var resourceSet = self.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var sizes = new List<int>();
            bool haveExact = false;

            foreach (DictionaryEntry entry in resourceSet)
            {
                var key = (string)entry.Key;

                if (key.StartsWith(name + "_", StringComparison.OrdinalIgnoreCase))
                {
                    string sizeString = key.Substring(name.Length + 1);
                    if (int.TryParse(sizeString, out int sizeValue))
                        sizes.Add(sizeValue);
                }
                else if (String.Equals(key, name, StringComparison.OrdinalIgnoreCase))
                {
                    haveExact = true;
                }
            }

            int? matchedSize = MatchSize(sizes, size);

            if (!matchedSize.HasValue)
            {
                if (haveExact)
                    return (Bitmap)resourceSet.GetObject(name);

                throw new ArgumentException("Could not find resource");
            }

            var bitmap = (Bitmap)resourceSet.GetObject(name + "_" + matchedSize.Value);

            bitmap.SetResolution(96, 96);

            var result = DpiScaling.Scale(bitmap, new Size(size, size));

            result.Tag = "__DPI__SCALED__";

            return result;
        }

        private static int? MatchSize(List<int> sizes, int size)
        {
            int? maxSize = null;
            int? minSize = null;

            foreach (int value in sizes)
            {
                if (value >= size)
                {
                    if (!maxSize.HasValue || maxSize.Value > value)
                        maxSize = value;
                }
                else
                {
                    if (!minSize.HasValue || minSize.Value < value)
                        minSize = value;
                }
            }

            return maxSize ?? minSize;
        }

        public static ImageList CreateImageList(this ResourceManager self, Size size, params string[] names)
        {
            return CreateImageList(self, null, size, names);
        }

        public static ImageList CreateImageList(this ResourceManager self, IContainer components, Size size, params string[] names)
        {
            var imageList = new ImageList
            {
                ImageSize = size,
                TransparentColor = Color.Transparent,
                ColorDepth = ColorDepth.Depth32Bit
            };

            components?.Add(imageList);

            self.FillImageList(imageList, names);

            return imageList;
        }

        public static void FillImageList(this ResourceManager self, ImageList imageList, params string[] names)
        {
            int size = imageList.ImageSize.Width;

            imageList.ImageSize = DpiScaling.Scale(imageList.ImageSize);

            foreach (string name in names)
            {
                imageList.Images.Add(self.GetScaledImage(name, size));
            }
        }
    }
}
