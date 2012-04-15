using System.Text;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

// Copyright (c) 2005 Claudio Grazioli, http://www.grazioli.ch
//
// This code is free software; you can redistribute it and/or modify it.
// However, this header must remain intact and unchanged.  Additional
// information may be appended after this header.  Publications based on
// this code must also include an appropriate reference.
// 
// This code is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE.
//

namespace SystemEx.Windows.Forms
{
    /// <summary>
    /// Represents a Windows date time picker control. It enhances the .NET standard <b>DateTimePicker</b>
    /// control with the possibility to show empty values (null values).
    /// </summary>
    [ComVisible(false)]
    public class DateTimePicker : System.Windows.Forms.DateTimePicker
    {
        // true, when no date shall be displayed (empty DateTimePicker)
        private bool _isNull;

        // If _isNull = true, this value is shown in the DTP
        private string _nullValue;

        // The format of the DateTimePicker control
        private DateTimePickerFormat _format = DateTimePickerFormat.Long;

        // The custom format of the DateTimePicker control
        private string _customFormat;

        // The format of the DateTimePicker control as string
        private string _formatAsString;

        private SolidBrush _backgroundBrush = null;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DateTimePicker()
            : base()
        {
            base.Format = DateTimePickerFormat.Custom;
            NullValue = " ";
            Format = DateTimePickerFormat.Long;

            _backgroundBrush = new SolidBrush(BackColor);

            Value = null;
        }

        /// <summary>
        /// Gets or sets the date/time value assigned to the control.
        /// </summary>
        /// <value>The DateTime value assigned to the control
        /// </value>
        /// <remarks>
        /// <p>If the <b>Value</b> property has not been changed in code or by the user, it is set
        /// to the current date and time (<see cref="DateTime.Now"/>).</p>
        /// <p>If <b>Value</b> is <b>null</b>, the DateTimePicker shows 
        /// <see cref="NullValue"/>.</p>
        /// </remarks>
        [Bindable(true)]
        [Browsable(false)]
        [DefaultValue(null)]
        public new DateTime? Value
        {
            get
            {
                if (_isNull)
                {
                    return null;
                }
                else
                {
                    if (Format == DateTimePickerFormat.Time)
                        return base.Value;
                    else
                        return base.Value.Date;
                }
            }
            set
            {
                if (value.HasValue)
                {
                    SetToDateTimeValue();
                    base.Value = (DateTime)value;
                }
                else
                {
                    SetToNullValue();
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the format of the date and time displayed in the control.
        /// </summary>
        /// <value>One of the <see cref="DateTimePickerFormat"/> values. The default is 
        /// <see cref="DateTimePickerFormat.Long"/>.</value>
        [Browsable(true)]
        [DefaultValue(DateTimePickerFormat.Long), TypeConverter(typeof(Enum))]
        public new DateTimePickerFormat Format
        {
            get { return _format; }
            set
            {
                _format = value;
                if (!_isNull)
                    SetFormat();
                OnFormatChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the custom date/time format string.
        /// <value>A string that represents the custom date/time format. The default is a null
        /// reference (<b>Nothing</b> in Visual Basic).</value>
        /// </summary>
        public new String CustomFormat
        {
            get { return _customFormat; }
            set { _customFormat = value; }
        }

        /// <summary>
        /// Gets or sets the string value that is assigned to the control as null value. 
        /// </summary>
        /// <value>The string value assigned to the control as null value.</value>
        /// <remarks>
        /// If the <see cref="Value"/> is <b>null</b>, <b>NullValue</b> is
        /// shown in the <b>DateTimePicker</b> control.
        /// </remarks>
        [Browsable(true)]
        [Category("Behavior")]
        [Description("The string used to display null values in the control")]
        [DefaultValue(" ")]
        public String NullValue
        {
            get { return _nullValue; }
            set { _nullValue = value; }
        }

        /// <summary>
        /// Stores the current format of the DateTimePicker as string. 
        /// </summary>
        private string FormatAsString
        {
            get { return _formatAsString; }
            set
            {
                _formatAsString = value;
                base.CustomFormat = value;
            }
        }

        /// <summary>
        /// Sets the format according to the current DateTimePickerFormat.
        /// </summary>
        private void SetFormat()
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            DateTimeFormatInfo dtf = ci.DateTimeFormat;
            switch (_format)
            {
                case DateTimePickerFormat.Long:
                    FormatAsString = dtf.LongDatePattern;
                    break;
                case DateTimePickerFormat.Short:
                    FormatAsString = dtf.ShortDatePattern;
                    break;
                case DateTimePickerFormat.Time:
                    FormatAsString = dtf.ShortTimePattern;
                    break;
                case DateTimePickerFormat.Custom:
                    FormatAsString = this.CustomFormat;
                    break;
            }
        }

        /// <summary>
        /// Sets the <b>DateTimePicker</b> to the value of the <see cref="NullValue"/> property.
        /// </summary>
        private void SetToNullValue()
        {
            if (!_isNull)
            {
                _isNull = true;
                base.CustomFormat = (_nullValue == null || _nullValue == String.Empty) ? " " : "'" + _nullValue + "'";
            }
        }

        /// <summary>
        /// Sets the <b>DateTimePicker</b> back to a non null value.
        /// </summary>
        private void SetToDateTimeValue()
        {
            if (_isNull)
            {
                SetFormat();
                _isNull = false;
                OnValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// This member overrides <see cref="Control.WndProc"/>.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.NativeMethods.WM_NOTIFY:
                    if (_isNull)
                    {
                        NMHDR nm = (NMHDR)m.GetLParam(typeof(NMHDR));
                        Console.WriteLine(nm.Code);
                        if (nm.Code == -746 || nm.Code == -722 || nm.Code == -949)  // DTN_CLOSEUP || DTN_?
                        {
                            SetToDateTimeValue();
                            OnValueChanged(EventArgs.Empty);
                        }
                    }
                    break;

                case Win32.NativeMethods.WM_ERASEBKGND:
                    var g = Graphics.FromHdc(m.WParam);
                    g.FillRectangle(_backgroundBrush, ClientRectangle);
                    g.Dispose();
                    return;
            }

            base.WndProc(ref m);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR
        {
            public IntPtr HwndFrom;
            public int IdFrom;
            public int Code;
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnKeyDown"/>.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                this.Value = null;

            base.OnKeyUp(e);
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;

                _backgroundBrush = new SolidBrush(value);

                Invalidate();
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (_isNull)
            {
                bool enableEditing = false;

                if (IsInputKey(e.KeyCode))
                {
                    enableEditing = true;
                }
                else
                {
                    switch (e.KeyCode)
                    {
                        case Keys.D0:
                        case Keys.D1:
                        case Keys.D2:
                        case Keys.D3:
                        case Keys.D4:
                        case Keys.D5:
                        case Keys.D6:
                        case Keys.D7:
                        case Keys.D8:
                        case Keys.D9:
                        case Keys.NumPad0:
                        case Keys.NumPad1:
                        case Keys.NumPad2:
                        case Keys.NumPad3:
                        case Keys.NumPad4:
                        case Keys.NumPad5:
                        case Keys.NumPad6:
                        case Keys.NumPad7:
                        case Keys.NumPad8:
                        case Keys.NumPad9:
                        case Keys.Up:
                        case Keys.Down:
                        case Keys.Left:
                        case Keys.Right:
                        case Keys.Space:
                            enableEditing = true;
                            break;
                    }
                }

                if (enableEditing)
                    SetToDateTimeValue();
            }

            base.OnPreviewKeyDown(e);
        }
    }
}
