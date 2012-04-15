/*
 * Copyright © 2007 Hedley Muscroft
 * 
 * Design Notes:-
 * --------------
 * References:
 * - http://www.codeproject.com/KB/vista/Vista_TaskDialog_Wrapper.aspx
 * 
 * Revision Control:-
 * ------------------
 * Created On: 2007 November 26
 * 
 * $Revision:$
 * $LastChangedDate:$
 */

using System.Text;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace SystemEx.Windows.Forms
{
    [DesignTimeVisible(false)]
    public partial class EmulateCommandLink : Button
    {
        enum ButtonState { Normal, MouseOver, Down }

        bool m_autoHeight = true;
        ButtonState buttonState = ButtonState.Normal;
        Image imgArrowNormal = null;
        Image imgArrowHovered = null;

        // Make sure the control is invalidated(repainted) when the text is changed.
        public override string Text { get { return base.Text; } set { base.Text = value; this.Invalidate ( ); } }

        public bool AutoHeight { get { return m_autoHeight; } set { m_autoHeight = value; if ( m_autoHeight ) this.Invalidate ( ); } }
        public Font SmallFont { get; set; }

        //--------------------------------------------------------------------------------
        public EmulateCommandLink ( )
        {
            InitializeComponent ( );
            base.Font = MessageForm.LargeThemedFont;
            SmallFont = MessageForm.ThemedFont;
        }

        //--------------------------------------------------------------------------------
        protected override void OnCreateControl ( )
        {
            base.OnCreateControl ( );
            imgArrowNormal = Properties.Resources.ArrowNormal;
            imgArrowHovered = Properties.Resources.ArrowHovered;
        }

        //--------------------------------------------------------------------------------
        const int LEFT_MARGIN = 10;
        const int TOP_MARGIN = 10;
        const int ARROW_WIDTH = 19;

        string GetLargeText ( )
        {
            string[] lines = this.Text.Split ( new char[] { '\n' } );
            return lines[0];
        }

        string GetSmallText ( )
        {
            if ( this.Text.IndexOf ( '\n' ) < 0 )
                return "";

            string s = this.Text;
            string[] lines = s.Split ( new char[] { '\n' } );
            s = "";
            for ( int i = 1; i < lines.Length; i++ )
                s += lines[i] + "\n";
            return s.Trim ( new char[] { '\n' } );
        }

        SizeF GetLargeTextSizeF ( )
        {
            int x = LEFT_MARGIN + ARROW_WIDTH + 5;
            SizeF mzSize = new SizeF ( this.Width - x - LEFT_MARGIN, 5000.0F );  // presume RIGHT_MARGIN = LEFT_MARGIN
            Graphics g = Graphics.FromHwnd ( this.Handle );
            SizeF textSize = g.MeasureString ( GetLargeText ( ), base.Font, mzSize );
            return textSize;
        }

        SizeF GetSmallTextSizeF ( )
        {
            string s = GetSmallText ( );
            if ( s == "" ) return new SizeF ( 0, 0 );
            int x = LEFT_MARGIN + ARROW_WIDTH + 8; // <- indent small text slightly more
            SizeF mzSize = new SizeF ( this.Width - x - LEFT_MARGIN, 5000.0F );  // presume RIGHT_MARGIN = LEFT_MARGIN
            Graphics g = Graphics.FromHwnd ( this.Handle );
            SizeF textSize = g.MeasureString ( s, SmallFont, mzSize );
            return textSize;
        }

        public int GetBestHeight ( )
        {
            //return 40;
            return ( TOP_MARGIN * 2 ) + (int)GetSmallTextSizeF ( ).Height + (int)GetLargeTextSizeF ( ).Height;
        }

        //--------------------------------------------------------------------------------
        protected override void OnPaint ( PaintEventArgs e )
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            LinearGradientBrush brush;
            LinearGradientMode mode = LinearGradientMode.Vertical;

            Rectangle newRect = new Rectangle ( ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1 );
            Color text_color = SystemColors.WindowText;

            Image img = imgArrowNormal;

            if ( Enabled )
            {
                switch ( buttonState )
                {
                    case ButtonState.Normal:
                        e.Graphics.FillRectangle ( Brushes.White, newRect );
                        if ( base.Focused )
                            e.Graphics.DrawRectangle ( new Pen ( Color.SkyBlue, 1 ), newRect );
                        else
                            e.Graphics.DrawRectangle ( new Pen ( Color.White, 1 ), newRect );
                        text_color = Color.DarkBlue;
                        break;

                    case ButtonState.MouseOver:
                        brush = new LinearGradientBrush ( newRect, Color.White, Color.WhiteSmoke, mode );
                        e.Graphics.FillRectangle ( brush, newRect );
                        e.Graphics.DrawRectangle ( new Pen ( Color.Silver, 1 ), newRect );
                        img = imgArrowHovered;
                        text_color = Color.Blue;
                        break;

                    case ButtonState.Down:
                        brush = new LinearGradientBrush ( newRect, Color.WhiteSmoke, Color.White, mode );
                        e.Graphics.FillRectangle ( brush, newRect );
                        e.Graphics.DrawRectangle ( new Pen ( Color.DarkGray, 1 ), newRect );
                        text_color = Color.DarkBlue;
                        break;
                }
            }
            else
            {
                brush = new LinearGradientBrush ( newRect, Color.WhiteSmoke, Color.Gainsboro, mode );
                e.Graphics.FillRectangle ( brush, newRect );
                e.Graphics.DrawRectangle ( new Pen ( Color.DarkGray, 1 ), newRect );
                text_color = Color.DarkBlue;
            }


            string largetext = this.GetLargeText ( );
            string smalltext = this.GetSmallText ( );

            SizeF szL = GetLargeTextSizeF ( );
            e.Graphics.DrawString ( largetext, base.Font, new SolidBrush ( text_color ), new RectangleF ( new PointF ( LEFT_MARGIN + imgArrowNormal.Width + 5, TOP_MARGIN ), szL ) );

            if ( smalltext != "" )
            {
                SizeF szS = GetSmallTextSizeF ( );
                e.Graphics.DrawString ( smalltext, SmallFont, new SolidBrush ( text_color ), new RectangleF ( new PointF ( LEFT_MARGIN + imgArrowNormal.Width + 8, TOP_MARGIN + (int)szL.Height ), szS ) );
            }

            e.Graphics.DrawImage ( img, new Point ( LEFT_MARGIN, TOP_MARGIN + (int)( szL.Height / 2 ) - (int)( img.Height / 2 ) ) );
        }

        //--------------------------------------------------------------------------------
        protected override void OnMouseLeave ( System.EventArgs e )
        {
            buttonState = ButtonState.Normal;
            this.Invalidate ( );
            base.OnMouseLeave ( e );
        }

        //--------------------------------------------------------------------------------
        protected override void OnMouseEnter ( System.EventArgs e )
        {
            buttonState = ButtonState.MouseOver;
            this.Invalidate ( );
            base.OnMouseEnter ( e );
        }

        //--------------------------------------------------------------------------------
        protected override void OnMouseUp ( System.Windows.Forms.MouseEventArgs e )
        {
            buttonState = ButtonState.MouseOver;
            this.Invalidate ( );
            base.OnMouseUp ( e );
        }

        //--------------------------------------------------------------------------------
        protected override void OnMouseDown ( System.Windows.Forms.MouseEventArgs e )
        {
            buttonState = ButtonState.Down;
            this.Invalidate ( );
            base.OnMouseDown ( e );
        }

        //--------------------------------------------------------------------------------
        protected override void OnSizeChanged ( EventArgs e )
        {
            if ( m_autoHeight )
            {
                int h = GetBestHeight ( );
                if ( this.Height != h )
                {
                    this.Height = h;
                    return;
                }
            }
            base.OnSizeChanged ( e );
        }

        //--------------------------------------------------------------------------------
    }
}
