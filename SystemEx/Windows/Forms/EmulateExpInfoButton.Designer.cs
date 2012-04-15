using System.Text;
using System.Collections.Generic;
using System;

namespace SystemEx.Windows.Forms
{
    partial class EmulateExpInfoButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.pictureBox = new System.Windows.Forms.PictureBox ( );
            this.label = new System.Windows.Forms.Label ( );
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel ( );
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox ) ).BeginInit ( );
            this.tableLayoutPanel.SuspendLayout ( );
            this.SuspendLayout ( );
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point ( 0, 0 );
            this.pictureBox.Margin = new System.Windows.Forms.Padding ( 0 );
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size ( 20, 20 );
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label.Location = new System.Drawing.Point ( 20, 0 );
            this.label.Margin = new System.Windows.Forms.Padding ( 0 );
            this.label.MaximumSize = new System.Drawing.Size ( 130, 0 );
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding ( 0, 2, 0, 0 );
            this.label.Size = new System.Drawing.Size ( 66, 20 );
            this.label.TabIndex = 3;
            this.label.Text = "label";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Absolute, 20F ) );
            this.tableLayoutPanel.ColumnStyles.Add ( new System.Windows.Forms.ColumnStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel.Controls.Add ( this.pictureBox, 0, 0 );
            this.tableLayoutPanel.Controls.Add ( this.label, 1, 0 );
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point ( 4, 0 );
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding ( 0 );
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add ( new System.Windows.Forms.RowStyle ( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel.Size = new System.Drawing.Size ( 86, 20 );
            this.tableLayoutPanel.TabIndex = 4;
            // 
            // EmulateExpInfoButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add ( this.tableLayoutPanel );
            this.Margin = new System.Windows.Forms.Padding ( 0 );
            this.MinimumSize = new System.Drawing.Size ( 0, 20 );
            this.Name = "EmulateExpInfoButton";
            this.Padding = new System.Windows.Forms.Padding ( 4, 0, 0, 0 );
            this.Size = new System.Drawing.Size ( 90, 20 );
            this.Leave += new System.EventHandler ( this.EmulateExpInfoButton_Leave );
            this.Enter += new System.EventHandler ( this.EmulateExpInfoButton_Enter );
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox ) ).EndInit ( );
            this.tableLayoutPanel.ResumeLayout ( false );
            this.tableLayoutPanel.PerformLayout ( );
            this.ResumeLayout ( false );
            this.PerformLayout ( );

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;

    }
}
