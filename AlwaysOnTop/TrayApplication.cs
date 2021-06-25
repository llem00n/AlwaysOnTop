using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MouseKeyboardLibrary;

namespace AlwaysOnTop
{
    public class TrayApplication : ApplicationContext
    {
        private NotifyIcon notifyicon;
        private MouseHook mouseHandler;
        private string fileToRun;

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
            Thread.Sleep(500);
            System.Diagnostics.Process.Start(this.fileToRun);
            mouseHandler.Stop();
        }

        private void onDiscardClicked(object sender, EventArgs e)
        {
            this.fileToRun = System.IO.Directory.GetCurrentDirectory() + "\\res\\Discard.exe";
            mouseHandler.Start();
        }

        private void onApplyClicked(object sender, EventArgs e)
        {
            this.fileToRun = System.IO.Directory.GetCurrentDirectory() + "\\res\\Apply.exe";
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
