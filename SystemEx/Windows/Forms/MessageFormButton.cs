using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx.Windows.Forms
{
    public class MessageFormButton
    {
        public MessageFormButton(string text, object value)
            : this(text, value, false, false)
        {
        }

        public MessageFormButton(string text, object value, bool isAcceptButton, bool isCancelButton)
        {
            Text = text;
            Result = value;
            IsAcceptButton = isAcceptButton;
            IsCancelButton = isCancelButton;
        }

        public string Text { get; private set; }
        public object Result { get; private set; }
        public bool IsAcceptButton { get; private set; }
        public bool IsCancelButton { get; private set; }
    }
}
