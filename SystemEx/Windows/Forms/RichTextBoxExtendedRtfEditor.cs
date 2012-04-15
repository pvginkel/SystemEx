//
// RichTextBoxExtended
// Written to support a word processing style toolbar by Richard Parsons
//		(http://www.codeproject.com/script/profile/whos_who.asp?id=419005)
// Extended to support form designer persistance and data binding by Declan Brennan
//		(http://www.codeproject.com/script/profile/whos_who.asp?id=495690)
//

using System.Text;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace SystemEx.Windows.Forms
{

	class RichTextBoxExtendedRtfEditor:System.Drawing.Design.UITypeEditor
	{
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context==null)
				return base.GetEditStyle(null);
			return System.Drawing.Design.UITypeEditorEditStyle.Modal;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context!=null && provider!=null)
			{
					IWindowsFormsEditorService edSrv= (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (edSrv!=null)
				{
					RichTextBoxExtendedRtfEditorForm dialog= new RichTextBoxExtendedRtfEditorForm();
					if (value is String)
						dialog.Value= (string)value;	
					if (edSrv.ShowDialog(dialog)==System.Windows.Forms.DialogResult.OK)
						value= dialog.Value;
					dialog.Dispose();
					dialog= null;
				}
			}
			return value;
		}
	}
}
