using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public partial class MessageForm : Form
    {
        internal static readonly Font LargeThemedFont = CreateFont(true);
        internal static readonly Font ThemedFont = CreateFont(false);

        public MessageForm()
        {
            InitializeComponent();

            MessageBoxButtons = MessageBoxButtons.OK;
            MessageBoxIcon = MessageBoxIcon.None;
        }

        public string MessageText
        {
            get { return messageFormContent1.Text; }
            set { messageFormContent1.Text = value; }
        }

        public Image MessageIcon
        {
            get { return messageFormContent1.Icon; }
            set { messageFormContent1.Icon = value; }
        }

        public MessageBoxIcon MessageBoxIcon { get; set; }

        [DefaultValue(MessageBoxButtons.OK)]
        public MessageBoxButtons MessageBoxButtons { get; private set; }

        public MessageFormButton[] Buttons { get; set; }

        public object Result { get; private set; }

        private void MessageForm_Showing(object sender, EventArgs e)
        {
            if (Owner == null)
                StartPosition = FormStartPosition.CenterScreen;

            SelectButtons();
            SelectIcon();

            BuildButtons();

            Height -= messageFormContent1.Height - messageFormContent1.CalculatedHeight;
        }

        private void BuildButtons()
        {
            bool hadAccept = false;
            bool hadCancel = false;

            for (int i = Buttons.Length - 1; i >= 0; i--)
            {
                var button = Buttons[i];

                var control = new Button
                {
                    Text = button.Text,
                    Tag = button,
                    TabIndex = i
                };

                control.Click += new EventHandler(control_Click);

                int width = TextRenderer.MeasureText(button.Text, Font).Width + 18;

                if (control.Width < width)
                    control.Width = width;

                formFlowFooter.Controls.Add(control);

                if (!hadAccept && button.IsAcceptButton)
                {
                    AcceptButton = control;
                    hadAccept = true;
                }
                if (!hadCancel && button.IsCancelButton)
                {
                    CancelButton = control;
                    hadCancel = true;
                }
            }

            messageFormContent1.Enabled = !hadAccept;

            if (!hadCancel)
            {
                Win32.NativeMethods.EnableMenuItem(
                    Win32.NativeMethods.GetSystemMenu(this.Handle, false),
                    Win32.NativeMethods.SC_CLOSE, Win32.NativeMethods.MF_GRAYED);
            }
        }

        void control_Click(object sender, EventArgs e)
        {
            Result = ((MessageFormButton)((Control)sender).Tag).Result;

            DialogResult = DialogResult.OK;
        }

        private void SelectIcon()
        {
            if (messageFormContent1.Icon == null && MessageBoxIcon != MessageBoxIcon.None)
            {
                switch (MessageBoxIcon)
                {
                    case MessageBoxIcon.Information:
                        messageFormContent1.Icon = Properties.Resources.MessageBoxIconInformation;
                        break;

                    case MessageBoxIcon.Question:
                        messageFormContent1.Icon = Properties.Resources.MessageBoxIconQuestion;
                        break;

                    case MessageBoxIcon.Error:
                        messageFormContent1.Icon = Properties.Resources.MessageBoxIconError;
                        break;

                    case MessageBoxIcon.Warning:
                        messageFormContent1.Icon = Properties.Resources.MessageBoxIconWarning;
                        break;
                }
            }
        }

        private void SelectButtons()
        {
            if (Buttons == null)
            {
                switch (MessageBoxButtons)
                {
                    case MessageBoxButtons.AbortRetryIgnore:
                        Buttons = new MessageFormButton[]
                        {
                            new MessageFormButton("&Abort", DialogResult.Abort, true, false),
                            new MessageFormButton("&Retry", DialogResult.Retry),
                            new MessageFormButton("&Ignore", DialogResult.Ignore)
                        };
                        break;

                    case MessageBoxButtons.OK:
                        Buttons = new MessageFormButton[]
                        {
                            new MessageFormButton("OK", DialogResult.OK, true, true)
                        };
                        break;

                    case MessageBoxButtons.OKCancel:
                        Buttons = new MessageFormButton[]
                        {
                            new MessageFormButton("OK", DialogResult.OK, true, false),
                            new MessageFormButton("Cancel", DialogResult.Cancel, false, true)
                        };
                        break;

                    case MessageBoxButtons.RetryCancel:
                        Buttons = new MessageFormButton[]
                        {
                            new MessageFormButton("&Retry", DialogResult.Retry, true, false),
                            new MessageFormButton("Cancel", DialogResult.Cancel, false, true)
                        };
                        break;

                    case MessageBoxButtons.YesNo:
                        Buttons = new MessageFormButton[]
                        {
                            new MessageFormButton("&Yes", DialogResult.Yes, true, false),
                            new MessageFormButton("&No", DialogResult.No)
                        };
                        break;

                    case MessageBoxButtons.YesNoCancel:
                        Buttons = new MessageFormButton[]
                        {
                            new MessageFormButton("&Yes", DialogResult.Yes, true, false),
                            new MessageFormButton("&No", DialogResult.No),
                            new MessageFormButton("Cancel", DialogResult.Cancel, false, true)
                        };
                        break;

                    default:
                        throw new Exception("Cannot understand MessageBoxButtons");
                }
            }
        }
        public static DialogResult Show(string text)
        {
            return Show(text, null);
        }

        public static DialogResult Show(IWin32Window owner, string text)
        {
            return Show(owner, text, null);
        }

        public static DialogResult Show(string text, string caption)
        {
            return Show(text, caption, MessageBoxButtons.OK);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption)
        {
            return Show(owner, text, caption, MessageBoxButtons.OK);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return Show(text, caption, buttons, MessageBoxIcon.None);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
        {
            return Show(owner, text, caption, buttons, MessageBoxIcon.None);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show((IWin32Window)null, text, caption, buttons, icon);
        }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (var form = new MessageForm())
            {
                form.MessageText = text;
                form.Text = caption;
                form.MessageBoxButtons = buttons;
                form.MessageBoxIcon = icon;

                form.ShowDialog(owner);

                return (DialogResult)form.Result;
            }
        }

        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CancelButton != null && Result == null)
                Result = ((MessageFormButton)((Control)CancelButton).Tag).Result;
        }

        private void MessageForm_Shown(object sender, EventArgs e)
        {
            if (AcceptButton != null)
                ((Button)AcceptButton).Focus();
            else
                messageFormContent1.Focus();
        }

        private static Font CreateFont(bool large)
        {
            string fontFamily = SystemFonts.MessageBoxFont.FontFamily.Name;
            float fontSize = large ? 12F : SystemFonts.MessageBoxFont.SizeInPoints;

            if (fontFamily != "Segoe UI")
            {
                fontFamily = "Arial";
                fontSize = large ? 11.75F : 8F;
            }

            return new Font(fontFamily, fontSize, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
        }
    }
}
