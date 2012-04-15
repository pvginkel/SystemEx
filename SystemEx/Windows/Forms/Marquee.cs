using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class Marquee : Panel
    {
        private Timer _timer = new Timer();
        int _distance = 0;

        public Marquee()
        {
            Disposed += new EventHandler(Marquee_Disposed);

            _timer.Tick += new EventHandler(Timer_Tick);
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            _distance += 4;

            if (_distance > BackgroundImage.Width)
                _distance -= BackgroundImage.Width;

            Invalidate(false);
        }

        void Marquee_Disposed(object sender, EventArgs e)
        {
            _timer.Dispose();
        }

        [DefaultValue(false)]
        public bool Running
        {
            get { return _timer.Enabled; }
            set { _timer.Enabled = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (BackgroundImage != null)
            {
                int start = _distance == 0 ? 0 : _distance - BackgroundImage.Width;

                while (start < Width)
                {
                    e.Graphics.DrawImage(
                        BackgroundImage,
                        new Rectangle(start, 0, BackgroundImage.Width, Height)
                    );

                    start += BackgroundImage.Width;
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_ERASEBKGND)
                m.Msg = 0;

            base.WndProc(ref m);
        }
    }
}
