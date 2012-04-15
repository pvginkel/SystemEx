using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class FlyoutForm : Form
    {
        private const int AnimateSteps = 20;
        private const int PendingDuration = 200;
        private const int WaitingDuration = 200;
        private const double OpaqueOpacity = 0.999; // Setting Opacity to 1.0 doesn't work

        private FlyoutState _state;
        private Timer _timer;
        private bool _disposed;
        private bool _changingState;
        private bool _designMode;
        private bool _mouseDown;

        public FlyoutForm()
        {
            _designMode = ControlUtil.GetIsInDesignMode(this);

            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            ControlBox = false;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            FadeDuration = 500;
            Animate = true;
            DelayHideOnMouseOver = true;
            DismissOnDeactivate = true;

            _timer = new Timer();
            _timer.Tick += new EventHandler(_timer_Tick);
        }

        public event CancelEventHandler EndShowing;

        protected virtual void OnEndShowing(CancelEventArgs e)
        {
            var ev = EndShowing;

            if (ev != null)
                ev(this, e);
        }

        public event FlyoutStateChangedEventHandler StateChanged;

        protected virtual void OnStateChanged(FlyoutStateChangedEventArgs e)
        {
            var ev = StateChanged;

            if (ev != null)
                ev(this, e);
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                if (_designMode)
                    base.Text = value;
            }
        }

        [DefaultValue(FormBorderStyle.SizableToolWindow)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = value; }
        }

        [DefaultValue(false)]
        public new bool ControlBox
        {
            get { return base.ControlBox; }
            set { base.ControlBox = value; }
        }

        [DefaultValue(FormStartPosition.Manual)]
        public new FormStartPosition StartPosition
        {
            get { return base.StartPosition; }
            set { base.StartPosition = value; }
        }

        [DefaultValue(false)]
        public new bool ShowInTaskbar
        {
            get { return base.ShowInTaskbar; }
            set { base.ShowInTaskbar = value; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;

                if (!_designMode)
                {
                    createParams.Style = NativeMethods.WS_POPUP | NativeMethods.WS_CLIPSIBLINGS | NativeMethods.WS_THICKFRAME;
                    createParams.ExStyle = NativeMethods.WS_EX_TOPMOST | NativeMethods.WS_EX_TOOLWINDOW | NativeMethods.WS_EX_LAYERED;
                }

                return createParams;
            }
        }

        [DefaultValue(500)]
        public int FadeDuration { get; set; }

        [DefaultValue(null)]
        public int? ShowDuration { get; set; }

        [DefaultValue(true)]
        public bool Animate { get; set; }

        [DefaultValue(false)]
        public bool ActivateOnShow { get; set; }

        [DefaultValue(false)]
        public bool DismissOnRightClick { get; set; }

        [DefaultValue(true)]
        public bool DismissOnDeactivate { get; set; }

        [DefaultValue(true)]
        public bool DelayHideOnMouseOver { get; set; }

        protected override bool ShowWithoutActivation
        {
            get { return !ActivateOnShow; }
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!_designMode)
            {
                if (Visible == value)
                    return;

                if (value)
                {
                    if (!ShouldShowPopup())
                    {
                        // Delay showing until its appropriate to show the popup.

                        SetState(FlyoutState.Pending);
                        return;
                    }
                    else if (
                        _state != FlyoutState.Opening &&
                        _state != FlyoutState.Showing &&
                        _state != FlyoutState.Waiting
                    )
                    {
                        // Showing should be done with any of the above state changes.

                        SetState(FlyoutState.Opening);
                        return;
                    }
                }
                else
                {
                    // Hiding should be done from the Closed state change.

                    if (_state != FlyoutState.Closed)
                    {
                        SetState(FlyoutState.Closed);
                        return;
                    }
                }

                if (value)
                    UpdatePosition();
            }

            base.SetVisibleCore(value);
        }

        private void UpdatePosition()
        {
            var location = TaskbarLocation.Detect();

            var bounds = location.WorkArea;

            bounds.Inflate(-10, -10);

            int top = bounds.Top;
            int left = bounds.Left;
            int right = bounds.Right;
            int bottom = bounds.Bottom;

            switch (location.Edge)
            {
                case TaskbarLocationEdge.Top:
                    bottom = top + Height;
                    break;

                default:
                    top = bottom - Height;
                    break;
            }

            switch (location.Edge)
            {
                case TaskbarLocationEdge.Left:
                    right = left + Width;
                    break;

                default:
                    left = right - Width;
                    break;
            }

            SetBounds(left, top, right - left, bottom - top);
        }

        protected override void WndProc(ref Message m)
        {
            if (_designMode)
            {
                base.WndProc(ref m);
                return;
            }

            switch (m.Msg)
            {
                case NativeMethods.WM_NCHITTEST:
                    // Disable all NC hit testing.

                    m.Result = (IntPtr)1;
                    return;

                case NativeMethods.WM_SETCURSOR:
                    // The WM_SETCURSOR catches all mouse moves and right
                    // clicks on the form.

                    switch (NativeMethods.Util.HIWORD(m.LParam))
                    {
                        case NativeMethods.WM_MOUSEMOVE:
                            switch (_state)
                            {
                                case FlyoutState.Showing:
                                case FlyoutState.Opening:
                                case FlyoutState.Closing:
                                    SetState(FlyoutState.Waiting);
                                    break;
                            }
                            break;

                        case NativeMethods.WM_RBUTTONDOWN:
                            if (DismissOnRightClick)
                                SetState(FlyoutState.Closed);
                            break;

                        case NativeMethods.WM_LBUTTONDOWN:
                            _mouseDown = true;
                            break;

                        case NativeMethods.WM_LBUTTONUP:
                            _mouseDown = false;
                            break;
                    }
                    break;

                case NativeMethods.WM_ACTIVATE:
                    switch (NativeMethods.Util.LOWORD(m.WParam))
                    {
                        case NativeMethods.WA_ACTIVE:
                            // Prevent window from getting focus. The browser
                            // control however messes with activation. When
                            // the mouse is over the form and the left mouse
                            // button is down, we're probably
                            // activating by mouse.

                            if (
                                !Bounds.Contains(Cursor.Position) ||
                                !_mouseDown
                            ) {
                                NativeMethods.SetActiveWindow(IntPtr.Zero);
                                m.Result = IntPtr.Zero;
                                Console.WriteLine("Suppressed focus {0}", this.Focused);
                                return;
                            }
                            break;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        private void SetState(FlyoutState state)
        {
            if (_changingState || _designMode)
                return;

            _changingState = true;

            // Transform the target state based on our state.

            switch (state)
            {
                case FlyoutState.Opening:
                    if (!Animate || DegradeVisualPerformance())
                        state = FlyoutState.Showing;
                    break;

                case FlyoutState.Closing:
                    if (!Animate || DegradeVisualPerformance())
                        state = FlyoutState.Closed;
                    break;
            }

            try
            {
                _timer.Stop();

                var currentState = _state;
                _state = state;

                SetStateCore(currentState);

                OnStateChanged(new FlyoutStateChangedEventArgs(currentState, _state));
            }
            finally
            {
                _changingState = false;
            }
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (_changingState || _designMode)
                return;

            _changingState = true;

            FlyoutState? newState;

            try
            {
                newState = TimerTickCore();
            }
            finally
            {
                _changingState = false;
            }

            if (newState.HasValue)
                SetState(newState.Value);
        }

        private void SetStateCore(FlyoutState currentState)
        {
            switch (_state)
            {
                case FlyoutState.Closed:
                    Visible = false;
                    break;

                case FlyoutState.Closing:
                    Opacity = OpaqueOpacity;
                    Visible = true;

                    _timer.Interval = FadeDuration / AnimateSteps;
                    _timer.Start();
                    break;

                case FlyoutState.Opening:
                    Opacity = 0;
                    Visible = true;

                    _timer.Interval = FadeDuration / AnimateSteps;
                    _timer.Start();
                    break;

                case FlyoutState.Pending:
                    Visible = false;
                    
                    _timer.Interval = PendingDuration;
                    _timer.Start();
                    break;

                case FlyoutState.Showing:
                    Opacity = OpaqueOpacity;
                    Visible = true;

                    if (ShowDuration.HasValue)
                    {
                        // Don't wait the full duration when we're continuing
                        // from a wait.

                        _timer.Interval =
                            currentState == FlyoutState.Waiting
                            ? ShowDuration.Value / 2
                            : ShowDuration.Value;

                        _timer.Start();
                    }
                    break;

                case FlyoutState.Waiting:
                    Opacity = OpaqueOpacity;
                    Visible = true;

                    _timer.Interval = WaitingDuration;
                    _timer.Start();
                    break;

                default:
                    throw new NotSupportedException("Unknown state");
            }
        }

        private FlyoutState? TimerTickCore()
        {
            double opacity;

            switch (_state)
            {
                case FlyoutState.Closed:
                    Debug.Fail("Timer should not be running");
                    break;

                case FlyoutState.Closing:
                    opacity = Opacity - (1.0 / (double)AnimateSteps);

                    if (opacity <= 0.0)
                    {
                        Opacity = 0.0;

                        return FlyoutState.Closed;
                    }
                    else
                    {
                        Opacity = opacity;
                    }
                    break;

                case FlyoutState.Opening:
                    opacity = Opacity + (1.0 / (double)AnimateSteps);

                    if (opacity >= 1.0)
                    {
                        Opacity = OpaqueOpacity;

                        return FlyoutState.Showing;
                    }
                    else
                    {
                        Opacity = opacity;
                    }
                    break;

                case FlyoutState.Pending:
                    if (ShouldShowPopup())
                        return FlyoutState.Opening;
                    break;

                case FlyoutState.Showing:
                    var e = new CancelEventArgs();

                    OnEndShowing(e);

                    if (e.Cancel)
                        return FlyoutState.Showing;
                    else
                        return FlyoutState.Closing;

                case FlyoutState.Waiting:
                    if (!Bounds.Contains(Cursor.Position))
                        return FlyoutState.Showing;
                    break;

                default:
                    throw new NotSupportedException("Unknown state");
            }

            return null;
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            if (DismissOnDeactivate)
            {
                switch (_state)
                {
                    case FlyoutState.Opening:
                        SetState(FlyoutState.Closed);
                        break;

                    case FlyoutState.Showing:
                    case FlyoutState.Waiting:
                        SetState(FlyoutState.Closing);
                        break;
                }
            }
        }

        private bool DegradeVisualPerformance()
        {
#if DEBUG
            return false;
#else
            return SystemInformation.TerminalServerSession;
#endif
        }

        private bool ShouldShowPopup()
        {
            var popupScreen = Screen.FromPoint(TaskbarLocation.Detect().WorkArea.Location);

            return PopupUtil.ShouldShowPopup(this, popupScreen);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
