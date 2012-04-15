using System.Text;
using System.Collections.Generic;
using System;

namespace SystemEx.Windows.Forms
{
    partial class EmulateTaskDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose ( );
            }
            base.Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.components = new System.ComponentModel.Container ( );
            this.tableLayoutPanelMainArea = new System.Windows.Forms.TableLayoutPanel ( );
            this.flowLayoutPanelMainAreaControls = new System.Windows.Forms.FlowLayoutPanel ( );
            this.pictureBoxMainAreaIcon = new System.Windows.Forms.PictureBox ( );
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel ( );
            this.tableLayoutPanelSubArea = new System.Windows.Forms.TableLayoutPanel ( );
            this.flowLayoutPanelSubAreaButtons = new System.Windows.Forms.FlowLayoutPanel ( );
            this.flowLayoutPanelSubAreaControls = new System.Windows.Forms.FlowLayoutPanel ( );
            this.tableLayoutPanelFooterArea = new System.Windows.Forms.TableLayoutPanel ( );
            this.pictureBoxFooterAreaIcon = new System.Windows.Forms.PictureBox ( );
            this.flowLayoutPanelFooterAreaText = new System.Windows.Forms.FlowLayoutPanel ( );
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel ( );
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel ( );
            this.tableLayoutPanelFooterExpandedInformationArea = new System.Windows.Forms.TableLayoutPanel ( );
            this.flowLayoutPanelFooterExpandedInformationText = new System.Windows.Forms.FlowLayoutPanel ( );
            this.timer = new System.Windows.Forms.Timer ( this.components );
            this.tableLayoutPanelMainArea.SuspendLayout ( );
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBoxMainAreaIcon ) ).BeginInit ( );
            this.tableLayoutPanelSubArea.SuspendLayout ( );
            this.tableLayoutPanelFooterArea.SuspendLayout ( );
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBoxFooterAreaIcon ) ).BeginInit ( );
            this.tableLayoutPanelFooterExpandedInformationArea.SuspendLayout ( );
            this.SuspendLayout ( );
            // 
            // tableLayoutPanelMainArea
            // 
            this.tableLayoutPanelMainArea.AutoSize = true;
            this.tableLayoutPanelMainArea.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelMainArea.ColumnCount = 2;
            this.tableLayoutPanelMainArea.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Absolute, 35F ) );
            this.tableLayoutPanelMainArea.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanelMainArea.Controls.Add ( this.flowLayoutPanelMainAreaControls, 1, 0 );
            this.tableLayoutPanelMainArea.Controls.Add ( this.pictureBoxMainAreaIcon, 0, 0 );
            this.tableLayoutPanelMainArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelMainArea.Location = new System.Drawing.Point ( 0, 0 );
            this.tableLayoutPanelMainArea.Margin = new System.Windows.Forms.Padding ( 0 );
            this.tableLayoutPanelMainArea.MinimumSize = new System.Drawing.Size ( 0, 50 );
            this.tableLayoutPanelMainArea.Name = "tableLayoutPanelMainArea";
            this.tableLayoutPanelMainArea.Padding = new System.Windows.Forms.Padding ( 15 );
            this.tableLayoutPanelMainArea.RowCount = 1;
            this.tableLayoutPanelMainArea.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanelMainArea.Size = new System.Drawing.Size ( 394, 62 );
            this.tableLayoutPanelMainArea.TabIndex = 0;
            // 
            // flowLayoutPanelMainAreaControls
            // 
            this.flowLayoutPanelMainAreaControls.AutoSize = true;
            this.flowLayoutPanelMainAreaControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelMainAreaControls.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelMainAreaControls.Location = new System.Drawing.Point ( 50, 15 );
            this.flowLayoutPanelMainAreaControls.Margin = new System.Windows.Forms.Padding ( 0 );
            this.flowLayoutPanelMainAreaControls.Name = "flowLayoutPanelMainAreaControls";
            this.flowLayoutPanelMainAreaControls.Size = new System.Drawing.Size ( 329, 32 );
            this.flowLayoutPanelMainAreaControls.TabIndex = 0;
            // 
            // pictureBoxMainAreaIcon
            // 
            this.pictureBoxMainAreaIcon.Location = new System.Drawing.Point ( 15, 15 );
            this.pictureBoxMainAreaIcon.Margin = new System.Windows.Forms.Padding ( 0 );
            this.pictureBoxMainAreaIcon.Name = "pictureBoxMainAreaIcon";
            this.pictureBoxMainAreaIcon.Size = new System.Drawing.Size ( 32, 32 );
            this.pictureBoxMainAreaIcon.TabIndex = 1;
            this.pictureBoxMainAreaIcon.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.LightGray;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point ( 0, 62 );
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding ( 0 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size ( 394, 2 );
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanelSubArea
            // 
            this.tableLayoutPanelSubArea.AutoSize = true;
            this.tableLayoutPanelSubArea.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanelSubArea.ColumnCount = 2;
            this.tableLayoutPanelSubArea.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Absolute, 150F ) );
            this.tableLayoutPanelSubArea.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanelSubArea.Controls.Add ( this.flowLayoutPanelSubAreaButtons, 1, 0 );
            this.tableLayoutPanelSubArea.Controls.Add ( this.flowLayoutPanelSubAreaControls, 0, 0 );
            this.tableLayoutPanelSubArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelSubArea.Location = new System.Drawing.Point ( 0, 64 );
            this.tableLayoutPanelSubArea.Margin = new System.Windows.Forms.Padding ( 0 );
            this.tableLayoutPanelSubArea.MinimumSize = new System.Drawing.Size ( 0, 29 );
            this.tableLayoutPanelSubArea.Name = "tableLayoutPanelSubArea";
            this.tableLayoutPanelSubArea.Padding = new System.Windows.Forms.Padding ( 8, 7, 15, 0 );
            this.tableLayoutPanelSubArea.RowCount = 1;
            this.tableLayoutPanelSubArea.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanelSubArea.Size = new System.Drawing.Size ( 394, 29 );
            this.tableLayoutPanelSubArea.TabIndex = 1;
            // 
            // flowLayoutPanelSubAreaButtons
            // 
            this.flowLayoutPanelSubAreaButtons.AutoSize = true;
            this.flowLayoutPanelSubAreaButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelSubAreaButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelSubAreaButtons.Location = new System.Drawing.Point ( 158, 7 );
            this.flowLayoutPanelSubAreaButtons.Margin = new System.Windows.Forms.Padding ( 0 );
            this.flowLayoutPanelSubAreaButtons.Name = "flowLayoutPanelSubAreaButtons";
            this.flowLayoutPanelSubAreaButtons.Size = new System.Drawing.Size ( 221, 22 );
            this.flowLayoutPanelSubAreaButtons.TabIndex = 1;
            this.flowLayoutPanelSubAreaButtons.WrapContents = false;
            // 
            // flowLayoutPanelSubAreaControls
            // 
            this.flowLayoutPanelSubAreaControls.AutoSize = true;
            this.flowLayoutPanelSubAreaControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelSubAreaControls.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelSubAreaControls.Location = new System.Drawing.Point ( 8, 7 );
            this.flowLayoutPanelSubAreaControls.Margin = new System.Windows.Forms.Padding ( 0 );
            this.flowLayoutPanelSubAreaControls.Name = "flowLayoutPanelSubAreaControls";
            this.flowLayoutPanelSubAreaControls.Size = new System.Drawing.Size ( 150, 22 );
            this.flowLayoutPanelSubAreaControls.TabIndex = 0;
            // 
            // tableLayoutPanelFooterArea
            // 
            this.tableLayoutPanelFooterArea.AutoSize = true;
            this.tableLayoutPanelFooterArea.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanelFooterArea.ColumnCount = 2;
            this.tableLayoutPanelFooterArea.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Absolute, 21F ) );
            this.tableLayoutPanelFooterArea.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanelFooterArea.Controls.Add ( this.pictureBoxFooterAreaIcon, 0, 0 );
            this.tableLayoutPanelFooterArea.Controls.Add ( this.flowLayoutPanelFooterAreaText, 1, 0 );
            this.tableLayoutPanelFooterArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelFooterArea.Location = new System.Drawing.Point ( 0, 94 );
            this.tableLayoutPanelFooterArea.Margin = new System.Windows.Forms.Padding ( 0 );
            this.tableLayoutPanelFooterArea.Name = "tableLayoutPanelFooterArea";
            this.tableLayoutPanelFooterArea.Padding = new System.Windows.Forms.Padding ( 15, 5, 15, 5 );
            this.tableLayoutPanelFooterArea.RowCount = 1;
            this.tableLayoutPanelFooterArea.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanelFooterArea.Size = new System.Drawing.Size ( 394, 26 );
            this.tableLayoutPanelFooterArea.TabIndex = 2;
            // 
            // pictureBoxFooterAreaIcon
            // 
            this.pictureBoxFooterAreaIcon.Location = new System.Drawing.Point ( 15, 5 );
            this.pictureBoxFooterAreaIcon.Margin = new System.Windows.Forms.Padding ( 0 );
            this.pictureBoxFooterAreaIcon.Name = "pictureBoxFooterAreaIcon";
            this.pictureBoxFooterAreaIcon.Size = new System.Drawing.Size ( 16, 16 );
            this.pictureBoxFooterAreaIcon.TabIndex = 0;
            this.pictureBoxFooterAreaIcon.TabStop = false;
            // 
            // flowLayoutPanelFooterAreaText
            // 
            this.flowLayoutPanelFooterAreaText.AutoSize = true;
            this.flowLayoutPanelFooterAreaText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelFooterAreaText.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelFooterAreaText.Location = new System.Drawing.Point ( 36, 5 );
            this.flowLayoutPanelFooterAreaText.Margin = new System.Windows.Forms.Padding ( 0 );
            this.flowLayoutPanelFooterAreaText.Name = "flowLayoutPanelFooterAreaText";
            this.flowLayoutPanelFooterAreaText.Size = new System.Drawing.Size ( 343, 16 );
            this.flowLayoutPanelFooterAreaText.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point ( 0, 93 );
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding ( 0 );
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel2.Size = new System.Drawing.Size ( 394, 1 );
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point ( 0, 120 );
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding ( 0 );
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 50F ) );
            this.tableLayoutPanel3.Size = new System.Drawing.Size ( 394, 1 );
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // tableLayoutPanelFooterExpandedInformationArea
            // 
            this.tableLayoutPanelFooterExpandedInformationArea.AutoSize = true;
            this.tableLayoutPanelFooterExpandedInformationArea.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanelFooterExpandedInformationArea.ColumnCount = 1;
            this.tableLayoutPanelFooterExpandedInformationArea.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanelFooterExpandedInformationArea.Controls.Add ( this.flowLayoutPanelFooterExpandedInformationText, 0, 0 );
            this.tableLayoutPanelFooterExpandedInformationArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelFooterExpandedInformationArea.Location = new System.Drawing.Point ( 0, 121 );
            this.tableLayoutPanelFooterExpandedInformationArea.Margin = new System.Windows.Forms.Padding ( 0 );
            this.tableLayoutPanelFooterExpandedInformationArea.Name = "tableLayoutPanelFooterExpandedInformationArea";
            this.tableLayoutPanelFooterExpandedInformationArea.Padding = new System.Windows.Forms.Padding ( 15, 3, 15, 3 );
            this.tableLayoutPanelFooterExpandedInformationArea.RowCount = 1;
            this.tableLayoutPanelFooterExpandedInformationArea.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanelFooterExpandedInformationArea.Size = new System.Drawing.Size ( 394, 6 );
            this.tableLayoutPanelFooterExpandedInformationArea.TabIndex = 3;
            // 
            // flowLayoutPanelFooterExpandedInformationText
            // 
            this.flowLayoutPanelFooterExpandedInformationText.AutoSize = true;
            this.flowLayoutPanelFooterExpandedInformationText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelFooterExpandedInformationText.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelFooterExpandedInformationText.Location = new System.Drawing.Point ( 15, 3 );
            this.flowLayoutPanelFooterExpandedInformationText.Margin = new System.Windows.Forms.Padding ( 0 );
            this.flowLayoutPanelFooterExpandedInformationText.Name = "flowLayoutPanelFooterExpandedInformationText";
            this.flowLayoutPanelFooterExpandedInformationText.Size = new System.Drawing.Size ( 364, 1 );
            this.flowLayoutPanelFooterExpandedInformationText.TabIndex = 0;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler ( this.timer_Tick );
            // 
            // EmulateTaskDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size ( 394, 174 );
            this.Controls.Add ( this.tableLayoutPanelFooterExpandedInformationArea );
            this.Controls.Add ( this.tableLayoutPanel3 );
            this.Controls.Add ( this.tableLayoutPanelFooterArea );
            this.Controls.Add ( this.tableLayoutPanel2 );
            this.Controls.Add ( this.tableLayoutPanelSubArea );
            this.Controls.Add ( this.tableLayoutPanel1 );
            this.Controls.Add ( this.tableLayoutPanelMainArea );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "EmulateTaskDialog";
            this.ShowInTaskbar = false;
            this.Text = "EmulateTaskDialog";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler ( this.EmulateTaskDialog_KeyDown );
            this.tableLayoutPanelMainArea.ResumeLayout ( false );
            this.tableLayoutPanelMainArea.PerformLayout ( );
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBoxMainAreaIcon ) ).EndInit ( );
            this.tableLayoutPanelSubArea.ResumeLayout ( false );
            this.tableLayoutPanelSubArea.PerformLayout ( );
            this.tableLayoutPanelFooterArea.ResumeLayout ( false );
            this.tableLayoutPanelFooterArea.PerformLayout ( );
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBoxFooterAreaIcon ) ).EndInit ( );
            this.tableLayoutPanelFooterExpandedInformationArea.ResumeLayout ( false );
            this.tableLayoutPanelFooterExpandedInformationArea.PerformLayout ( );
            this.ResumeLayout ( false );
            this.PerformLayout ( );

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMainArea;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMainAreaControls;
        private System.Windows.Forms.PictureBox pictureBoxMainAreaIcon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSubArea;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSubAreaButtons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSubAreaControls;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFooterArea;
        private System.Windows.Forms.PictureBox pictureBoxFooterAreaIcon;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFooterAreaText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFooterExpandedInformationArea;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFooterExpandedInformationText;
        private System.Windows.Forms.Timer timer;








    }
}