using System;
using System.Drawing;
using System.Linq;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayApplicationContext());
        }
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
            enableDisableMenuItem.Checked = false;

            trayIcon = new NotifyIcon()
            {
                Icon = SystemIcons.Application,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    enableDisableMenuItem,
                    new MenuItem("Exit", OnExit)
                }),
                Visible = true,
                Text = "8BitdoSuperButton"
            };
        }

        private void OnToggleMonitoring(object sender, EventArgs e)
        {
            enableDisableMenuItem.Checked = !enableDisableMenuItem.Checked;
            if (enableDisableMenuItem.Checked)
            {
                audioMonitor.Start();
            }
            else
            {
                audioMonitor.Stop();
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

        private bool isLeftDown = false;
        private bool isRightDown = false;

        public AudioMonitor()
        {
            var enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                var caps = WaveIn.GetCapabilities(i);
                if (caps.ProductName.ToLower().Contains("line") && !caps.ProductName.ToLower().Contains("mic"))
                {
                    inputDeviceIndex = i;
                    break;
                }
            }

            if (inputDeviceIndex == -1)
            {
                Console.WriteLine("No suitable audio input device found.");
            }
        }

        public void Start()
        {
            if (inputDeviceIndex == -1) return;

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
                // 16-bit stereo has 4 bytes per sample (2 for left, 2 for right)
                short leftSample = (short)(args.Buffer[i] | (args.Buffer[i + 1] << 8));
                short rightSample = (short)(args.Buffer[i + 2] | (args.Buffer[i + 3] << 8));


                // Simple peak detection for left button
                if (leftSample < -20000 && !isLeftDown) {
                    Console.WriteLine("Left Pressed Down");
                    isLeftDown = true;
                } else if (leftSample > 20000 && isLeftDown)
                {
                    Console.WriteLine("Left Released");
                    isLeftDown = false;
                }

                // Simple peak detection for right button
                if (rightSample < -20000 && !isRightDown)
                {
                    Console.WriteLine("Right Pressed Down");
                    isRightDown = true;
                }
                else if (rightSample > 20000 && isRightDown)
                {
                    Console.WriteLine("Right Released");
                    isRightDown = false;
                }
            }
        }
    }
}