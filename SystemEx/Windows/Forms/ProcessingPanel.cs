using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public partial class ProcessingPanel : Panel
    {
        private const int Steps = 32;

        private ProcessingSize _processingSize;
        private Size _size;
        private Bitmap _bitmap;
        private bool _running;
        private Timer _timer;
        private int _updateInterval;
        private bool _inDesignMode;

        public ProcessingPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, false);

            _processingSize = Forms.ProcessingSize.Large;
            _updateInterval = 25;
            _inDesignMode = ControlUtil.GetIsInDesignMode(this);

            UpdateFromSize();
        }

        public ProcessingPanel(IContainer container)
            : this()
        {
            if (container != null)
            {
                container.Add(this);
            }
        }

        [Category("Layout")]
        [Browsable(true)]
        [DefaultValue(typeof(ProcessingSize), "Large")]
        public ProcessingSize ProcessingSize
        {
            get { return _processingSize; }
            set
            {
                _processingSize = value;

                UpdateFromSize();
            }
        }

        [Category("Behavior")]
        [Browsable(true)]
        [DefaultValue(25)]
        public int UpdateInterval
        {
            get { return _updateInterval; }
            set
            {
                if (_updateInterval != value)
                {
                    _updateInterval = value;

                    if (_timer != null)
                        _timer.Interval = _updateInterval;
                }
            }
        }

        private void UpdateFromSize()
        {
            switch (_processingSize)
            {
                case ProcessingSize.Small:
                    _size = new Size(16, 16);
                    _bitmap = Properties.Resources.processing_small;
                    break;

                case ProcessingSize.Medium:
                    _size = new Size(22, 22);
                    _bitmap = Properties.Resources.processing_medium;
                    break;

                case ProcessingSize.Large:
                    _size = new Size(32, 32);
                    _bitmap = Properties.Resources.processing_large;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("value");
            }

            Size = _size;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if ((specified & BoundsSpecified.Height) != 0)
                height = _size.Height;
            if ((specified & BoundsSpecified.Width) != 0)
                width = _size.Width;

            base.SetBoundsCore(x, y, width, height, specified);
        }

        [Category("Behavior")]
        [Browsable(true)]
        [DefaultValue(false)]
        public bool Running
        {
            get { return _running; }
            set
            {
                if (_running != value)
                {
                    _running = value;

                    if (!_inDesignMode)
                    {
                        if (value)
                            Start();
                        else
                            Stop();
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintStep(e.Graphics);
        }

        private void PaintStep(Graphics g)
        {
            int step;

            if (Running && !_inDesignMode)
            {
                long time = Stopwatch.GetTimestamp();

                step = (int)((time / (Stopwatch.Frequency / 1000L * (long)_updateInterval)) % (long)(Steps - 1)) + 1;
            }
            else
            {
                step = 0;
            }

            int row = step / 8;
            int col = step % 8;

            var sourceRect = new Rectangle(
                _size.Width * col, _size.Height * row,
                _size.Width, _size.Height);

            var destRect = new Rectangle(0, 0, _size.Width, _size.Height);

            g.DrawImage(_bitmap, destRect, sourceRect, GraphicsUnit.Pixel);
        }

        private void Start()
        {
            PaintStep();

            _timer = new Timer();

            _timer.Tick += Tick;
            _timer.Interval = _updateInterval;

            _timer.Start();
        }

        void Tick(object sender, EventArgs e)
        {
            PaintStep();
        }

        private void Stop()
        {
            _timer.Stop();
            _timer.Tick -= Tick;
            _timer.Dispose();
            
            _timer = null;

            PaintStep();
        }

        private void PaintStep()
        {
            using (var g = CreateGraphics())
            {
                PaintStep(g);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_ERASEBKGND)
                return;

            base.WndProc(ref m);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (_timer != null)
                    _timer.Dispose();
            }
        }
    }

    public enum ProcessingSize
    {
        Small,
        Medium,
        Large
    }
}
