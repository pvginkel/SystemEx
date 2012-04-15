/*
 * Design Notes:-
 * --------------
 * - Maximum size: 150px. Limited by the control itself.
 * 
 * Revision Control:-
 * ------------------
 * Created On: 2008 October 04, 23:52
 * 
 * $Revision:$
 * $LastChangedDate:$
 */

using System.Text;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace SystemEx.Windows.Forms
{
    [DesignTimeVisible(false)]
    public partial class EmulateExpInfoButton : UserControl
    {
        public override string Text
        {
            get { return this.label.Text; }
            set { this.label.Text = value; }
        }

        private bool isExpanded;
        private string expandedText;
        private string collapsedText;

        public EmulateExpInfoButton ( bool newIsExpanded, string newExpandedText, string newCollapsedText  )
        {
            InitializeComponent ( );

            this.pictureBox.Click += new EventHandler ( EmulateExpInfoButton_Click );
            this.pictureBox.MouseDown += new MouseEventHandler ( EmulateExpInfoButton_MouseDown );
            this.pictureBox.MouseEnter += new EventHandler ( EmulateExpInfoButton_MouseEnter );
            this.pictureBox.MouseLeave += new EventHandler ( EmulateExpInfoButton_MouseLeave );
            this.pictureBox.MouseUp += new MouseEventHandler ( EmulateExpInfoButton_MouseUp );
            this.label.Click += new EventHandler ( EmulateExpInfoButton_Click );
            this.label.MouseDown += new MouseEventHandler ( EmulateExpInfoButton_MouseDown );
            this.label.MouseEnter += new EventHandler ( EmulateExpInfoButton_MouseEnter );
            this.label.MouseLeave += new EventHandler ( EmulateExpInfoButton_MouseLeave );
            this.label.MouseUp += new MouseEventHandler ( EmulateExpInfoButton_MouseUp );
            this.tableLayoutPanel.Click += new EventHandler ( EmulateExpInfoButton_Click );
            this.tableLayoutPanel.MouseDown += new MouseEventHandler ( EmulateExpInfoButton_MouseDown );
            this.tableLayoutPanel.MouseEnter += new EventHandler ( EmulateExpInfoButton_MouseEnter );
            this.tableLayoutPanel.MouseLeave += new EventHandler ( EmulateExpInfoButton_MouseLeave );
            this.tableLayoutPanel.MouseUp += new MouseEventHandler ( EmulateExpInfoButton_MouseUp );

            this.isExpanded = newIsExpanded;
            this.expandedText = newExpandedText;
            this.collapsedText = newCollapsedText;

            SetState ( );
        }

        private void SetState ( )
        {
            if ( this.isExpanded )
            {
                this.pictureBox.Image = Properties.Resources.ChevronLess;

                if ( String.IsNullOrEmpty ( this.collapsedText ) )
                {
                    this.Text = "Hide details";
                }
                else
                {
                    this.Text = this.collapsedText;
                }
            }
            else
            {
                this.pictureBox.Image = Properties.Resources.ChevronMore;

                if ( String.IsNullOrEmpty ( this.expandedText ) && String.IsNullOrEmpty ( this.collapsedText ) )
                {
                    this.Text = "See details";
                }
                else if ( String.IsNullOrEmpty ( this.expandedText ) && !String.IsNullOrEmpty ( this.collapsedText ) )
                {
                    this.Text = this.collapsedText;
                }
                else
                {
                    this.Text = this.expandedText;
                }
            }
        }

        public void ToogleState ( )
        {
            this.isExpanded = !this.isExpanded;

            SetState ( );
        }

        #region Events.
        private void EmulateExpInfoButton_Enter ( object sender, EventArgs e )
        {
            // Focus rectangle.
            // http://msdn.microsoft.com/en-us/library/system.windows.forms.controlpaint.drawfocusrectangle(VS.71).aspx
            ControlPaint.DrawFocusRectangle ( Graphics.FromHwnd ( this.label.Handle ), this.label.ClientRectangle );
        }

        private void EmulateExpInfoButton_Leave ( object sender, EventArgs e )
        {
            // Lost focus.
            // Correct way to erase a border?
            ControlPaint.DrawBorder ( 
                Graphics.FromHwnd ( this.label.Handle ),
                this.label.ClientRectangle,
                this.label.BackColor,
                ButtonBorderStyle.Solid
                );
        }

        private void EmulateExpInfoButton_Click ( object sender, EventArgs e )
        {
            this.OnClick ( e );
        }

        private void EmulateExpInfoButton_MouseDown ( object sender, MouseEventArgs e )
        {
            this.pictureBox.Image = this.isExpanded ?
                Properties.Resources.ChevronLessPressed :
                Properties.Resources.ChevronMorePressed;
        }

        private void EmulateExpInfoButton_MouseEnter ( object sender, EventArgs e )
        {
            this.pictureBox.Image = this.isExpanded ?
                Properties.Resources.ChevronLessHovered :
                Properties.Resources.ChevronMoreHovered;
        }

        private void EmulateExpInfoButton_MouseLeave ( object sender, EventArgs e )
        {
            this.pictureBox.Image = this.isExpanded ?
                Properties.Resources.ChevronLess :
                Properties.Resources.ChevronMore;
        }

        private void EmulateExpInfoButton_MouseUp ( object sender, MouseEventArgs e )
        {
            this.pictureBox.Image = this.isExpanded ?
                Properties.Resources.ChevronLess :
                Properties.Resources.ChevronMore;
        }
        #endregion
    }
}
