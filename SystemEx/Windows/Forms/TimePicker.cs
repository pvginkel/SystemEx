using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SystemEx.Windows.Forms
{
    [DefaultProperty("SelectedTime")]
    [DefaultEvent("SelectedTimeChanged")]
    public class TimePicker : ComboBox
    {
        private static readonly string[] Values = new string[] {
            "0:00", "0:30", "1:00", "1:30", "2:00", "2:30", "3:00", "3:30", "4:00", "4:30",
            "5:00", "5:30", "6:00", "6:30", "7:00", "7:30", "8:00", "8:30", "9:00", "9:30",
            "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30",
            "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30",
            "20:00", "20:30", "21:00", "21:30", "22:00", "22:30", "23:00", "23:30"
        };

        private const int MinValue = 0;
        private const int MaxValue = 86400; // 24:00:00

        private bool _isNullable = true;
        private int? _selectedTime;
        private bool _midnightInclusive;

        [Category("Behavior")]
        [DefaultValue(true)]
        public bool IsNullable
        {
            get { return _isNullable; }
            set
            {
                if (_isNullable != value)
                {
                    _isNullable = value;

                    if (!_isNullable && !_selectedTime.HasValue)
                        SelectedTime = 0;
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool MidnightInclusive
        {
            get { return _midnightInclusive; }
            set
            {
                if (_midnightInclusive != value)
                {
                    _midnightInclusive = value;

                    if (
                        !_midnightInclusive &&
                        _selectedTime.HasValue &&
                        _selectedTime.Value > MaxValue - 1
                    )
                        SelectedTime = MaxValue - 1;
                }
            }
        }

        [Category("Appearance")]
        [DefaultValue(null)]
        public int? SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                value = CoerceValue(value);

                if (!Equals(_selectedTime, value))
                {
                    _selectedTime = value;

                    FormatText(_selectedTime);

                    OnSelectedTimeChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Behavior")]
        public event EventHandler SelectedTimeChanged;

        public TimePicker()
        {
            if (!ControlUtil.GetIsInDesignMode(this))
                Items.AddRange(Values);
        }

        protected virtual void OnSelectedTimeChanged(EventArgs e)
        {
            if (SelectedTimeChanged != null)
                SelectedTimeChanged(this, e);
        }

        private void FormatText(int? value)
        {
            if (value.HasValue)
            {
                var time = TimeSpan.FromSeconds(value.Value);

                if (time.Seconds == 0)
                    Text = String.Format("{0}:{1:00}", time.Hours, time.Minutes);
                else
                    Text = String.Format("{0}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);
            }
            else
            {
                Text = "";
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            ValidateText();

            base.OnLeave(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            ValidateText();

            base.OnSelectedIndexChanged(e);
        }

        private void ValidateText()
        {
            int? newSelectedTime = null;

            string text = Text;

            if (text != null)
                text = text.Trim();

            if (!String.IsNullOrEmpty(text))
            {
                var matches = Regex.Matches(text, "\\d+");

                if (matches.Count >= 1 && matches.Count <= 3)
                {
                    int hours = int.Parse(matches[0].Value);
                    int minutes = 0;
                    int seconds = 0;

                    if (matches.Count >= 2)
                    {
                        minutes = int.Parse(matches[1].Value);

                        if (matches.Count == 3)
                            seconds = int.Parse(matches[2].Value);
                    }
                    else if (hours >= 100)
                    {
                        minutes = hours;
                        hours = 0;
                    }

                    if (seconds >= 100)
                    {
                        minutes += seconds / 100;
                        seconds %= 100;
                    }

                    if (minutes >= 100)
                    {
                        hours += minutes / 100;
                        minutes %= 100;
                    }

                    if (seconds > 59)
                        seconds = 0;
                    if (minutes > 59)
                        minutes = 0;
                    if (hours > 23)
                        hours = 0;

                    newSelectedTime = hours * 3600 + minutes * 60 + seconds;
                }
            }

            newSelectedTime = CoerceValue(newSelectedTime);

            if (Equals(_selectedTime, newSelectedTime))
                FormatText(newSelectedTime);
            else
                SelectedTime = newSelectedTime;
        }

        private int? CoerceValue(int? value)
        {
            if (value.HasValue)
            {
                if (value < MinValue)
                    value = MinValue;
                else
                {
                    int maxValue = MaxValue - (_midnightInclusive ? 0 : 1);

                    if (value > maxValue)
                        value = maxValue;
                }
            }
            else if (!_isNullable)
            {
                value = 0;
            }

            return value;
        }
    }
}
