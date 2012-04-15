using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class ToolTip : IDisposable
    {
        private System.Windows.Forms.ToolTip _toolTip = null;
        private TipInfo _tipInfo = null;
        private bool _disposed = false;

        public ToolTip()
        {
        }

        private System.Windows.Forms.ToolTip GetToolTip()
        {
            if (_toolTip == null)
            {
                _toolTip = new TransparentToolTip();

                _toolTip.OwnerDraw = true;
                _toolTip.ShowAlways = true;
                _toolTip.UseAnimation = false;
                _toolTip.UseFading = false;
                _toolTip.Popup += new PopupEventHandler(ToolTip_Popup);
                _toolTip.Draw += new DrawToolTipEventHandler(ToolTip_Draw);
            }

            return _toolTip;
        }

        void ToolTip_Popup(object sender, System.Windows.Forms.PopupEventArgs e)
        {
            e.ToolTipSize = _tipInfo.Size;
        }

        void ToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            _tipInfo.Paint(e.Graphics);
        }

        public void Show(TipInfo tipInfo)
        {
            if (_tipInfo != null)
            {
                Hide();
            }

            _tipInfo = tipInfo;

            var toolTip = GetToolTip();

            toolTip.Show("?", _tipInfo.Owner, new Point(0, _tipInfo.Owner.Height));
        }

        public void Hide()
        {
            if (_tipInfo != null)
            {
                var toolTip = GetToolTip();

                toolTip.Hide(_tipInfo.Owner);

                _tipInfo = null;
            }
        }

        public bool Active
        {
            get { return _tipInfo != null; }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_toolTip != null)
                {
                    _toolTip.Dispose();
                }

                _disposed = true;
            }
        }

        public TipInfo TipInfo
        {
            get { return _tipInfo; }
        }
    }

    internal class TransparentToolTip : System.Windows.Forms.ToolTip
    {
        protected override CreateParams CreateParams
        {
            get
            {
                // The tooltip is forced transparent no to make it transparent but
                // because transparent windows automatically propagate mouse click
                // events back to the parent. So, when the user clicks on the
                // tooltip, he actually clicks on the form.

                var createParams = base.CreateParams;

                createParams.ExStyle |= (int)NativeMethods.WS_EX_TRANSPARENT;

                return createParams;
            }
        }
    }
}
