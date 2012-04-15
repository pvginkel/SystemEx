using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;

namespace SystemEx.Windows.Forms
{
    public class LinkLabel : Label
    {
        public LinkLabel()
        {
            this.ForeColor = Color.Blue;
            this.Cursor = Cursors.Hand;
            this.Font = new Font(Font, FontStyle.Underline);

            Click += new EventHandler(LinkLabelEx_Click);
        }

        void LinkLabelEx_Click(object sender, EventArgs e)
        {
            string uriText = null;
            Uri uri = null;

            if (this.Tag != null && (this.Tag is string || this.Tag is Uri))
            {
                if (this.Tag is Uri)
                    uri = (Uri)this.Tag;
                else if (this.Tag is string)
                    uriText = (string)this.Tag;
            }
            else
            {
                uriText = this.Text;
            }

            if (uri == null && uriText != null && uriText.Contains("://"))
                uri = new Uri(uriText);

            if (uri != null)
            {
                try
                {
                    using (var proc = new Process())
                    {
                        proc.StartInfo.FileName = uri.ToString();
                        proc.StartInfo.UseShellExecute = true;

                        proc.Start();
                    }
                }
                catch
                {
                    // Ignore all exceptions.
                }
            }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue(typeof(Color), "Blue")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DefaultValue(typeof(Cursors), "Hand")]
        public override Cursor Cursor
        {
            get { return base.Cursor; }
            set { base.Cursor = value; }
        }
    }
}
