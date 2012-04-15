using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SystemEx.Windows.Forms
{
    public class OleDataObjectItem : IDisposable
    {
        private bool _disposed = false;
        private MemoryStream _memoryStream = null;
        private GetMemoryStreamCallback _getMemoryStream = null;

        public OleDataObjectItem(string fileName, DateTime writeTime, long fileSize, GetMemoryStreamCallback getMemoryStream)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");
            if (getMemoryStream == null)
                throw new ArgumentNullException("getMemoryStream");

            FileName = fileName;
            WriteTime = writeTime;
            FileSize = fileSize;

            _getMemoryStream = getMemoryStream;
        }

        public String FileName { get; private set; }
        public DateTime WriteTime { get; private set; }
        public Int64 FileSize { get; private set; }

        public MemoryStream MemoryStream
        {
            get
            {
                if (_memoryStream == null)
                {
                    // Write at least one byte to prevent stoppage.

                    if (FileSize == 0)
                    {
                        _memoryStream = new MemoryStream();

                        _memoryStream.WriteByte(0);
                    }
                    else
                    {
                        _memoryStream = _getMemoryStream();

                        if (_memoryStream == null)
                            throw new Exception("GetMemoryStream returned null");
                        if (_memoryStream.Length != FileSize)
                            throw new Exception("Size of memory stream is not the same as the provided fileSize");
                    }
                }

                return _memoryStream;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                if (_memoryStream != null)
                {
                    _memoryStream.Dispose();
                    _memoryStream = null;
                }
            }
        }
    }

    public delegate MemoryStream GetMemoryStreamCallback();
}
