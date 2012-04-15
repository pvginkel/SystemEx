using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    public sealed class Finalizer : IDisposable
    {
        private FinalizerCallback _action;
        private bool _disposed;

        public Finalizer(FinalizerCallback action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            _action = action;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_action != null)
                {
                    // Reset _action to null to prevent duplicate calls
                    // to the finalizer.

                    var action = _action;

                    _action = null;

                    action();
                }

                _disposed = true;
            }
        }
    }

    public delegate void FinalizerCallback();
}
