using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class TaskbarLocation
    {
        private TaskbarLocation(Rectangle taskbar, Rectangle workArea, TaskbarLocationEdge edge)
        {
            Taskbar = taskbar;
            WorkArea = workArea;
            Edge = edge;
        }

        public static TaskbarLocation Detect()
        {
            var abd = new NativeMethods.APPBARDATA();

            abd.cbSize = Marshal.SizeOf(abd);

            Win32.NativeMethods.SHAppBarMessage(Win32.NativeMethods.ABM_GETTASKBARPOS, ref abd);

            var taskbar = (Rectangle)abd.rc;

            var p = new Point(
                abd.rc.left + (abd.rc.right - abd.rc.left) / 2,
                abd.rc.top + (abd.rc.bottom - abd.rc.top) / 2
            );

            var workArea = Screen.GetWorkingArea(p);

            // Windows 7 reports the work area as including the taskbar, but we need
            // it exclusing the taskbar. Correct.

            if (workArea.Contains(p))
            {
                var pMon = new Point(
			        workArea.Left + workArea.Width / 2,
			        workArea.Top + workArea.Height / 2
		        );

                // Whether the taskbar is horizontal or vertical.

                if ((abd.rc.right - abd.rc.left) > (abd.rc.bottom - abd.rc.top))
                {
                    // Whether the baskbar is at the top or the bottom.

                    if (p.Y < pMon.Y)
                    {
                        workArea = new Rectangle(
                            workArea.Left, abd.rc.bottom,
                            workArea.Width, workArea.Bottom - abd.rc.bottom
                        );
                    }
                    else
                    {
                        workArea = new Rectangle(
                            workArea.Left, workArea.Top,
                            workArea.Width, abd.rc.top
                        );
                    }
                }
                else
                {
                    // Whether the taskbar is at the left or right.

                    if (p.X < pMon.X)
                    {
                        workArea = new Rectangle(
                            abd.rc.right, workArea.Top,
                            workArea.Right - abd.rc.right, workArea.Height
                        );
                    }
                    else
                    {
                        workArea = new Rectangle(
                            workArea.Left, abd.rc.left,
                            workArea.Top, workArea.Height
                        );
                    }
                }
            }

            var pBar = new Point(
                taskbar.Left + taskbar.Width / 2,
                taskbar.Top + taskbar.Height / 2
            );

            TaskbarLocationEdge edge;

            if (pBar.X > workArea.Right)
            {
                edge = TaskbarLocationEdge.Right;
            }
            else if (pBar.X < workArea.Left)
            {
                edge = TaskbarLocationEdge.Left;
            }
            else if (pBar.Y > workArea.Bottom)
            {
                edge = TaskbarLocationEdge.Bottom;
            }
            else if (pBar.Y < workArea.Top)
            {
                edge = TaskbarLocationEdge.Top;
            }
            else
            {
                edge = TaskbarLocationEdge.Bottom;
            }

            return new TaskbarLocation(taskbar, workArea, edge);
        }

        public Rectangle Taskbar { get; private set; }

        public Rectangle WorkArea { get; private set; }

        public TaskbarLocationEdge Edge { get; private set; }
    }

    public enum TaskbarLocationEdge
    {
        Left,
        Top,
        Right,
        Bottom
    }
}
