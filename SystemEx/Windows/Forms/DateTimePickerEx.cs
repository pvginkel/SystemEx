using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    [DefaultProperty("SelectedDateTime")]
    [DefaultEvent("SelectedDateTimeChanged")]
    public partial class DateTimePickerEx : UserControl
    {
        private bool _updating;
        private DateTime? _selectedDateTime;

        [Category("Behavior")]
        public event EventHandler SelectedDateTimeChanged;

        [Browsable(true)]
        [DefaultValue(DateTimePickerFormat.Long), TypeConverter(typeof(Enum))]
        [Category("Appearance")]
        public DateTimePickerFormat DateFormat
        {
            get { return _dateTimePicker.Format; }
            set { _dateTimePicker.Format = value; }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            _timePicker.Bounds = new Rectangle(
                Width - _timePicker.Width,
                0,
                _timePicker.Width,
                _timePicker.Height
            );

            _dateTimePicker.Bounds = new Rectangle(
                0,
                0,
                Width - (_timePicker.Width + 6),
                _timePicker.Height
            );

            base.OnLayout(levent);
        }

        protected override Size DefaultSize
        {
            get { return new Size(250, 21); } 
        }

        public DateTimePickerEx()
        {
            InitializeComponent();

            ForceHeight();
        }

        protected virtual void OnSelectedDateTimeChanged(EventArgs e)
        {
            if (SelectedDateTimeChanged != null)
                SelectedDateTimeChanged(this, e);
        }

        private void timePicker_SizeChanged(object sender, EventArgs e)
        {
            ForceHeight();
        }

        private void ForceHeight()
        {
            _dateTimePicker.MinimumSize = new Size(0, _timePicker.Height);
            _dateTimePicker.Height = _timePicker.Height;

            Height = _timePicker.Height;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if ((specified & BoundsSpecified.Height) != 0)
                height = _timePicker.Height;

            base.SetBoundsCore(x, y, width, height, specified);
        }

        [Category("Behavior")]
        [DefaultValue(null)]
        public DateTime? SelectedDateTime
        {
            get { return _selectedDateTime; }
            set
            {
                if (!Equals(_selectedDateTime, value))
                {
                    try
                    {
                        _updating = true;

                        _selectedDateTime = value;

                        if (_selectedDateTime.HasValue)
                        {
                            _dateTimePicker.Value = _selectedDateTime.Value.Date;
                            _timePicker.SelectedTime = (int)_selectedDateTime.Value.TimeOfDay.TotalSeconds;
                        }
                        else
                        {
                            _dateTimePicker.Value = null;
                            _timePicker.SelectedTime = null;
                        }

                        OnSelectedDateTimeChanged(EventArgs.Empty);
                    }
                    finally
                    {
                        _updating = false;
                    }
                }
            }
        }

        private void _dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_updating)
                return;

            DateTime? value = null;

            if (_dateTimePicker.Value.HasValue)
            {
                value =
                    _dateTimePicker.Value.Value +
                    (_timePicker.SelectedTime.HasValue ? TimeSpan.FromSeconds(_timePicker.SelectedTime.Value) : new TimeSpan(0));
            }

            SelectedDateTime = value;
        }

        private void _timePicker_SelectedTimeChanged(object sender, EventArgs e)
        {
            if (_updating)
                return;

            DateTime? value = null;

            if (_timePicker.SelectedTime.HasValue)
            {
                value =
                    (_dateTimePicker.Value.HasValue ? _dateTimePicker.Value.Value : DateTime.Now).Date +
                    TimeSpan.FromSeconds(_timePicker.SelectedTime.Value);
            }

            SelectedDateTime = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTimePicker DateControl
        {
            get { return _dateTimePicker; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimePicker TimeControl
        {
            get { return _timePicker; }
        }
    }
}
