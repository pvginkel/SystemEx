using System.Text;
using System.Collections.Generic;
using System;

namespace SystemEx.Windows.Forms
{
    partial class CalculatorControl
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
            this._clientArea = new System.Windows.Forms.TableLayoutPanel();
            this._number0Button = new System.Windows.Forms.Button();
            this._resultButton = new System.Windows.Forms.Button();
            this._number2Button = new System.Windows.Forms.Button();
            this._number1Button = new System.Windows.Forms.Button();
            this._oneDividedByButton = new System.Windows.Forms.Button();
            this._subtractButton = new System.Windows.Forms.Button();
            this._number3Button = new System.Windows.Forms.Button();
            this._multiplyButton = new System.Windows.Forms.Button();
            this._number6Button = new System.Windows.Forms.Button();
            this._number5Button = new System.Windows.Forms.Button();
            this._number4Button = new System.Windows.Forms.Button();
            this._percentageButton = new System.Windows.Forms.Button();
            this._divideButton = new System.Windows.Forms.Button();
            this._number9Button = new System.Windows.Forms.Button();
            this._number8Button = new System.Windows.Forms.Button();
            this._number7Button = new System.Windows.Forms.Button();
            this._rootButton = new System.Windows.Forms.Button();
            this._negativeButton = new System.Windows.Forms.Button();
            this._clearButton = new System.Windows.Forms.Button();
            this._clearEntryButton = new System.Windows.Forms.Button();
            this._backspaceButton = new System.Windows.Forms.Button();
            this._memorySubtractButton = new System.Windows.Forms.Button();
            this._memoryAddButton = new System.Windows.Forms.Button();
            this._memorySetButton = new System.Windows.Forms.Button();
            this._memoryRecallButton = new System.Windows.Forms.Button();
            this._memoryClearButton = new System.Windows.Forms.Button();
            this._decimalButton = new System.Windows.Forms.Button();
            this._addButton = new System.Windows.Forms.Button();
            this._displayContainerPanel = new ThemedPanel();
            this._displayPanel = new System.Windows.Forms.TableLayoutPanel();
            this._lastResultLabel = new System.Windows.Forms.Label();
            this._resultLabel = new System.Windows.Forms.Label();
            this._memoryStatusLabel = new System.Windows.Forms.Label();
            this._clientArea.SuspendLayout();
            this._displayContainerPanel.SuspendLayout();
            this._displayPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _clientArea
            // 
            this._clientArea.ColumnCount = 5;
            this._clientArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._clientArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._clientArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._clientArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._clientArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._clientArea.Controls.Add(this._number0Button, 0, 6);
            this._clientArea.Controls.Add(this._resultButton, 4, 5);
            this._clientArea.Controls.Add(this._number2Button, 1, 5);
            this._clientArea.Controls.Add(this._number1Button, 0, 5);
            this._clientArea.Controls.Add(this._oneDividedByButton, 4, 4);
            this._clientArea.Controls.Add(this._subtractButton, 3, 5);
            this._clientArea.Controls.Add(this._number3Button, 2, 5);
            this._clientArea.Controls.Add(this._multiplyButton, 3, 4);
            this._clientArea.Controls.Add(this._number6Button, 2, 4);
            this._clientArea.Controls.Add(this._number5Button, 1, 4);
            this._clientArea.Controls.Add(this._number4Button, 0, 4);
            this._clientArea.Controls.Add(this._percentageButton, 4, 3);
            this._clientArea.Controls.Add(this._divideButton, 3, 3);
            this._clientArea.Controls.Add(this._number9Button, 2, 3);
            this._clientArea.Controls.Add(this._number8Button, 1, 3);
            this._clientArea.Controls.Add(this._number7Button, 0, 3);
            this._clientArea.Controls.Add(this._rootButton, 4, 2);
            this._clientArea.Controls.Add(this._negativeButton, 3, 2);
            this._clientArea.Controls.Add(this._clearButton, 2, 2);
            this._clientArea.Controls.Add(this._clearEntryButton, 1, 2);
            this._clientArea.Controls.Add(this._backspaceButton, 0, 2);
            this._clientArea.Controls.Add(this._memorySubtractButton, 4, 1);
            this._clientArea.Controls.Add(this._memoryAddButton, 3, 1);
            this._clientArea.Controls.Add(this._memorySetButton, 2, 1);
            this._clientArea.Controls.Add(this._memoryRecallButton, 1, 1);
            this._clientArea.Controls.Add(this._memoryClearButton, 0, 1);
            this._clientArea.Controls.Add(this._displayContainerPanel, 0, 0);
            this._clientArea.Controls.Add(this._decimalButton, 2, 6);
            this._clientArea.Controls.Add(this._addButton, 3, 6);
            this._clientArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clientArea.Location = new System.Drawing.Point(3, 3);
            this._clientArea.Name = "_clientArea";
            this._clientArea.RowCount = 7;
            this._clientArea.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._clientArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this._clientArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this._clientArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this._clientArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this._clientArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this._clientArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this._clientArea.Size = new System.Drawing.Size(274, 269);
            this._clientArea.TabIndex = 0;
            // 
            // _number0Button
            // 
            this._clientArea.SetColumnSpan(this._number0Button, 2);
            this._number0Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this._number0Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._number0Button.Location = new System.Drawing.Point(3, 234);
            this._number0Button.Name = "_number0Button";
            this._number0Button.Size = new System.Drawing.Size(102, 32);
            this._number0Button.TabIndex = 25;
            this._number0Button.Text = "0";
            this._number0Button.UseVisualStyleBackColor = true;
            // 
            // _resultButton
            // 
            this._resultButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._resultButton.Location = new System.Drawing.Point(219, 200);
            this._resultButton.Name = "_resultButton";
            this._clientArea.SetRowSpan(this._resultButton, 2);
            this._resultButton.Size = new System.Drawing.Size(52, 66);
            this._resultButton.TabIndex = 24;
            this._resultButton.Text = "=";
            this._resultButton.UseVisualStyleBackColor = true;
            // 
            // _number2Button
            // 
            this._number2Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this._number2Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._number2Button.Location = new System.Drawing.Point(57, 200);
            this._number2Button.Name = "_number2Button";
            this._number2Button.Size = new System.Drawing.Size(48, 28);
            this._number2Button.TabIndex = 21;
            this._number2Button.Text = "2";
            this._number2Button.UseVisualStyleBackColor = true;
            // 
            // _number1Button
            // 
            this._number1Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this._number1Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._number1Button.Location = new System.Drawing.Point(3, 200);
            this._number1Button.Name = "_number1Button";
            this._number1Button.Size = new System.Drawing.Size(48, 28);
            this._number1Button.TabIndex = 20;
            this._number1Button.Text = "1";
            this._number1Button.UseVisualStyleBackColor = true;
            // 
            // _oneDividedByButton
            // 
            this._oneDividedByButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._oneDividedByButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._oneDividedByButton.Location = new System.Drawing.Point(219, 166);
            this._oneDividedByButton.Name = "_oneDividedByButton";
            this._oneDividedByButton.Size = new System.Drawing.Size(52, 28);
            this._oneDividedByButton.TabIndex = 19;
            this._oneDividedByButton.Text = "1/x";
            this._oneDividedByButton.UseVisualStyleBackColor = true;
            // 
            // _subtractButton
            // 
            this._subtractButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._subtractButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._subtractButton.Location = new System.Drawing.Point(165, 200);
            this._subtractButton.Name = "_subtractButton";
            this._subtractButton.Size = new System.Drawing.Size(48, 28);
            this._subtractButton.TabIndex = 23;
            this._subtractButton.Text = "-";
            this._subtractButton.UseVisualStyleBackColor = true;
            // 
            // _number3Button
            // 
            this._number3Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this._number3Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._number3Button.Location = new System.Drawing.Point(111, 200);
            this._number3Button.Name = "_number3Button";
            this._number3Button.Size = new System.Drawing.Size(48, 28);
            this._number3Button.TabIndex = 22;
            this._number3Button.Text = "3";
            this._number3Button.UseVisualStyleBackColor = true;
            // 
            // _multiplyButton
            // 
            this._multiplyButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._multiplyButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._multiplyButton.Location = new System.Drawing.Point(165, 166);
            this._multiplyButton.Name = "_multiplyButton";
            this._multiplyButton.Size = new System.Drawing.Size(48, 28);
            this._multiplyButton.TabIndex = 18;
            this._multiplyButton.Text = "*";
            this._multiplyButton.UseVisualStyleBackColor = true;
            // 
            // _number6Button
            // 
            this._number6Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this._number6Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._number6Button.Location = new System.Drawing.Point(111, 166);
            this._number6Button.Name = "_number6Button";
            this._number6Button.Size = new System.Drawing.Size(48, 28);
            this._number6Button.TabIndex = 17;
            this._number6Button.Text = "6";
            this._number6Button.UseVisualStyleBackColor = true;
            // 
            // _number5Button
            // 
            this._number5Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this._number5Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._number5Button.Location = new System.Drawing.Point(57, 166);
            this._number5Button.Name = "_number5Button";
            this._number5Button.Size = new System.Drawing.Size(48, 28);
            this._number5Button.TabIndex = 16;
            this._number5Button.Text = "5";
            this._number5Button.UseVisualStyleBackColor = true;
            // 
            // _number4Button
            // 
            this._number4Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this._number4Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._number4Button.Location = new System.Drawing.Point(3, 166);
            this._number4Button.Name = "_number4Button";
            this._number4Button.Size = new System.Drawing.Size(48, 28);
            this._number4Button.TabIndex = 15;
            this._number4Button.Text = "4";
            this._number4Button.UseVisualStyleBackColor = true;
            // 
            // _percentageButton
            // 
            this._percentageButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._percentageButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._percentageButton.Location = new System.Drawing.Point(219, 132);
            this._percentageButton.Name = "_percentageButton";
            this._percentageButton.Size = new System.Drawing.Size(52, 28);
            this._percentageButton.TabIndex = 14;
            this._percentageButton.Text = "%";
            this._percentageButton.UseVisualStyleBackColor = true;
            // 
            // _divideButton
            // 
            this._divideButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._divideButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._divideButton.Location = new System.Drawing.Point(165, 132);
            this._divideButton.Name = "_divideButton";
            this._divideButton.Size = new System.Drawing.Size(48, 28);
            this._divideButton.TabIndex = 13;
            this._divideButton.Text = "/";
            this._divideButton.UseVisualStyleBackColor = true;
            // 
            // _number9Button
            // 
            this._number9Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this._number9Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._number9Button.Location = new System.Drawing.Point(111, 132);
            this._number9Button.Name = "_number9Button";
            this._number9Button.Size = new System.Drawing.Size(48, 28);
            this._number9Button.TabIndex = 12;
            this._number9Button.Text = "9";
            this._number9Button.UseVisualStyleBackColor = true;
            // 
            // _number8Button
            // 
            this._number8Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this._number8Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._number8Button.Location = new System.Drawing.Point(57, 132);
            this._number8Button.Name = "_number8Button";
            this._number8Button.Size = new System.Drawing.Size(48, 28);
            this._number8Button.TabIndex = 11;
            this._number8Button.Text = "8";
            this._number8Button.UseVisualStyleBackColor = true;
            // 
            // _number7Button
            // 
            this._number7Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this._number7Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._number7Button.Location = new System.Drawing.Point(3, 132);
            this._number7Button.Name = "_number7Button";
            this._number7Button.Size = new System.Drawing.Size(48, 28);
            this._number7Button.TabIndex = 10;
            this._number7Button.Text = "7";
            this._number7Button.UseVisualStyleBackColor = true;
            // 
            // _rootButton
            // 
            this._rootButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._rootButton.Location = new System.Drawing.Point(219, 98);
            this._rootButton.Name = "_rootButton";
            this._rootButton.Size = new System.Drawing.Size(52, 28);
            this._rootButton.TabIndex = 9;
            this._rootButton.Text = "√";
            this._rootButton.UseVisualStyleBackColor = true;
            // 
            // _negativeButton
            // 
            this._negativeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._negativeButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._negativeButton.Location = new System.Drawing.Point(165, 98);
            this._negativeButton.Name = "_negativeButton";
            this._negativeButton.Size = new System.Drawing.Size(48, 28);
            this._negativeButton.TabIndex = 8;
            this._negativeButton.Text = "±";
            this._negativeButton.UseVisualStyleBackColor = true;
            // 
            // _clearButton
            // 
            this._clearButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clearButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._clearButton.Location = new System.Drawing.Point(111, 98);
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(48, 28);
            this._clearButton.TabIndex = 7;
            this._clearButton.Text = "C";
            this._clearButton.UseVisualStyleBackColor = true;
            // 
            // _clearEntryButton
            // 
            this._clearEntryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clearEntryButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._clearEntryButton.Location = new System.Drawing.Point(57, 98);
            this._clearEntryButton.Name = "_clearEntryButton";
            this._clearEntryButton.Size = new System.Drawing.Size(48, 28);
            this._clearEntryButton.TabIndex = 6;
            this._clearEntryButton.Text = "CE";
            this._clearEntryButton.UseVisualStyleBackColor = true;
            // 
            // _backspaceButton
            // 
            this._backspaceButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._backspaceButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._backspaceButton.Location = new System.Drawing.Point(3, 98);
            this._backspaceButton.Name = "_backspaceButton";
            this._backspaceButton.Size = new System.Drawing.Size(48, 28);
            this._backspaceButton.TabIndex = 5;
            this._backspaceButton.Text = "←";
            this._backspaceButton.UseVisualStyleBackColor = true;
            // 
            // _memorySubtractButton
            // 
            this._memorySubtractButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._memorySubtractButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._memorySubtractButton.Location = new System.Drawing.Point(219, 64);
            this._memorySubtractButton.Name = "_memorySubtractButton";
            this._memorySubtractButton.Size = new System.Drawing.Size(52, 28);
            this._memorySubtractButton.TabIndex = 4;
            this._memorySubtractButton.Text = "M-";
            this._memorySubtractButton.UseVisualStyleBackColor = true;
            // 
            // _memoryAddButton
            // 
            this._memoryAddButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._memoryAddButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._memoryAddButton.Location = new System.Drawing.Point(165, 64);
            this._memoryAddButton.Name = "_memoryAddButton";
            this._memoryAddButton.Size = new System.Drawing.Size(48, 28);
            this._memoryAddButton.TabIndex = 3;
            this._memoryAddButton.Text = "M+";
            this._memoryAddButton.UseVisualStyleBackColor = true;
            // 
            // _memorySetButton
            // 
            this._memorySetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._memorySetButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._memorySetButton.Location = new System.Drawing.Point(111, 64);
            this._memorySetButton.Name = "_memorySetButton";
            this._memorySetButton.Size = new System.Drawing.Size(48, 28);
            this._memorySetButton.TabIndex = 2;
            this._memorySetButton.Text = "MS";
            this._memorySetButton.UseVisualStyleBackColor = true;
            // 
            // _memoryRecallButton
            // 
            this._memoryRecallButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._memoryRecallButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._memoryRecallButton.Location = new System.Drawing.Point(57, 64);
            this._memoryRecallButton.Name = "_memoryRecallButton";
            this._memoryRecallButton.Size = new System.Drawing.Size(48, 28);
            this._memoryRecallButton.TabIndex = 1;
            this._memoryRecallButton.Text = "MR";
            this._memoryRecallButton.UseVisualStyleBackColor = true;
            // 
            // _memoryClearButton
            // 
            this._memoryClearButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._memoryClearButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._memoryClearButton.Location = new System.Drawing.Point(3, 64);
            this._memoryClearButton.Name = "_memoryClearButton";
            this._memoryClearButton.Size = new System.Drawing.Size(48, 28);
            this._memoryClearButton.TabIndex = 0;
            this._memoryClearButton.Text = "MC";
            this._memoryClearButton.UseVisualStyleBackColor = true;
            // 
            // _decimalButton
            // 
            this._decimalButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._decimalButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._decimalButton.Location = new System.Drawing.Point(111, 234);
            this._decimalButton.Name = "_decimalButton";
            this._decimalButton.Size = new System.Drawing.Size(48, 32);
            this._decimalButton.TabIndex = 26;
            this._decimalButton.Text = ",";
            this._decimalButton.UseVisualStyleBackColor = true;
            // 
            // _addButton
            // 
            this._addButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._addButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._addButton.Location = new System.Drawing.Point(165, 234);
            this._addButton.Name = "_addButton";
            this._addButton.Size = new System.Drawing.Size(48, 32);
            this._addButton.TabIndex = 28;
            this._addButton.Text = "+";
            this._addButton.UseVisualStyleBackColor = true;
            // 
            // _displayContainerPanel
            // 
            this._displayContainerPanel.AutoSize = true;
            this._displayContainerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._clientArea.SetColumnSpan(this._displayContainerPanel, 5);
            this._displayContainerPanel.Controls.Add(this._displayPanel);
            this._displayContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._displayContainerPanel.Location = new System.Drawing.Point(3, 3);
            this._displayContainerPanel.Name = "_displayContainerPanel";
            this._displayContainerPanel.Padding = new System.Windows.Forms.Padding(3);
            this._displayContainerPanel.Size = new System.Drawing.Size(268, 55);
            this._displayContainerPanel.TabIndex = 0;
            // 
            // _displayPanel
            // 
            this._displayPanel.AutoSize = true;
            this._displayPanel.ColumnCount = 2;
            this._displayPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this._displayPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._displayPanel.Controls.Add(this._lastResultLabel, 1, 0);
            this._displayPanel.Controls.Add(this._resultLabel, 1, 1);
            this._displayPanel.Controls.Add(this._memoryStatusLabel, 0, 1);
            this._displayPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._displayPanel.Location = new System.Drawing.Point(3, 3);
            this._displayPanel.Name = "_displayPanel";
            this._displayPanel.RowCount = 2;
            this._displayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._displayPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._displayPanel.Size = new System.Drawing.Size(260, 47);
            this._displayPanel.TabIndex = 0;
            // 
            // _lastResultLabel
            // 
            this._lastResultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lastResultLabel.AutoSize = true;
            this._lastResultLabel.Location = new System.Drawing.Point(244, 3);
            this._lastResultLabel.Margin = new System.Windows.Forms.Padding(3);
            this._lastResultLabel.Name = "_lastResultLabel";
            this._lastResultLabel.Size = new System.Drawing.Size(13, 13);
            this._lastResultLabel.TabIndex = 0;
            this._lastResultLabel.Text = "0";
            // 
            // _resultLabel
            // 
            this._resultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._resultLabel.AutoSize = true;
            this._resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._resultLabel.Location = new System.Drawing.Point(237, 22);
            this._resultLabel.Margin = new System.Windows.Forms.Padding(3);
            this._resultLabel.Name = "_resultLabel";
            this._resultLabel.Size = new System.Drawing.Size(20, 22);
            this._resultLabel.TabIndex = 1;
            this._resultLabel.Text = "0";
            // 
            // _memoryStatusLabel
            // 
            this._memoryStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._memoryStatusLabel.AutoSize = true;
            this._memoryStatusLabel.Location = new System.Drawing.Point(3, 31);
            this._memoryStatusLabel.Margin = new System.Windows.Forms.Padding(3);
            this._memoryStatusLabel.Name = "_memoryStatusLabel";
            this._memoryStatusLabel.Size = new System.Drawing.Size(16, 13);
            this._memoryStatusLabel.TabIndex = 0;
            this._memoryStatusLabel.Text = "M";
            // 
            // CalculatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._clientArea);
            this.Name = "CalculatorControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(280, 275);
            this._clientArea.ResumeLayout(false);
            this._clientArea.PerformLayout();
            this._displayContainerPanel.ResumeLayout(false);
            this._displayContainerPanel.PerformLayout();
            this._displayPanel.ResumeLayout(false);
            this._displayPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _clientArea;
        private System.Windows.Forms.Button _number0Button;
        private System.Windows.Forms.Button _resultButton;
        private System.Windows.Forms.Button _number2Button;
        private System.Windows.Forms.Button _number1Button;
        private System.Windows.Forms.Button _oneDividedByButton;
        private System.Windows.Forms.Button _subtractButton;
        private System.Windows.Forms.Button _number3Button;
        private System.Windows.Forms.Button _multiplyButton;
        private System.Windows.Forms.Button _number6Button;
        private System.Windows.Forms.Button _number5Button;
        private System.Windows.Forms.Button _number4Button;
        private System.Windows.Forms.Button _percentageButton;
        private System.Windows.Forms.Button _divideButton;
        private System.Windows.Forms.Button _number9Button;
        private System.Windows.Forms.Button _number8Button;
        private System.Windows.Forms.Button _number7Button;
        private System.Windows.Forms.Button _rootButton;
        private System.Windows.Forms.Button _negativeButton;
        private System.Windows.Forms.Button _clearButton;
        private System.Windows.Forms.Button _clearEntryButton;
        private System.Windows.Forms.Button _backspaceButton;
        private System.Windows.Forms.Button _memorySubtractButton;
        private System.Windows.Forms.Button _memoryAddButton;
        private System.Windows.Forms.Button _memorySetButton;
        private System.Windows.Forms.Button _memoryRecallButton;
        private System.Windows.Forms.Button _memoryClearButton;
        private ThemedPanel _displayContainerPanel;
        private System.Windows.Forms.TableLayoutPanel _displayPanel;
        private System.Windows.Forms.Label _lastResultLabel;
        private System.Windows.Forms.Label _resultLabel;
        private System.Windows.Forms.Label _memoryStatusLabel;
        private System.Windows.Forms.Button _decimalButton;
        private System.Windows.Forms.Button _addButton;
    }
}
