using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MiniForms
{
    internal class Program
    {
        static Icon icon;
        static void Main(string[] args)
        {
            MyForm form = new MyForm();

            icon = Icons.LoadApplicationIcon(form.Handle);

            form.Title = "Example App";
            form.icon = icon;
            form.Window_Rect = new RECT(0, 0, 600, 400);

            Application.Run(form);
        }
    }
    public class MyForm : Form
    {
        [DllImport("user32.dll")]
        static extern int DrawTextEx(IntPtr hdc, StringBuilder lpchText, int cchText,
   ref RECT lprc, uint dwDTFormat, ref DRAWTEXTPARAMS lpDTParams);

        public MyForm()
        {
            new Thread( () => {
                Thread.Sleep(150);
                Invalidate();
            }).Start();
        }

        public override void OnPaint(IntPtr hdc, RECT rect)
        {

            DRAWTEXTPARAMS p = new DRAWTEXTPARAMS();
            p.cbSize = (uint)Marshal.SizeOf(typeof(DRAWTEXTPARAMS));
            DrawTextEx(hdc, new StringBuilder("TEXT!"), -1, ref rect, 0, ref p);
        }
    }
}
