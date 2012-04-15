using System.Text;
using System.Collections.Generic;
using System;

namespace SystemEx.Windows.Forms
{
    partial class DateTimePickerEx
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._timePicker = new TimePicker();
            this._dateTimePicker = new DateTimePicker();
            this.SuspendLayout();
            // 
            // _timePicker
            // 
            this._timePicker.FormattingEnabled = true;
            this._timePicker.Location = new System.Drawing.Point(180, 0);
            this._timePicker.Name = "_timePicker";
            this._timePicker.Size = new System.Drawing.Size(70, 21);
            this._timePicker.TabIndex = 1;
            this._timePicker.SelectedTimeChanged += new System.EventHandler(this._timePicker_SelectedTimeChanged);
            this._timePicker.SizeChanged += new System.EventHandler(this.timePicker_SizeChanged);
            // 
            // _dateTimePicker
            // 
            this._dateTimePicker.Location = new System.Drawing.Point(0, 0);
            this._dateTimePicker.MinimumSize = new System.Drawing.Size(0, 21);
            this._dateTimePicker.Name = "_dateTimePicker";
            this._dateTimePicker.Size = new System.Drawing.Size(174, 21);
            this._dateTimePicker.TabIndex = 0;
            this._dateTimePicker.ValueChanged += new System.EventHandler(this._dateTimePicker_ValueChanged);
            // 
            // DateTimePickerEx
            // 
            this.Controls.Add(this._timePicker);
            this.Controls.Add(this._dateTimePicker);
            this.Size = new System.Drawing.Size(250, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private DateTimePicker _dateTimePicker;
        private TimePicker _timePicker;
    }
}
