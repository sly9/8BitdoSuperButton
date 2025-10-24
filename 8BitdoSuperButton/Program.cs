using System;
using System.Drawing;
using System.Linq;
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
        private AudioMonitor audioMonitor;

        public TrayApplicationContext()
        {
            audioMonitor = new AudioMonitor();

            enableDisableMenuItem = new MenuItem("Enable", OnToggleMonitoring);
            enableDisableMenuItem.Checked = Properties.Settings.Default.Enabled;

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

        private bool isLeftPressed = false;
        private bool isRightPressed = false;

        public AudioMonitor()
        {
            inputDeviceIndex = Properties.Settings.Default.AudioDevice;
            leftKey = Properties.Settings.Default.LeftKey;
            rightKey = Properties.Settings.Default.RightKey;
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
                    KeySimulator.SendKeyPress(leftKey);

                }
                else if (leftSample > 20000 && isLeftPressed)
                {
                    Console.WriteLine("Left Released");
                    isLeftPressed = false;
                    KeySimulator.SendKeyRelease(leftKey);
                }

                // Right Channel

                if (rightSample < -20000 && !isRightPressed)
                {
                    Console.WriteLine("Right Pressed Down");
                    isRightPressed = true;
                    KeySimulator.SendKeyPress(rightKey);

                }
                else if (rightSample > 20000 && isRightPressed)
                {
                    Console.WriteLine("Right Released");
                    isRightPressed = false;
                    KeySimulator.SendKeyRelease(rightKey);
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

        public static void SendKeyPress(Keys key)
        {
            INPUT[] inputs = new INPUT[]
            {
                new INPUT
                {
                    type = INPUT_KEYBOARD,
                    u = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = (ushort)key,
                            wScan = 0,
                            dwFlags = 0,
                            time = 0,
                            dwExtraInfo = GetMessageExtraInfo(),
                        }
                    }
                }
            };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void SendKeyRelease(Keys key)
        {
            INPUT[] inputs = new INPUT[]
            {
                new INPUT
                {
                    type = INPUT_KEYBOARD,
                    u = new InputUnion
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = (ushort)key,
                            wScan = 0,
                            dwFlags = KEYEVENTF_KEYUP,
                            time = 0,
                            dwExtraInfo = GetMessageExtraInfo(),
                        }
                    }
                }
            };

            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
