//
// RichTextBoxExtended
// Written to support a word processing style toolbar by Richard Parsons
//		(http://www.codeproject.com/script/profile/whos_who.asp?id=419005)
// Extended to support form designer persistance and data binding by Declan Brennan
//		(http://www.codeproject.com/script/profile/whos_who.asp?id=495690)
//

using System.Text;
using System.Collections.Generic;
using System;

namespace SystemEx.Windows.Forms
{
	/// <summary>
	/// Summary description for RichTextBoxContentEditor.
	/// </summary>
	public class RichTextBoxExtendedRtfEditorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button OKButton;
		private RichTextBoxExtended richTextBoxExtended1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RichTextBoxExtendedRtfEditorForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.OKButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.richTextBoxExtended1 = new RichTextBoxExtended();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.OKButton);
			this.panel1.Controls.Add(this.cancelButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.DockPadding.All = 10;
			this.panel1.Location = new System.Drawing.Point(0, 264);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(440, 48);
			this.panel1.TabIndex = 1;
			// 
			// OKButton
			// 
			this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.OKButton.Location = new System.Drawing.Point(355, 10);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(75, 28);
			this.OKButton.TabIndex = 1;
			this.OKButton.Text = "OK";
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Dock = System.Windows.Forms.DockStyle.Left;
			this.cancelButton.Location = new System.Drawing.Point(10, 10);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 28);
			this.cancelButton.TabIndex = 0;
			this.cancelButton.Text = "Cancel";
			// 
			// richTextBoxExtended1
			// 
			this.richTextBoxExtended1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxExtended1.EditorBackColor = System.Drawing.SystemColors.Window;
			this.richTextBoxExtended1.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxExtended1.Name = "richTextBoxExtended1";
			this.richTextBoxExtended1.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft S" +
				"ans Serif;}}\r\n{\\*\\generator Riched20 5.40.11.2210;}\\viewkind4\\uc1\\pard\\f0\\fs16\\p" +
				"ar\r\n}\r\n";
			this.richTextBoxExtended1.ShowOpen = false;
			this.richTextBoxExtended1.ShowSave = false;
			this.richTextBoxExtended1.ShowStamp = false;
			this.richTextBoxExtended1.Size = new System.Drawing.Size(440, 264);
			this.richTextBoxExtended1.TabIndex = 2;
			// 
			// RichTextBoxExtendedRtfEditorForm
			// 
			this.AcceptButton = this.OKButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(440, 312);
			this.Controls.Add(this.richTextBoxExtended1);
			this.Controls.Add(this.panel1);
			this.MinimumSize = new System.Drawing.Size(350, 220);
			this.Name = "RichTextBoxExtendedRtfEditorForm";
			this.Text = "Rich Text";
			this.Load += new System.EventHandler(this.RichTextBoxExtendedRtfEditorForm_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void RichTextBoxExtendedRtfEditorForm_Load(object sender, System.EventArgs e)
		{
			this.richTextBoxExtended1.Rtf= mValue;
		}

		string mValue;

		public string Value
		{
			get
			{	return this.richTextBoxExtended1.Rtf;
			}
			set
			{	// Delay the setting of Rtf until the form is fully loaded
				// Otherwise the RichTextBox looses its formatting.
				mValue= value;
			}
		}
	}
}
