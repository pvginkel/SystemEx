using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public class Popup : IDisposable
    {
        private bool _disposed = false;

        public Popup()
        {
            Duration = 5000;
            Size = new Size(160, 120);
            AnimationDuration = 120;
            Location = Rectangle.Empty;
            Animation = PopupAnimation.Slide | PopupAnimation.Transparency;
            BorderOffset = Size.Empty;
            Cursor = Cursors.Default;
        }

        public Size Size { get; set; }
        internal Rectangle Location { get; set; }
        public int Duration { get; set; }
        public int AnimationDuration { get; set; }
        public PopupAnimation Animation { get; set; }
        public Size BorderOffset { get; set; }
        public Cursor Cursor { get; set; }
        public object Tag { get; set; }

        public System.Windows.Forms.Form Owner
        {
            get { return PopupForm.Instance; }
        }

        public Rectangle ClientRectangle
        {
            get { return new Rectangle(Point.Empty, Size); }
        }

        public event EventHandler<PaintEventArgs> Paint;
        public event EventHandler Disposed;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseMove;
        public event EventHandler MouseLeave;
        public event EventHandler MouseCaptureChanged;
        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseDoubleClick;
        public event EventHandler MouseEnter;
        public event EventHandler MouseHover;
        public event MouseEventHandler MouseWheel;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                OnDisposed(EventArgs.Empty);
            }
        }

        protected virtual void OnDisposed(EventArgs e)
        {
            if (Disposed != null)
            {
                Disposed(this, e);
            }
        }

        internal void RaisePaint(PaintEventArgs e)
        {
            OnPaint(e);
        }

        protected virtual void OnPaint(PaintEventArgs e)
        {
            if (Paint != null)
            {
                Paint(this, e);
            }
        }

        public void Show()
        {
            PopupForm.GetInstance(true).Show(this);
        }

        public void ExtendDuration(int duration)
        {
            if (IsDisplaying())
            {
                PopupForm.Instance.ExtendPopupDuration(duration);
            }
            else if (duration > Duration)
            {
                Duration = duration;
            }
        }

        public bool IsDisplaying()
        {
            var form = PopupForm.GetInstance(false);

            return form.Current == this && form.PopupState != PopupState.Pending;
        }

        internal void RaiseMouseUp(MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        internal void RaiseMouseMove(MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        internal void RaiseMouseLeave(EventArgs e)
        {
            OnMouseLeave(e);
        }

        internal void RaiseMouseCaptureChanged(EventArgs e)
        {
            OnMouseCaptureChanged(e);
        }

        internal void RaiseMouseClick(MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        internal void RaiseMouseDoubleClick(MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }

        internal void RaiseMouseDown(MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        internal void RaiseMouseEnter(EventArgs e)
        {
            OnMouseEnter(e);
        }

        internal void RaiseMouseHover(EventArgs e)
        {
            OnMouseHover(e);
        }

        internal void RaiseMouseWheel(MouseEventArgs e)
        {
            OnMouseWheel(e);
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            if (MouseUp != null)
            {
                MouseUp(this, e);
            }
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            if (MouseDown != null)
            {
                MouseDown(this, e);
            }
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null)
            {
                MouseMove(this, e);
            }
        }

        protected virtual void OnMouseClick(MouseEventArgs e)
        {
            if (MouseClick != null)
            {
                MouseClick(this, e);
            }
        }

        protected virtual void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (MouseDoubleClick != null)
            {
                MouseDoubleClick(this, e);
            }
        }

        protected virtual void OnMouseWheel(MouseEventArgs e)
        {
            if (MouseWheel != null)
            {
                MouseWheel(this, e);
            }
        }

        protected virtual void OnMouseLeave(EventArgs e)
        {
            if (MouseLeave != null)
            {
                MouseLeave(this, e);
            }
        }

        protected virtual void OnMouseCaptureChanged(EventArgs e)
        {
            if (MouseCaptureChanged != null)
            {
                MouseCaptureChanged(this, e);
            }
        }

        protected virtual void OnMouseEnter(EventArgs e)
        {
            if (MouseEnter != null)
            {
                MouseEnter(this, e);
            }
        }

        protected virtual void OnMouseHover(EventArgs e)
        {
            if (MouseHover != null)
            {
                MouseHover(this, e);
            }
        }

        public static EventHandler<PopupEventArgs> PopupOpening;

        protected static void OnPopupOpening(PopupEventArgs e)
        {
            if (PopupOpening != null)
            {
                PopupOpening(null, e);
            }
        }

        internal static void RaisePopupOpening(PopupEventArgs e)
        {
            OnPopupOpening(e);
        }

        public static Popup Current
        {
            get { return PopupForm.Instance == null ? null : PopupForm.Instance.Current; }
        }

        public static void ShowNext()
        {
            if (PopupForm.Instance != null)
            {
                PopupForm.Instance.ShowNext();
            }
        }

        public static IEnumerable<Popup> GetPopups()
        {
            if (PopupForm.Instance != null)
            {
                return PopupForm.Instance.GetPopups();
            }
            else
            {
                return new Popup[0];
            }
        }

        public static void CancelPopup(Popup popup)
        {
            if (PopupForm.Instance != null)
            {
                PopupForm.Instance.CancelPopup(popup);
            }
        }

        public static void ExtendPopupDuration(int duration)
        {
            if (PopupForm.Instance != null)
            {
                PopupForm.Instance.ExtendPopupDuration(duration);
            }
        }
    }

    public enum PopupState
    {
        Pending,
        Opening,
        Waiting
    }
}
