using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MouseKeyboardLibrary;
using Windows;


namespace AlwaysOnTop
{
    public class TrayApplication : ApplicationContext
    {
        private enum ActionType { Apply, Discard };

        private NotifyIcon notifyicon;
        private MouseHook mouseHandler;
        private ActionType action;

        public TrayApplication()
        {
            this.notifyicon = new NotifyIcon();
            this.notifyicon.Icon = new System.Drawing.Icon("res/icon.ico");
            this.notifyicon.Text = "Always on top";
            this.notifyicon.Visible = true;
            this.notifyicon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.notifyicon.ContextMenuStrip.Items.Add("Apply", null, onApplyClicked);
            this.notifyicon.ContextMenuStrip.Items.Add("Discard", null, onDiscardClicked);
            this.notifyicon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            this.notifyicon.ContextMenuStrip.Items.Add("Exit", null, onExitClicked);

            mouseHandler = new MouseHook();
            mouseHandler.MouseDown += new MouseEventHandler(mouseHandler_MouseDown);
        }

        private void mouseHandler_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Thread.Sleep(500);

                IntPtr window = WinApi.GetForegroundWindow();
                WinApi.SetWindowPos(window, action == ActionType.Apply ? WinApi.HWND_TOPMOST : WinApi.HWND_NOTOPMOST, 0, 0, 0, 0, WinApi.SWP_NOSIZE | WinApi.SWP_NOMOVE);
            }

            mouseHandler.Stop();
        }

        private void onDiscardClicked(object sender, EventArgs e)
        {
            action = ActionType.Discard;

            mouseHandler.Start();
        }

        private void onApplyClicked(object sender, EventArgs e)
        {
            action = ActionType.Apply;

            mouseHandler.Start();
        }

        private void onExitClicked(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        void Exit(object sender, EventArgs e)
        {
            notifyicon.Visible = false;

            Application.Exit();
        }
    }
}
