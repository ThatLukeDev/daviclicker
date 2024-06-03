using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autohold
{
    public partial class Form1 : Form
    {
        bool autoclicking = false;
        bool cooldown = false;
        int interval = 10;
        IntPtr lParamD = IntPtr.Zero;
        IntPtr lParamU = IntPtr.Zero;

        public delegate IntPtr HookCallbackDelegate(int nCode, IntPtr wParam, IntPtr lParam);

        const int MOUSEEVENTF_LEFTDOWN = 0x02;
        const int MOUSEEVENTF_LEFTUP = 0x04;
        const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        const int MOUSEEVENTF_RIGHTUP = 0x10;
        [DllImport("user32.dll", SetLastError = true)]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookCallbackDelegate lpfn, IntPtr wParam, uint lParam);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        const int WH_KEYBOARD_LL = 13;
        const int WH_MOUSE_LL = 14;
        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        const int WM_ALTKEYDOWN = 0x0104;
        const int WM_ALTKEYUP = 0x0105;
        const int WM_MOUSEMOVE = 0x0200;
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_LBUTTONUP = 0x0202;
        const int WM_MOUSEWHEEL = 0x020A;
        const int WM_RBUTTONDOWN = 0x0204;
        const int WM_RBUTTONUP = 0x0205;
        const int WM_LBUTTONDBLCLK = 0x0203;
        const int WM_MBUTTONDOWN = 0x0207;
        const int WM_MBUTTONUP = 0x020;

        HookCallbackDelegate mhcDelegate;
        IntPtr whllmousehook;

        IntPtr mhandler(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (lParamD == IntPtr.Zero || lParam == lParamD)
            {
                if (wParam == (IntPtr)WM_LBUTTONDOWN)
                {
                    if (lParamD == IntPtr.Zero)
                    {
                        lParamD = lParam;
                    }
                    if (enabled.Checked && !cooldown)
                    {
                        autoclicking = true;
                    }
                }
            }

            if (lParamU == IntPtr.Zero || lParam == lParamU)
            {
                if (wParam == (IntPtr)WM_LBUTTONUP)
                {
                    if (lParamU == IntPtr.Zero)
                    {
                        lParamU = lParam;
                    }
                    autoclicking = false;
                    cooldown = true;
                }
            }

            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        public void startListening()
        {
            mhcDelegate = mhandler;

            string mainModuleName = Process.GetCurrentProcess().MainModule.ModuleName;
            whllmousehook = SetWindowsHookEx(WH_MOUSE_LL, mhcDelegate, GetModuleHandle(mainModuleName), 0);
        }
        public void endListening()
        {
            UnhookWindowsHookEx(whllmousehook);
            mhcDelegate = null;
        }

        int cps = 0;
        int count = 0;

        Timer secondInterval;
        Timer msInterval;
        Timer cdInterval;

        private void secondInterval_Tick(object sender, EventArgs e)
        {
            cps = count;
            count = 0;
            view.Text = string.Format("{0:00} cps", cps);
        }

        private void msInterval_Tick(object sender, EventArgs e)
        {
            if (autoclicking && enabled.Checked)
            {
                Click(5);
            }
        }

        private void cdInterval_Tick(object sender, EventArgs e)
        {
            cooldown = false;
        }

        public Form1()
        {
            InitializeComponent();
            startListening();
            secondInterval = new Timer();
            secondInterval.Tick += new EventHandler(secondInterval_Tick);
            secondInterval.Interval = 1000;
            secondInterval.Start();
            msInterval = new Timer();
            msInterval.Tick += new EventHandler(msInterval_Tick);
            msInterval.Interval = interval;
            msInterval.Start();
            cdInterval = new Timer();
            cdInterval.Tick += new EventHandler(cdInterval_Tick);
            cdInterval.Interval = 50;
            cdInterval.Start();
        }

        private void tester_Click(object sender, EventArgs e)
        {
            count++;
        }

        private async void Click(int delay)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
            await Task.Delay(delay);
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
        }
    }
}
