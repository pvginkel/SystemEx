using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Windows.Forms
{
    public class EditButtonCollection : Collection<Button>
    {
        internal event EventHandler Changed;

        internal protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        protected override void ClearItems()
        {
            base.ClearItems();

            OnChanged(EventArgs.Empty);
        }

        protected override void InsertItem(int index, Button item)
        {
            base.InsertItem(index, item);

            OnChanged(EventArgs.Empty);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);

            OnChanged(EventArgs.Empty);
        }

        protected override void SetItem(int index, Button item)
        {
            base.SetItem(index, item);

            OnChanged(EventArgs.Empty);
        }
    }
}
