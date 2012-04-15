using System.Text;
using System.Collections.Generic;
using System;

namespace SystemEx.Windows.Forms
{
    partial class CalculatorForm
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
            this._clientArea = new System.Windows.Forms.TableLayoutPanel();
            this._calculator = new CalculatorControl();
            this._footerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._acceptButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._clientArea.SuspendLayout();
            this._footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _clientArea
            // 
            this._clientArea.BackColor = System.Drawing.SystemColors.Control;
            this._clientArea.ColumnCount = 1;
            this._clientArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._clientArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._clientArea.Controls.Add(this._calculator, 0, 0);
            this._clientArea.Controls.Add(this._footerPanel, 0, 1);
            this._clientArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clientArea.Location = new System.Drawing.Point(1, 1);
            this._clientArea.Name = "_clientArea";
            this._clientArea.RowCount = 2;
            this._clientArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._clientArea.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._clientArea.Size = new System.Drawing.Size(292, 319);
            this._clientArea.TabIndex = 0;
            // 
            // _calculator
            // 
            this._calculator.Dock = System.Windows.Forms.DockStyle.Fill;
            this._calculator.Location = new System.Drawing.Point(3, 3);
            this._calculator.Name = "_calculator";
            this._calculator.Padding = new System.Windows.Forms.Padding(3);
            this._calculator.Size = new System.Drawing.Size(286, 278);
            this._calculator.TabIndex = 0;
            // 
            // _footerPanel
            // 
            this._footerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._footerPanel.AutoSize = true;
            this._footerPanel.Controls.Add(this._acceptButton);
            this._footerPanel.Controls.Add(this._cancelButton);
            this._footerPanel.Location = new System.Drawing.Point(124, 284);
            this._footerPanel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 6);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Size = new System.Drawing.Size(162, 29);
            this._footerPanel.TabIndex = 1;
            // 
            // _acceptButton
            // 
            this._acceptButton.Location = new System.Drawing.Point(3, 3);
            this._acceptButton.Name = "_acceptButton";
            this._acceptButton.Size = new System.Drawing.Size(75, 23);
            this._acceptButton.TabIndex = 0;
            this._acceptButton.Text = "OK";
            this._acceptButton.UseVisualStyleBackColor = true;
            this._acceptButton.Click += new System.EventHandler(this._acceptButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(84, 3);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // CalculatorForm
            // 
            this.AcceptButton = this._acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(294, 321);
            this.Controls.Add(this._clientArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CalculatorForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CalculatorForm";
            this.Deactivate += new System.EventHandler(this.CalculatorForm_Deactivate);
            this.Shown += new System.EventHandler(this.CalculatorForm_Shown);
            this._clientArea.ResumeLayout(false);
            this._clientArea.PerformLayout();
            this._footerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _clientArea;
        private CalculatorControl _calculator;
        private System.Windows.Forms.FlowLayoutPanel _footerPanel;
        private System.Windows.Forms.Button _acceptButton;
        private System.Windows.Forms.Button _cancelButton;
    }
}