using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace SystemEx.Windows.Forms
{
    public class ExplorerBarButtonCollection : CollectionBase
    {
        private ExplorerBar _owner;

        internal ExplorerBarButtonCollection(ExplorerBar owner)
        {
            _owner = owner;
        }

        public ExplorerBarButton this[int key]
        {
            get { return (ExplorerBarButton)this.List[key]; }
        }

        public ExplorerBarButton this[string key]
        {
            get
            {
                foreach (ExplorerBarButton button in this.List)
                {
                    if (button.Text == key)
                    {
                        return button;
                    }
                }

                return null;
            }
        }

        public ExplorerBarButton GetItemAtPoint(Point point)
        {
            foreach (ExplorerBarButton button in this.List)
            {
                if (button.Bounds != Rectangle.Empty && button.Bounds.Contains(point))
                {
                    return button;
                }
            }

            return null;
        }

        public ExplorerBarButton Add(ExplorerBarButton item)
        {
            item.Owner = _owner;

            int index = this.List.Add(item);

            return this[index];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public void AddRange(ExplorerBarButtonCollection items)
        {
            foreach (ExplorerBarButton item in items)
            {
                Add(item);
            }
        }

        public int IndexOf(ExplorerBarButton item)
        {
            return this.List.IndexOf(item);
        }

        public void Insert(int index, ExplorerBarButton item)
        {
            this.List.Insert(index, item);
        }

        public void Remove(ExplorerBarButton item)
        {
            this.List.Remove(item);
        }

        public bool Contains(ExplorerBarButton item)
        {
            return this.List.Contains(item);
        }

        protected override void OnValidate(object value)
        {
            if (!typeof(ExplorerBarButton).IsAssignableFrom(value.GetType()))
            {
                throw new ArgumentException("value must be of type ExplorerBarButton", "value");
            }

            base.OnValidate(value);
        }

        public int CountVisible()
        {
            int count = 0;

            foreach (ExplorerBarButton button in this.List)
            {
                if (button.Visible)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
