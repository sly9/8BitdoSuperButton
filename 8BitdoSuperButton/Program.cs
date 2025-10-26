using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace _8BitdoSuperButton
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Enable DPI awareness
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayApplicationContext());
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }

    public class TrayApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private MenuItem enableDisableMenuItem;
        private MenuItem startWithWindowsMenuItem;
        private AudioMonitor audioMonitor;

        public TrayApplicationContext()
        {
            audioMonitor = new AudioMonitor();

            enableDisableMenuItem = new MenuItem("Enable", OnToggleMonitoring);
            enableDisableMenuItem.Checked = Properties.Settings.Default.Enabled;

            startWithWindowsMenuItem = new MenuItem("Start with Windows", OnStartWithWindows);
            startWithWindowsMenuItem.Checked = IsStartupEnabled();

            // Create DPI-aware tray icon
            Icon trayIconIcon;
            try
            {
                // Try to load a high-resolution icon for better DPI support
                trayIconIcon = new Icon("icon.ico", new Size(32, 32));
            }
            catch
            {
                // Fallback to default icon loading
                trayIconIcon = new Icon("icon.ico");
            }

            trayIcon = new NotifyIcon()
            {
                Icon = trayIconIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    enableDisableMenuItem,
                    startWithWindowsMenuItem,
                    new MenuItem("Settings", OnSettings),
                    new MenuItem("Exit", OnExit)
                }),
                Visible = true,
                Text = "8BitdoSuperButton"
            };

            if (Properties.Settings.Default.Enabled)
            {
                audioMonitor.Start();
            }
        }

        private void OnToggleMonitoring(object sender, EventArgs e)
        {
            enableDisableMenuItem.Checked = !enableDisableMenuItem.Checked;
            Properties.Settings.Default.Enabled = enableDisableMenuItem.Checked;
            Properties.Settings.Default.Save();
            if (enableDisableMenuItem.Checked)
            {
                audioMonitor.Start();
            }
            else
            {
                audioMonitor.Stop();
            }
        }

        private void OnStartWithWindows(object sender, EventArgs e)
        {
            startWithWindowsMenuItem.Checked = !startWithWindowsMenuItem.Checked;
            SetStartup(startWithWindowsMenuItem.Checked);
        }

        private bool IsStartupEnabled()
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false))
            {
                return rk.GetValue(Application.ProductName) != null;
            }
        }

        private void SetStartup(bool isStartupEnabled)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (isStartupEnabled)
                {
                    rk.SetValue(Application.ProductName, Application.ExecutablePath);
                }
                else
                {
                    rk.DeleteValue(Application.ProductName, false);
                }
            }
        }

        private void OnSettings(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm())
            {
                settingsForm.ShowDialog();
            }
            // After settings are saved, we need to re-initialize the audio monitor
            audioMonitor.Stop();
            audioMonitor = new AudioMonitor();
            if(enableDisableMenuItem.Checked)
            {
                audioMonitor.Start();
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            audioMonitor.Stop();
            Application.Exit();
        }
    }

    public class AudioMonitor
    {
        private WaveInEvent waveIn;
        private int inputDeviceIndex = -1;
        private Keys leftKey;
        private Keys rightKey;
        private bool leftCtrl;
        private bool leftAlt;
        private bool leftShift;
        private bool rightCtrl;
        private bool rightAlt;
        private bool rightShift;

        private bool isLeftPressed = false;
        private bool isRightPressed = false;

        public AudioMonitor()
        {
            inputDeviceIndex = Properties.Settings.Default.AudioDevice;
            leftKey = Properties.Settings.Default.LeftKey;
            rightKey = Properties.Settings.Default.RightKey;
            leftCtrl = Properties.Settings.Default.LeftCtrl;
            leftAlt = Properties.Settings.Default.LeftAlt;
            leftShift = Properties.Settings.Default.LeftShift;
            rightCtrl = Properties.Settings.Default.RightCtrl;
            rightAlt = Properties.Settings.Default.RightAlt;
            rightShift = Properties.Settings.Default.RightShift;
        }

        public void Start()
        {
            waveIn = new WaveInEvent
            {
                DeviceNumber = inputDeviceIndex,
                WaveFormat = new WaveFormat(8000, 16, 2) // 8kHz, 16-bit, stereo
            };
            waveIn.DataAvailable += OnDataAvailable;
            waveIn.StartRecording();
        }

        public void Stop()
        {
            waveIn?.StopRecording();
            waveIn?.Dispose();
            waveIn = null;
        }

        private void OnDataAvailable(object sender, WaveInEventArgs args)
        {
            for (int i = 0; i < args.BytesRecorded; i += 4)
            {
                short leftSample = (short)(args.Buffer[i] | (args.Buffer[i + 1] << 8));
                short rightSample = (short)(args.Buffer[i + 2] | (args.Buffer[i + 3] << 8));


                // Left channel
                if (leftSample < -20000 && !isLeftPressed)
                {
                    Console.WriteLine("Left Pressed Down");
                    isLeftPressed = true;
                    var keys = new List<Keys>();
                    if (leftCtrl) keys.Add(Keys.ControlKey);
                    if (leftAlt) keys.Add(Keys.Menu);
                    if (leftShift) keys.Add(Keys.ShiftKey);
                    keys.Add(leftKey);
                    KeySimulator.SendKeyPress(keys.ToArray());

                }
                else if (leftSample > 20000 && isLeftPressed)
                {
                    Console.WriteLine("Left Released");
                    isLeftPressed = false;
                    var keys = new List<Keys>();
                    if (leftCtrl) keys.Add(Keys.ControlKey);
                    if (leftAlt) keys.Add(Keys.Menu);
                    if (leftShift) keys.Add(Keys.ShiftKey);
                    keys.Add(leftKey);
                    KeySimulator.SendKeyRelease(keys.ToArray());
                }

                // Right Channel

                if (rightSample < -20000 && !isRightPressed)
                {
                    Console.WriteLine("Right Pressed Down");
                    isRightPressed = true;
                    var keys = new List<Keys>();
                    if (rightCtrl) keys.Add(Keys.ControlKey);
                    if (rightAlt) keys.Add(Keys.Menu);
                    if (rightShift) keys.Add(Keys.ShiftKey);
                    keys.Add(rightKey);
                    KeySimulator.SendKeyPress(keys.ToArray());

                }
                else if (rightSample > 20000 && isRightPressed)
                {
                    Console.WriteLine("Right Released");
                    isRightPressed = false;
                    var keys = new List<Keys>();
                    if (rightCtrl) keys.Add(Keys.ControlKey);
                    if (rightAlt) keys.Add(Keys.Menu);
                    if (rightShift) keys.Add(Keys.ShiftKey);
                    keys.Add(rightKey);
                    KeySimulator.SendKeyRelease(keys.ToArray());
                }
            }
        }
    }

    public static class KeySimulator
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public uint type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        private const uint INPUT_KEYBOARD = 1;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        public static void SendKeyPress(params Keys[] keys)
        {
            if (keys == null || keys.Length == 0) return;

            INPUT[] inputs = new INPUT[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                inputs[i] = new INPUT
                {
                    type = INPUT_KEYBOARD,
                    u = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = (ushort)keys[i],
                            wScan = 0,
                            dwFlags = 0,
                            time = 0,
                            dwExtraInfo = GetMessageExtraInfo(),
                        }
                    }
                };
            }
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void SendKeyRelease(params Keys[] keys)
        {
            if (keys == null || keys.Length == 0) return;

            INPUT[] inputs = new INPUT[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                inputs[i] = new INPUT
                {
                    type = INPUT_KEYBOARD,
                    u = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = (ushort)keys[i],
                            wScan = 0,
                            dwFlags = KEYEVENTF_KEYUP,
                            time = 0,
                            dwExtraInfo = GetMessageExtraInfo(),
                        }
                    }
                };
            }
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
