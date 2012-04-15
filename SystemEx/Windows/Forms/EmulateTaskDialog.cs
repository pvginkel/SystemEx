/*
 * Design Notes:-
 * --------------
 * TableLayoutPanels are the containers to hold the sub sections
 * (FlowLayoutPanels), while the FlowLayoutPanels holds and performs the layout
 * of the controls:
 * 
 * - Main area (tableLayoutPanelMainArea):
 *   + Main icon (pictureBoxMainIcon).
 *   + Main information (flowLayoutPanelMainAreaControls).
 *   + Content (flowLayoutPanelMainAreaControls).
 *   + Expanded information (flowLayoutPanelMainAreaControls).
 *   + Progressbar (flowLayoutPanelMainAreaControls).
 *   + Radio buttons (flowLayoutPanelMainAreaControls).
 *   + Command links. (flowLayoutPanelMainAreaControls).
 * - Sub area (tableLayoutPanelSubArea).
 *   + Expand/Collapse button (flowLayoutPanelSubAreaControls).
 *   + Verification checkbox (flowLayoutPanelSubAreaControls).
 *   + Custom/Common buttons (flowLayoutPanelsSubAreaButtons).
 * - Footer area (tableLayoutPanelFooterArea).
 *   + Footer text (flowLayoutFooterAreaText).
 * - Footer expanded information (flowLayoutFooterExpandedInformationArea).
 *   + Expanded information (flowLayoutPanelFooterExpandedInformationAreaText).
 * 
 * Limitations:
 * - Doesn't support every ActiveTaskDialog actions, follow is the list of not
 *   supported actions (catch the actions at WndProc):
 *   + ClickButton
 *   + SetMarqueeProgressBar
 *   + SetProgressBarState
 *   + SetProgressBarRange
 *   + SetProgressBarMarquee
 *   + SetContent
 *   + SetExpandedInformation
 *   + SetFooter
 *   + SetMainInstruction
 *   + ClickRadioButton
 *   + EnableButton
 *   + EnableRadioButton
 *   + ClickVerification
 *   + UpdateContent
 *   + UpdateExpandedInformation
 *   + UpdateFooter
 *   + UpdateMainInstruction
 *   + SetButtonElevationRequiredState
 *   + UpdateMainIcon (TaskDialogIcon)
 *   + UpdateMainIcon (Icon)
 *   + UpdateFooterIcon (TaskDialogIcon)
 *   + UpdateFooterIcon (Icon)
 * - Form issues:
 *   + No form icon when TaskDialog.CanBeMinimized is true.
 *     * Note: .Net does not provide an easy way to convert bitmap to icon.
 *   + Cancellable form (allow cancel) does not work exactly like task dialog:
 *     * Form cannot be close using the "esc" key without a cancel button,
 *       which task dialog allows.
 *   + Might not support right to left layouts.
 *   + "Retry" button will be added in a wrong order.
 *   + Command links has no accelerator key support.
 *     * Note: CommandLink class draws the text instead of letting the button
 *       handle the text.
 * 
 * Revision Control:-
 * ------------------
 * Created On: 2008 October 03, 05:56
 * 
 * $Revision: 362 $
 * $LastChangedDate: 2008-10-09 22:10:25 +0800 (Thu, 09 Oct 2008) $
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    /// <summary>
    /// Tries to emulate the Task Dialog. Form will be called when the call to
    /// Task Dialog is not supported.
    /// </summary>
    public partial class EmulateTaskDialog : Form
    {
        public int TaskDialogRadioButtonResult { get; private set; }
        public int TaskDialogResult { get; private set; }
        public bool TaskDialogVerificationFlagChecked { get; private set; }

        private bool isExpanded = true;
        private Button defaultButton;
        private RadioButton defaultRadioButton;
        private EmulateExpInfoButton expandedInfoButton;
        private System.Windows.Forms.LinkLabel linkLabelExpandedInformation;
        private Dictionary<int, Button> taskDialogButtons = new Dictionary<int, Button> ( );
        private ProgressBar progressBar;
        private TaskDialog taskDialog;
        private uint timerTickCount;

        public EmulateTaskDialog ( TaskDialog newTaskDialog )
        {
            // http://dotnetperls.com/Content/Segoe-Tahoma-Windows-Forms.aspx
            // http://www.codeproject.com/KB/cs/AdjustingFontAndLayout.aspx
            // For some discussion about setting program fonts.
            this.Font = SystemFonts.MessageBoxFont;

            InitializeComponent ( );

            this.taskDialog = newTaskDialog;
            
            BuildForm ( );

            // Setup the default settings.

            if ( this.defaultRadioButton != null )
            {
                this.defaultRadioButton.Checked = true;
            }

            // Only can set focus after everything has been build.
            if ( this.taskDialog.DefaultButton != 0 )
            {
                // Set the default button.
                if ( this.taskDialogButtons.TryGetValue ( this.taskDialog.DefaultButton, out this.defaultButton ) )
                {
                    this.defaultButton.Select ( );
                }
                else
                {
                    if ( this.flowLayoutPanelSubAreaButtons.Controls.Count > 0 )
                    {
                        // Select left-most button.
                        this.flowLayoutPanelSubAreaButtons.Controls[this.flowLayoutPanelSubAreaButtons.Controls.Count - 1].Select ( );
                    }
                }
            }
            else
            {
                if ( this.flowLayoutPanelSubAreaButtons.Controls.Count > 0 )
                {
                    // Set the left-most button to be the default. Dictionary does
                    // not keep the order of the button that is added in, so this
                    // work around is used.
                    //
                    // Layout right to left, left-most item is at the end of the list.
                    this.flowLayoutPanelSubAreaButtons.Controls[this.flowLayoutPanelSubAreaButtons.Controls.Count - 1].Select ( );
                }
            }

            if ( this.taskDialog.Callback != null )
            {
                timer.Start ( );
            }
        }

        /// <summary>
        /// Creates a standard button with a common event handler to close the
        /// the form and return the button's Id.
        /// </summary>
        /// <param name="tag">The button's Id.</param>
        /// <param name="text">The button's name.</param>
        /// <returns></returns>
        private Button BuildControlButton ( int tag, string text )
        {
            Button button = new Button ( );
            button.Click += new EventHandler ( button_Click );
            button.Tag = tag;
            button.Text = text;

            this.taskDialogButtons.Add ( tag, button );

            return button;
        }

        /// <summary>
        /// Creates a standard link label and will convert any anchor links to
        /// a proper link.
        /// </summary>
        /// <param name="margin">The margin of the link label.</param>
        /// <param name="text">The text of the link label.</param>
        /// <returns></returns>
        private System.Windows.Forms.LinkLabel BuildControlLinkLabel(Padding margin, string text)
        {
            System.Windows.Forms.LinkLabel linkLabel = new System.Windows.Forms.LinkLabel ( );
            linkLabel.AutoSize = true;
            linkLabel.LinkArea = new LinkArea ( );
            linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler ( linkLabel_LinkClicked );
            linkLabel.Margin = margin;
            linkLabel.Text = text;

            if ( this.taskDialog.EnableHyperlinks )
            {
                linkLabel = ParseLinkLabel ( linkLabel );
            }

            return linkLabel;
        }

        /// <summary>
        /// Main entry method to build the form.
        /// </summary>
        private void BuildForm ( )
        {
            this.ControlBox = this.taskDialog.AllowDialogCancellation;
            if ( ( this.MinimizeBox = this.taskDialog.CanBeMinimized ) == true )
            {
                // If can be minimised, ControlBox must be enabled.
                this.ControlBox = true;
                this.ShowInTaskbar = true;

                // TODO: Form icon will be the TaskDialog.MainIcon. Probem is
                // .Net framework does not provide a way to generate an icon.
            }
            this.StartPosition = taskDialog.PositionRelativeToWindow ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen;
            this.Text = String.IsNullOrEmpty ( taskDialog.WindowTitle ) ? "" : taskDialog.WindowTitle;
            
            BuildFormMainArea ( );
            BuildFormFooterArea ( );
            // Build the sub area last because of the possibility of having the
            // expanded information in the footer. Then building the "expand/
            // collapse" button will be accurate.
            BuildFormSubArea ( );

            SetFormHeight ( );
        }

        private void BuildFormFooterArea ( )
        {
            if ( !String.IsNullOrEmpty ( this.taskDialog.Footer ) )
            {
                this.pictureBoxFooterAreaIcon.Size = new Size ( 16, 16 );

                switch ( this.taskDialog.FooterIcon )
                {
                    case TaskDialogIcon.Error:
                        this.pictureBoxFooterAreaIcon.Image = SystemIcons.Error.ToBitmap ( ).GetThumbnailImage ( 16, 16, null, IntPtr.Zero );
                        break;
                    case TaskDialogIcon.Information:
                        this.pictureBoxFooterAreaIcon.Image = SystemIcons.Information.ToBitmap ( ).GetThumbnailImage ( 16, 16, null, IntPtr.Zero );
                        break;
                    case TaskDialogIcon.Shield:
                        this.pictureBoxFooterAreaIcon.Image = SystemIcons.Shield.ToBitmap ( ).GetThumbnailImage ( 16, 16, null, IntPtr.Zero );
                        break;
                    case TaskDialogIcon.Warning:
                        this.pictureBoxFooterAreaIcon.Image = SystemIcons.Warning.ToBitmap ( ).GetThumbnailImage ( 16, 16, null, IntPtr.Zero );
                        break;
                    default:
                        this.tableLayoutPanelFooterArea.ColumnStyles[0].Width = 0;
                        break;
                }

                this.flowLayoutPanelFooterAreaText.Controls.Add ( BuildControlLinkLabel ( new Padding ( 0 ), this.taskDialog.Footer ) );
            }
            else
            {
                this.tableLayoutPanelFooterArea.AutoSize = false;
                this.tableLayoutPanelFooterArea.Height = 0;
                this.tableLayoutPanelFooterArea.Visible = false;

                this.tableLayoutPanel2.Height = 0;
                this.tableLayoutPanel2.Visible = false;
            }

            if ( this.taskDialog.ExpandFooterArea && !String.IsNullOrEmpty ( this.taskDialog.ExpandedInformation ) )
            {
                linkLabelExpandedInformation = BuildControlLinkLabel ( new Padding ( 0 ), this.taskDialog.ExpandedInformation );

                this.flowLayoutPanelFooterExpandedInformationText.Controls.Add ( linkLabelExpandedInformation );

                if ( !this.taskDialog.ExpandedByDefault )
                {
                    ToggleExpandedInformationState ( );
                }
            }
            else
            {
                this.flowLayoutPanelFooterExpandedInformationText.AutoSize = false;
                this.flowLayoutPanelFooterExpandedInformationText.Height = 0;
                this.tableLayoutPanelFooterExpandedInformationArea.Visible = false;


                this.tableLayoutPanel3.Height = 0;
                this.tableLayoutPanel3.Visible = false;
            }
        }

        private void BuildFormMainArea ( )
        {
            switch ( this.taskDialog.MainIcon )
            {
                case TaskDialogIcon.Error:
                    this.pictureBoxMainAreaIcon.Image = SystemIcons.Error.ToBitmap ( );
                    break;
                case TaskDialogIcon.Information:
                    this.pictureBoxMainAreaIcon.Image = SystemIcons.Information.ToBitmap ( );
                    break;
                case TaskDialogIcon.Shield:
                    this.pictureBoxMainAreaIcon.Image = SystemIcons.Shield.ToBitmap ( );
                    break;
                case TaskDialogIcon.Warning:
                    this.pictureBoxMainAreaIcon.Image = SystemIcons.Warning.ToBitmap ( );
                    break;
                default:
                    tableLayoutPanelMainArea.ColumnStyles[0].Width = 0;
                    break;
            }

            if ( !String.IsNullOrEmpty ( this.taskDialog.MainInstruction ) )
            {
                Label label = new Label ( );
                label.AutoSize = true;
                label.Font = new Font ( this.Font.FontFamily, (float)10.25, FontStyle.Bold );
                label.ForeColor = Color.DarkBlue;
                label.Margin = new Padding ( 0, 0, 0, 15 );
                label.Text = this.taskDialog.MainInstruction;

                this.flowLayoutPanelMainAreaControls.Controls.Add ( label );
            }

            if ( !String.IsNullOrEmpty ( this.taskDialog.Content ) )
            {
                this.flowLayoutPanelMainAreaControls.Controls.Add ( BuildControlLinkLabel ( new Padding ( 0, 0, 0, 15 ), this.taskDialog.Content ) );
            }

            if ( !String.IsNullOrEmpty ( this.taskDialog.ExpandedInformation ) && !this.taskDialog.ExpandFooterArea )
            {
                linkLabelExpandedInformation = BuildControlLinkLabel ( new Padding ( 0, 0, 0, 15 ), this.taskDialog.ExpandedInformation );

                this.flowLayoutPanelMainAreaControls.Controls.Add ( linkLabelExpandedInformation );

                if ( !this.taskDialog.ExpandedByDefault )
                {
                    ToggleExpandedInformationState ( );
                }
            }

            if ( this.taskDialog.ShowProgressBar || this.taskDialog.ShowMarqueeProgressBar )
            {
                this.progressBar = new ProgressBar ( );

                if ( this.taskDialog.ShowMarqueeProgressBar )
                {
                    this.progressBar.Style = ProgressBarStyle.Marquee;
                }

                this.progressBar.Size = new Size ( this.flowLayoutPanelMainAreaControls.Width - 5, 15 );
                this.progressBar.Margin = new Padding ( 0, 0, 0, 5 );

                this.flowLayoutPanelMainAreaControls.Controls.Add ( this.progressBar );
            }

            for ( int i = 0; i < this.taskDialog.RadioButtons.Length; i++ )
            {
                RadioButton radioButton = new RadioButton ( );
                radioButton.Text = this.taskDialog.RadioButtons[i].ButtonText;
                radioButton.Tag = this.taskDialog.RadioButtons[i].ButtonId;

                // Select the first radio button by default.
                if ( ( i == 0 ) || ( this.taskDialog.DefaultRadioButton == this.taskDialog.RadioButtons[i].ButtonId ) )
                {
                    this.defaultRadioButton = radioButton;
                }

                this.flowLayoutPanelMainAreaControls.Controls.Add ( radioButton );
            }

            // Command links.
            if ( this.taskDialog.UseCommandLinks )
            {
                for ( int i = 0; i < this.taskDialog.Buttons.Length; i++ )
                {
                    EmulateCommandLink cmdLink = new EmulateCommandLink ( );
                    cmdLink.Click += new EventHandler ( button_Click );
                    cmdLink.Tag = this.taskDialog.Buttons[i].ButtonId;
                    cmdLink.Text = this.taskDialog.Buttons[i].ButtonText;
                    cmdLink.Size = new Size ( this.flowLayoutPanelMainAreaControls.Width - 10, cmdLink.GetBestHeight ( ) );

                    this.taskDialogButtons.Add ( this.taskDialog.Buttons[i].ButtonId, cmdLink );

                    this.flowLayoutPanelMainAreaControls.Controls.Add ( cmdLink );
                }
            }
        }
        
        private void BuildFormSubArea ( )
        {
            // Left side of the sub control area.
            // Check if there is any expanded information first.
            if ( !String.IsNullOrEmpty ( this.taskDialog.ExpandedInformation ) )
            {
                expandedInfoButton = new EmulateExpInfoButton (
                        this.isExpanded,
                        this.taskDialog.ExpandedControlText,
                        this.taskDialog.CollapsedControlText
                        );
                expandedInfoButton.Click += new EventHandler ( expandedInfoButton_Click );

                this.flowLayoutPanelSubAreaControls.Controls.Add ( expandedInfoButton );
                // Smallest element in this area.
                this.tableLayoutPanelSubArea.MinimumSize = new Size ( 0, 34 );
            }

            if ( !String.IsNullOrEmpty ( this.taskDialog.VerificationText ) )
            {
                CheckBox checkBox = new CheckBox ( );
                checkBox.Checked = this.taskDialog.VerificationFlagChecked;
                checkBox.Margin = new Padding ( 8, 5, 0, 5 );
                checkBox.MaximumSize = new Size ( 150, 0 );
                checkBox.Text = this.taskDialog.VerificationText;
                checkBox.Width = 165;

                // AdjustControls doesn't take in account the checkbox, so need
                // to work around it.
                Size size = AdjustControls.GetBestSize (
                    checkBox,
                    checkBox.Text,
                    new Rectangle ( 0, 0, checkBox.Width - 52, checkBox.Height )
                    );
                checkBox.Height = size.Height + 8;

                this.flowLayoutPanelSubAreaControls.Controls.Add ( checkBox );
            }

            // Right side of the sub control area.
            // If there is no button, by default there will be an "Okay" button.
            if ( this.taskDialog.CommonButtons.Equals ( TaskDialogCommonButtons.None ) &&
                this.taskDialog.Buttons.Length == 0 )
            {
                this.flowLayoutPanelSubAreaButtons.Controls.Add ( BuildControlButton ( (int)DialogResult.OK, DialogResult.OK.ToString ( ) ) );
            }
            else
            {
                int requiredTotalButtonsWidth = 0;

                TaskDialogCommonButtons[] commonButtons = (TaskDialogCommonButtons[])Enum.GetValues ( typeof ( TaskDialogCommonButtons ) );

                // Iterate through all the common buttons. Get by reverse order
                // as the layout direction is right to left.
                for ( int i = commonButtons.Length - 1; i >= 0; i-- )
                {
                    // There is no "None" button.
                    if ( !commonButtons[i].Equals ( TaskDialogCommonButtons.None ) )
                    {
                        // Now to check which button is needed.
                        if ( ( this.taskDialog.CommonButtons & commonButtons[i] ).Equals ( commonButtons[i] ) )
                        {
                            // TaskDialogCommonButtons enums is not the same as
                            // DialogResult enums. Need to use another way of
                            // getting the correct DialogResult value.
                            Button button = new Button ( );

                            // DialogResult does not contain a "Close" enum,
                            // return value by TaskDialog is int 8.
                            if ( commonButtons[i].Equals ( TaskDialogCommonButtons.Close ) )
                            {
                                button = BuildControlButton ( 8, TaskDialogCommonButtons.Close.ToString ( ) );
                            }
                            // No "Ok" button. Is it a typo for TaskDialogCommonButtons in TaskDialog.cs?
                            else if ( commonButtons[i].Equals ( TaskDialogCommonButtons.OK ) )
                            {
                                button = BuildControlButton ( (int)DialogResult.OK, DialogResult.OK.ToString ( ) );
                            }
                            else
                            {
                                DialogResult result = (DialogResult)Enum.Parse ( typeof ( DialogResult ), commonButtons[i].ToString ( ) );
                                button = BuildControlButton ( (int)result, result.ToString ( ) );
                            }

                            button.TabIndex = i + this.taskDialog.Buttons.Length;

                            this.flowLayoutPanelSubAreaButtons.Controls.Add ( button );

                            requiredTotalButtonsWidth += button.Width + button.Margin.Left + button.Margin.Right;
                        }
                    }
                }

                // Custom buttons. Get by reverse order as the layout direction
                // is right to left.
                if ( !this.taskDialog.UseCommandLinks )
                {
                    for ( int i = this.taskDialog.Buttons.Length - 1; i >= 0; i-- )
                    {
                        Button button = BuildControlButton ( this.taskDialog.Buttons[i].ButtonId, this.taskDialog.Buttons[i].ButtonText );
                        this.flowLayoutPanelSubAreaButtons.Controls.Add ( button );

                        button.TabIndex = i;

                        requiredTotalButtonsWidth += button.Width + button.Margin.Left + button.Margin.Right;
                    }
                }

                // Check if there is anything in the sub area.
                if ( ( requiredTotalButtonsWidth == 0 ) &&
                    ( String.IsNullOrEmpty ( this.taskDialog.ExpandedInformation ) ) &&
                    ( String.IsNullOrEmpty ( this.taskDialog.VerificationText ) ) )
                {
                    this.tableLayoutPanel1.Visible = false;
                    this.tableLayoutPanel1.Height = 0;
                    this.tableLayoutPanelSubArea.Visible = false;
                }
                else
                {
                    // Padding on the right.
                    requiredTotalButtonsWidth += 15;

                    if ( requiredTotalButtonsWidth > this.flowLayoutPanelSubAreaButtons.Width )
                    {
                        this.Width += requiredTotalButtonsWidth - this.flowLayoutPanelSubAreaButtons.Width;
                    }
                }
            }
        }

        /// <summary>
        /// Closes the form and populate the necessary return results.
        /// </summary>
        private void CloseForm ( )
        {
            if ( taskDialog.RadioButtons.Length > 0 )
            {
                // Check all the radio buttons.
                foreach ( Control control in this.flowLayoutPanelMainAreaControls.Controls )
                {
                    if ( control is RadioButton )
                    {
                        RadioButton radioButton = (RadioButton)control;
                        if ( radioButton.Checked )
                        {
                            this.TaskDialogRadioButtonResult = (int)radioButton.Tag;
                            break;
                        }
                    }
                }
            }

            if ( !String.IsNullOrEmpty ( this.taskDialog.VerificationText ) )
            {
                foreach ( Control control in this.flowLayoutPanelSubAreaControls.Controls )
                {
                    if ( control is CheckBox )
                    {
                        this.TaskDialogVerificationFlagChecked = ( (CheckBox)control ).Checked;
                        break;
                    }
                }
            }

            this.timer.Stop ( );

            this.Close ( );
        }

        /// <summary>
        /// Parses a LinkLabel text and extract out all the anchor (a) tags.
        /// Then it will parse all the anchor tags and create the relevant
        /// links in the text.
        /// </summary>
        /// <param name="linkLabel"></param>
        /// <returns></returns>
        private System.Windows.Forms.LinkLabel ParseLinkLabel(System.Windows.Forms.LinkLabel linkLabel)
        {
            // Set up the text to parse.
            string text = linkLabel.Text;
            
            // Set up the regex for finding the link urls.
            StringBuilder hrefPattern = new StringBuilder ( );
            // Start anchor tag and anything that comes before "href" tag.
            hrefPattern.Append ( "<a[^>]+" );
            hrefPattern.Append ( "href\\s*=\\s*" ); // Start href property.
            // Three possibilities for "href":
            // (1) enclosed in double quotes.
            // (2) enclosed in single quotes.
            // (3) enclosed in spaces.
            hrefPattern.Append ( "(?:\"(?<href>[^\"]*)\"|'(?<href>[^']*)'|(?<href>[^\"'>\\s]+))" );
            // Grab the inner html too.
            hrefPattern.Append ( "[^>]*>(?<a>.*?)</a>" ); // End of anchor tag.
            Regex hrefRegex = new Regex ( hrefPattern.ToString ( ), RegexOptions.IgnoreCase );

            // Look for matches.
            Match hrefCheck = hrefRegex.Match ( text );
            
            while ( hrefCheck.Success )
            {
                string href = hrefCheck.Groups["href"].Value;
                string innerText = hrefCheck.Groups["a"].Value;

                // Get the starting index of the anchor tag.
                int index = linkLabel.Text.IndexOf ( hrefCheck.Value );
                // Replace it with the inner text, create a link and store the
                // link.
                linkLabel.Text = linkLabel.Text.Replace ( hrefCheck.Value, innerText );
                linkLabel.Links.Add ( index, innerText.Length, href );

                hrefCheck = hrefCheck.NextMatch ( );
            }

            return linkLabel;
        }

        private void SetFormHeight ( )
        {
            this.Height = 
                (this.Height - this.ClientSize.Height) + 7
                + this.tableLayoutPanelMainArea.Height
                + this.tableLayoutPanel1.Height // Separator.
                // When empty, default height is 33. TableLayoutPanel refuses to
                // shrink to 0 as there are items in it.
                + ( ( this.tableLayoutPanelSubArea.Height > 33 ) ? this.tableLayoutPanelSubArea.Height : 0 )
                + this.tableLayoutPanel2.Height // Separator.
                + ( ( this.tableLayoutPanelFooterArea.Height > 0 ) ? this.tableLayoutPanelFooterArea.Height : 0 )
                + this.tableLayoutPanel3.Height // Separator.
                // When empty, default height is 6. TableLayoutPanel refuses to
                // shrink to 0 as there are items in it.
                + ( ( this.tableLayoutPanelFooterExpandedInformationArea.Height > 6 ) ? this.tableLayoutPanelFooterExpandedInformationArea.Height : 0 );
        }

        private void ToggleExpandedInformationState ( )
        {
            if ( this.taskDialog.ExpandFooterArea )
            {
                // Shrink/expand our upper "border".
                if ( this.isExpanded )
                {
                    this.tableLayoutPanel3.Visible = false;
                    this.Height -= this.tableLayoutPanel3.Height;
                }
                else
                {
                    this.tableLayoutPanel3.Visible = true;
                    this.Height += this.tableLayoutPanel3.Height;
                }
            }

            if ( this.isExpanded )
            {
                // Shrink the form first.
                this.Height -= linkLabelExpandedInformation.Height;
                this.linkLabelExpandedInformation.Visible = false;
                this.isExpanded = false;
            }
            else
            {
                this.linkLabelExpandedInformation.Visible = true;
                // Expand the form.
                this.Height += linkLabelExpandedInformation.Height;
                this.isExpanded = true;
            }
        }

        /// <summary>
        /// This will return the formatted contents of the form. Useful when
        /// the user press "ctrl + c" on the form.
        /// </summary>
        /// <returns></returns>
        public override string ToString ( )
        {
            StringBuilder sb = new StringBuilder ( );

            sb.AppendLine ( "[Window Title]" );
            sb.AppendLine ( this.Text );

            if ( !String.IsNullOrEmpty ( this.taskDialog.MainInstruction ) )
            {
                sb.AppendLine ( "" );
                sb.AppendLine ( "[Main Instruction]" );
                sb.AppendLine ( this.taskDialog.MainInstruction );
            }

            if ( !String.IsNullOrEmpty ( this.taskDialog.Content ) )
            {
                sb.AppendLine ( "" );
                sb.AppendLine ( "[Content]" );
                sb.AppendLine ( this.taskDialog.Content );
            }

            if ( !String.IsNullOrEmpty ( this.taskDialog.ExpandedInformation ) && !this.taskDialog.ExpandFooterArea )
            {
                sb.AppendLine ( "" );
                sb.AppendLine ( "[Expanded Information]" );
                sb.AppendLine ( this.taskDialog.ExpandedInformation );
            }

            // There should be a max of two controls here.
            foreach ( Control control in this.flowLayoutPanelSubAreaControls.Controls )
            {
                if ( control is EmulateExpInfoButton )
                {
                    // Should be the expand/collapse button.
                    sb.AppendLine ( "" );
                    sb.Append ( ( this.isExpanded ? "[^] " : "[v] " ) );
                    sb.Append ( control.Text );
                    sb.Append ( "  " );
                }

                if ( control is CheckBox )
                {
                    sb.Append ( ( ( (CheckBox)control ).Checked ? "[X] " : "[ ] " ) );
                    sb.Append ( control.Text );
                    sb.Append ( "  " );
                }
            }

            // Should contains only buttons. As the layout direction is right
            // to left, need to get by reverse order. Dictionary does not keep
            // the order of buttons added in. So this work around is used.
            for ( int i = this.flowLayoutPanelSubAreaButtons.Controls.Count - 1; i >= 0; i-- )
            {
                Control control = this.flowLayoutPanelSubAreaButtons.Controls[i];
                if ( control is Button )
                {
                    sb.Append ( "[" + control.Text + "]" );
                    sb.Append ( " " );
                }
            }

            sb.AppendLine ( "" );

            if ( !String.IsNullOrEmpty ( this.taskDialog.Footer ) )
            {
                sb.AppendLine ( "" );
                sb.AppendLine ( "[Footer]" );
                sb.AppendLine ( this.taskDialog.Footer );
            }

            if ( !String.IsNullOrEmpty ( this.taskDialog.ExpandedInformation ) && this.taskDialog.ExpandFooterArea )
            {
                sb.AppendLine ( "" );
                sb.AppendLine ( "[Expanded Information]" );
                sb.AppendLine ( this.taskDialog.ExpandedInformation );
            }

            return sb.ToString ( );
        }

        protected override void WndProc ( ref Message m )
        {
            switch ( m.Msg )
            {
                case (int)Win32.NativeMethods.TASKDIALOG_MESSAGES.TDM_SET_PROGRESS_BAR_POS:
                    if ( this.progressBar != null )
                    {
                        if ( m.WParam.ToInt32 ( ) <= this.progressBar.Maximum )
                        {
                            this.progressBar.Value = m.WParam.ToInt32 ( );
                        }
                    }
                    break;
                default:
                    base.WndProc ( ref m );
                    break;
            }
        }

        #region Events.
        private void button_Click ( object sender, EventArgs e )
        {
            this.TaskDialogResult = (int)( (Button)sender ).Tag;

            CloseForm ( );
        }

        private void expandedInfoButton_Click ( object sender, EventArgs e )
        {
            ToggleExpandedInformationState ( );

            expandedInfoButton.ToogleState ( );

            SetFormHeight ( );
        }

        /// <summary>
        /// Detect user pressing "ctrl + c".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmulateTaskDialog_KeyDown ( object sender, KeyEventArgs e )
        {
            Keys notWanted = Keys.Alt | Keys.Shift;

            if ( ( e.Modifiers & notWanted ) == 0 )
            {
                if ( ( e.Modifiers & Keys.Control ) == Keys.Control )
                {
                    if ( e.KeyCode.Equals ( Keys.C ) )
                    {
                        Clipboard.SetText ( this.ToString ( ) );
                    }
                }
            }
        }

        private void linkLabel_LinkClicked ( object sender, LinkLabelLinkClickedEventArgs e )
        {
            TaskDialogNotificationArgs notificationArgs = new TaskDialogNotificationArgs ( );
            notificationArgs.Notification = TaskDialogNotification.HyperlinkClicked;
            notificationArgs.Hyperlink = (string)e.Link.LinkData;

            this.taskDialog.Callback ( null, notificationArgs, null );
        }

        private void timer_Tick ( object sender, EventArgs e )
        {
            this.timerTickCount += (uint)this.timer.Interval;

            TaskDialogNotificationArgs notificationArgs = new TaskDialogNotificationArgs ( );
            notificationArgs.Notification = TaskDialogNotification.Timer;
            notificationArgs.TimerTickCount = this.timerTickCount;

            if ( this.taskDialog.Callback ( new ActiveTaskDialog ( this.Handle ), notificationArgs, null ) )
            {
                // Reset timer.
                this.timerTickCount = 0;
            }
        }
        #endregion
    }
}
