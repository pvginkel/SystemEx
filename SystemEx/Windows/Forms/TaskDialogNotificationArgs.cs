/*
 * Copyright © 2007 KevinGre
 * 
 * Design Notes:-
 * --------------
 * References:
 * - http://www.codeproject.com/KB/vista/TaskDialogWinForms.aspx
 * 
 * Revision Control:-
 * ------------------
 * Created On: 2007 January 02
 * 
 * $Revision:$
 * $LastChangedDate:$
 */

using System.Text;
using System.Collections.Generic;
using System;

namespace SystemEx.Windows.Forms
{
    /// <summary>
    /// Arguments passed to the TaskDialog callback.
    /// </summary>
    public class TaskDialogNotificationArgs
    {
        /// <summary>
        /// What the TaskDialog callback is a notification of.
        /// </summary>
        private TaskDialogNotification notification;

        /// <summary>
        /// The button ID if the notification is about a button. This a DialogResult
        /// value or the ButtonID member of a TaskDialogButton set in the
        /// TaskDialog.Buttons or TaskDialog.RadioButtons members.
        /// </summary>
        private int buttonId;

        /// <summary>
        /// The HREF string of the hyperlink the notification is about.
        /// </summary>
        private string hyperlink;

        /// <summary>
        /// The number of milliseconds since the dialog was opened or the last time the
        /// callback for a timer notification reset the value by returning true.
        /// </summary>
        private uint timerTickCount;

        /// <summary>
        /// The state of the verification flag when the notification is about the verification flag.
        /// </summary>
        private bool verificationFlagChecked;

        /// <summary>
        /// The state of the dialog expando when the notification is about the expando.
        /// </summary>
        private bool expanded;

        /// <summary>
        /// What the TaskDialog callback is a notification of.
        /// </summary>
        public TaskDialogNotification Notification
        {
            get { return this.notification; }
            set { this.notification = value; }
        }

        /// <summary>
        /// The button ID if the notification is about a button. This a DialogResult
        /// value or the ButtonID member of a TaskDialogButton set in the
        /// TaskDialog.Buttons member.
        /// </summary>
        public int ButtonId
        {
            get { return this.buttonId; }
            set { this.buttonId = value; }
        }

        /// <summary>
        /// The HREF string of the hyperlink the notification is about.
        /// </summary>
        public string Hyperlink
        {
            get { return this.hyperlink; }
            set { this.hyperlink = value; }
        }

        /// <summary>
        /// The number of milliseconds since the dialog was opened or the last time the
        /// callback for a timer notification reset the value by returning true.
        /// </summary>
        [CLSCompliant(false)]
        public uint TimerTickCount
        {
            get { return this.timerTickCount; }
            set { this.timerTickCount = value; }
        }

        /// <summary>
        /// The state of the verification flag when the notification is about the verification flag.
        /// </summary>
        public bool VerificationFlagChecked
        {
            get { return this.verificationFlagChecked; }
            set { this.verificationFlagChecked = value; }
        }

        /// <summary>
        /// The state of the dialog expando when the notification is about the expando.
        /// </summary>
        public bool Expanded
        {
            get { return this.expanded; }
            set { this.expanded = value; }
        }
    }
}
