using System.Text;
using System.Collections.Generic;
using System;

namespace SystemEx.Windows.Forms
{
    partial class MessageForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.formFlowFooter = new FormFlowFooter();
            this.messageFormContent1 = new MessageFormContent();
            this.SuspendLayout();
            // 
            // formFlowFooter
            // 
            this.formFlowFooter.Location = new System.Drawing.Point(0, 99);
            this.formFlowFooter.Name = "formFlowFooter";
            this.formFlowFooter.Size = new System.Drawing.Size(354, 16);
            this.formFlowFooter.TabIndex = 1;
            // 
            // messageFormContent1
            // 
            this.messageFormContent1.BackColor = System.Drawing.SystemColors.Window;
            this.messageFormContent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageFormContent1.Icon = null;
            this.messageFormContent1.Location = new System.Drawing.Point(0, 0);
            this.messageFormContent1.Name = "messageFormContent1";
            this.messageFormContent1.Size = new System.Drawing.Size(354, 99);
            this.messageFormContent1.TabIndex = 0;
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 115);
            this.Controls.Add(this.messageFormContent1);
            this.Controls.Add(this.formFlowFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 140);
            this.Name = "MessageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageForm";
            this.Showing += new System.EventHandler(this.MessageForm_Showing);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageForm_FormClosing);
            this.Shown += new System.EventHandler(this.MessageForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FormFlowFooter formFlowFooter;
        private MessageFormContent messageFormContent1;
    }
}