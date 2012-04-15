using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SystemEx.Windows.Forms
{
    public class FilesDropData
    {
        private IDataObject _dataObject;
        private OleDropData _oleDropData = null;
        private bool _parsed = false;
        private MemoryStream[] _streams = null;
        private bool _isValid;
        private string[] _filenames;

        public FilesDropData(IDataObject dataObject)
        {
            _dataObject = dataObject;

            var formats = _dataObject.GetFormats();

            _isValid = false;

            foreach (var format in formats)
            {
                switch (format)
                {
                    case "FileDrop":
                    case "FileGroupDescriptor":
                    case "FileGroupDescriptorW":
                        _isValid = true;
                        break;
                }

                if (_isValid)
                    break;
            }

            _parsed = !_isValid;
        }

        public bool HasFiles
        {
            get { return _isValid; }
        }

        public string[] Filenames
        {
            get
            {
                Parse();

                return _filenames;
            }
        }

        public byte[] GetFileData(int index)
        {
            Parse();

            if (_oleDropData != null)
            {
                InitialiseStreams();

                byte[] result = new byte[_streams[index].Length];

                _streams[index].Read(result, 0, result.Length);

                return result;
            }
            else
            {
                return File.ReadAllBytes(_filenames[index]);
            }
        }

        public int GetFileSize(int index)
        {
            Parse();

            if (_oleDropData != null)
            {
                InitialiseStreams();

                return (int)_streams[index].Length;
            }
            else
            {
                return (int)new FileInfo(_filenames[index]).Length;
            }
        }

        private void InitialiseStreams()
        {
            if (_oleDropData != null && _streams == null)
                _streams = (MemoryStream[])_oleDropData.GetData("FileContents");
        }

        private void Parse()
        {
            if (!_isValid)
                throw new Exception("DropData does not contain any files");

            if (!_parsed)
            {
                if (!_dataObject.GetDataPresent("FileDrop"))
                {
                    _oleDropData = new OleDropData(_dataObject);

                    if (_dataObject.GetDataPresent("FileGroupDescriptorW"))
                        _filenames = (string[])_oleDropData.GetData("FileGroupDescriptorW");
                    else
                        _filenames = (string[])_oleDropData.GetData("FileGroupDescriptor");
                }
                else
                {
                    _filenames = (string[])_dataObject.GetData("FileDrop");
                }

                _parsed = true;
            }
        }
    }
}
