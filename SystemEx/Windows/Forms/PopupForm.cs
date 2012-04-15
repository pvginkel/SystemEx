using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    internal partial class PopupForm : System.Windows.Forms.Form
    {
        private const int AnimationSteps = 20;

        private static PopupForm _instance = null;

        private List<Popup> _queue = new List<Popup>();
        private TaskbarLocationEdge _edge;
        private Screen _popupScreen;
        private PopupState _state;
        private bool _mouseOver;
        private int _animationStep;
        private Popup _current;
        private Timer _popupTimer;
        private Timer _idleCompleteTimer;

        internal PopupForm()
        {
            _instance = this;
            _current = null;
            _mouseOver = false;
            _animationStep = 0;
            _popupScreen = null;

            _popupTimer = new Timer();

            _popupTimer.Tick += new EventHandler(PopupTimer_Tick);

            _idleCompleteTimer = new Timer { Interval = 4000 };

            _idleCompleteTimer.Tick += new EventHandler(IdleCompleteTimer_Tick);

            Disposed += new EventHandler(PopupForm_Disposed);

            SetStyle(ControlStyles.Opaque | ControlStyles.Selectable , false);
            SetStyle(ControlStyles.OptimizedDoubleBuffer| ControlStyles.SupportsTransparentBackColor, true);
        }

        public Popup Current { get { return _current; } }

        public PopupState PopupState { get { return _state; } }

        void PopupForm_Disposed(object sender, EventArgs e)
        {
            if (_current != null)
            {
                _current.Dispose();
                _current = null;
            }

            _popupTimer.Dispose();
            _idleCompleteTimer.Dispose();

            _instance = null;

            while (_queue.Count > 0)
            {
                var popup = _queue[0];

                _queue.RemoveAt(0);

                popup.Show();
            }
        }

        internal static PopupForm GetInstance(bool create)
        {
            if (_instance == null && create)
            {
                new PopupForm();
            }

            return _instance;
        }

        public static PopupForm Instance
        {
            get { return _instance; }
        }

        internal void Show(Popup popup)
        {
            _queue.Add(popup);

            if (_current == null)
            {
                _current = _queue[0];

                _queue.RemoveAt(0);

                Cursor = _current.Cursor;

                CalculatePopupRect();

                if (PopupUtil.ShouldShowPopup(this, _popupScreen))
                {
                    ShowPopup();
                }
                else
                {
                    _state = PopupState.Pending;

                    _popupTimer.Interval = 200;
                    _popupTimer.Start();
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;

                createParams.Style = NativeMethods.WS_POPUP | NativeMethods.WS_CLIPSIBLINGS;
                createParams.ExStyle = NativeMethods.WS_EX_TOPMOST | NativeMethods.WS_EX_TOOLWINDOW | NativeMethods.WS_EX_LAYERED;

                return createParams;
            }
        }

        private void CalculatePopupRect()
        {
            var sPopupSize = _current.Size;
            var sOffset = _current.BorderOffset;

            var rc = new Rectangle();

            var tl = TaskbarLocation.Detect();

            rc = GetTaskbarPopupLocation(tl, sPopupSize, sOffset);

            _current.Location = rc;

            _edge = tl.Edge;

            var pt = new Point(
                rc.Left + rc.Width / 2,
                rc.Top + rc.Height / 2
            );

            _popupScreen = Screen.FromPoint(pt);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Right)
            {
                CancelAll();
            }
            else
            {
                _current.RaiseMouseUp(e);
            }
        }

        void IdleCompleteTimer_Tick(object sender, EventArgs e)
        {
            _idleCompleteTimer.Stop();

            ShowPopup();
        }

        void PopupTimer_Tick(object sender, EventArgs e)
        {
            switch (_state)
            {
                case PopupState.Pending: AnimatePending(); break;
                case PopupState.Opening: AnimateOpening(); break;
                case PopupState.Waiting: AnimateWaiting(); break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!_mouseOver)
            {
                _mouseOver = true;

                if (_state == PopupState.Waiting)
                {
                    _popupTimer.Stop();
                }
            }

            _current.RaiseMouseMove(e);
        }

        private void AnimatePending()
        {
            if (PopupUtil.ShouldShowPopup(this, _popupScreen))
            {
#if DEBUG
                ShowPopup();
#else
                _popupTimer.Stop();
                _idleCompleteTimer.Start();
#endif
            }
        }

        private void AnimateOpening()
        {
            if (_animationStep == AnimationSteps)
            {
                OpeningComplete();
            }
            else
            {
                UpdateFromAnimationStep();

                _animationStep++;
            }
        }

        private void OpeningComplete()
        {
            _animationStep = AnimationSteps;
            _state = PopupState.Waiting;

            UpdateFromAnimationStep();

            if (!_mouseOver)
            {
                _popupTimer.Interval = _current.Duration;
                _popupTimer.Start();
            }
            else
            {
                _popupTimer.Stop();
            }
        }

        private void AnimateWaiting()
        {
            _popupTimer.Stop();

            if (_queue.Count > 0)
            {
                _state = PopupState.Waiting;

                if (_current != null)
                {
                    _current.Dispose();
                }

                _current = _queue[0];

                _queue.RemoveAt(0);

                if (_current == null)
                    throw new Exception("Expected current popup");

                CalculatePopupRect();
                UpdateFromAnimationStep();

                _popupTimer.Interval = _current.Duration;
                _popupTimer.Start();

                Invalidate();
            }
            else
            {
                CompleteClosing();
            }
        }

        private void CompleteClosing()
        {
            _animationStep = 0;

            Close();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (_mouseOver)
            {
                _mouseOver = false;

                if (_state == PopupState.Waiting)
                {
                    _popupTimer.Interval = 2000;
                    _popupTimer.Start();
                }
            }

            _current.RaiseMouseLeave(e);
        }

        private void UpdateFromAnimationStep()
        {
            double opacity;
            int offset;

            if (_current == null)
                throw new Exception("Expected current");

            if (_animationStep == AnimationSteps || (_current.Animation & PopupAnimation.Slide) == 0)
            {
                offset = 0;
            }
            else
            {
                int animationHeight = _current.Size.Height / 2;

                offset = animationHeight - ((animationHeight * _animationStep) / AnimationSteps);
            }

            if (_animationStep == AnimationSteps || (_current.Animation & PopupAnimation.Transparency) == 0)
            {
                opacity = 1.0;
            }
            else
            {
                opacity = (double)_animationStep / (double)AnimationSteps;
            }

            var rc = _current.Location;

            int left = rc.Left;
            int top = rc.Top;
            int right = rc.Right;
            int bottom = rc.Bottom;

            switch (_edge)
            {
                case TaskbarLocationEdge.Left:
                    left -= offset;
                    right -= offset;
                    break;

                case TaskbarLocationEdge.Right:
                    left += offset;
                    right += offset;
                    break;

                case TaskbarLocationEdge.Top:
                    top -= offset;
                    bottom -= offset;
                    break;

                case TaskbarLocationEdge.Bottom:
                    top += offset;
                    bottom += offset;
                    break;
            }

            NativeMethods.SetLayeredWindowAttributes(Handle, 0, (byte)(int)(opacity * 255), NativeMethods.LWA_ALPHA);

            NativeMethods.SetWindowPos(
                Handle,
                IntPtr.Zero,
                left,
                top,
                right - left,
                bottom - top,
                NativeMethods.SWP_NOACTIVATE | NativeMethods.SWP_NOZORDER);
        }

        private void ShowPopup()
        {
            Popup.RaisePopupOpening(new PopupEventArgs(_current));

            if (DegradeVisualPerformance())
            {
                OpeningComplete();
            }
            else
            {
                _state = PopupState.Opening;
                _animationStep = 0;

                _popupTimer.Interval = _current.AnimationDuration / AnimationSteps;
                _popupTimer.Start();

                NativeMethods.SetLayeredWindowAttributes(Handle, 0, 0, NativeMethods.LWA_ALPHA);
            }

            NativeMethods.ShowWindow(Handle, NativeMethods.SW_SHOWNOACTIVATE);
        }

        private void CancelAll()
        {
            while (_queue.Count > 0)
            {
                var popup = _queue[0];

                popup.Dispose();

                _queue.RemoveAt(0);
            }

            Close();
        }

        public void ShowNext()
        {
            if (_queue.Count == 0)
            {
                CancelAll();
            }
            else
            {
                _state = PopupState.Waiting;

                AnimateWaiting();
            }
        }

        public IEnumerable<Popup> GetPopups()
        {
            return _queue.ToArray();
        }

        public void CancelPopup(Popup popup)
        {
            if (popup == _current)
            {
                ShowNext();
            }
            else
            {
                if (_queue.Remove(popup))
                {
                    popup.Dispose();
                }
            }
        }

        public void ExtendPopupDuration(int duration)
        {
            if (_mouseOver && _state == PopupState.Waiting)
            {
                _popupTimer.Stop();
                _popupTimer.Interval = duration;
                _popupTimer.Start();
            }
        }

        private Rectangle GetTaskbarPopupLocation(TaskbarLocation tl, Size sSize, Size sOffset)
        {
            int left = tl.WorkArea.Left;
            int right = tl.WorkArea.Right;
            int top = tl.WorkArea.Top;
            int bottom = tl.WorkArea.Bottom;

            switch (tl.Edge)
            {
                case TaskbarLocationEdge.Left:
                    left += sOffset.Width;
                    right = left + sSize.Width;
                    bottom -= sOffset.Height;
                    top = bottom - sSize.Height;
                    break;

                case TaskbarLocationEdge.Right:
                    right -= sOffset.Width;
                    left = right - sSize.Width;
                    bottom -= sOffset.Height;
                    top = bottom - sSize.Height;
                    break;

                case TaskbarLocationEdge.Top:
                    right -= sOffset.Width;
                    left = right - sSize.Width;
                    top += sOffset.Height;
                    bottom = top + sSize.Height;
                    break;

                case TaskbarLocationEdge.Bottom:
                    right -= sOffset.Width;
                    left = right - sSize.Width;
                    bottom -= sOffset.Height;
                    top = bottom - sSize.Height;
                    break;

                default:
                    throw new Exception("Could not calculate popup location");
            }

            return new Rectangle(left, top, right - left, bottom - top);
        }

        private bool DegradeVisualPerformance()
        {
#if DEBUG
            return false;
#else
            return SystemInformation.TerminalServerSession;
#endif
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_ERASEBKGND)
            {
                m.Msg = NativeMethods.WM_NULL;
            }

            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_current == null)
            {
                base.OnPaint(e);
            }
            else
            {
                _current.RaisePaint(e);
            }
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);

            _current.RaiseMouseCaptureChanged(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            _current.RaiseMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            _current.RaiseMouseDoubleClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _current.RaiseMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            _current.RaiseMouseEnter(e);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

            _current.RaiseMouseHover(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            _current.RaiseMouseWheel(e);
        }
    }

    public class PopupEventArgs : EventArgs
    {
        public PopupEventArgs(Popup popup)
        {
            Popup = popup;
        }

        public Popup Popup { get; private set; }
    }

    [Flags]
    public enum PopupAnimation
    {
        None = 0,
        Transparency = 1,
        Slide = 2
    }
}
