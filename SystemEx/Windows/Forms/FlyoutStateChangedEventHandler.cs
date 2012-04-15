using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx.Windows.Forms
{
    public class FlyoutStateChangedEventArgs : EventArgs
    {
        public FlyoutStateChangedEventArgs(FlyoutState previousState, FlyoutState state)
        {
            PreviousState = previousState;
            State = state;
        }

        public FlyoutState PreviousState { get; private set; }
        public FlyoutState State { get; private set; }
    }

    public delegate void FlyoutStateChangedEventHandler(object sender, FlyoutStateChangedEventArgs e);
}
