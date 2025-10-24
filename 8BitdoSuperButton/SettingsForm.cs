using System;
using System.Linq;
using System.Windows.Forms;
using NAudio.Wave;

namespace _8BitdoSuperButton
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Populate audio devices
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                var caps = WaveIn.GetCapabilities(i);
                audioDeviceComboBox.Items.Add(caps.ProductName);
            }

            // Populate key codes
            var keyValues = Enum.GetValues(typeof(Keys)).Cast<Keys>().ToList();
            leftKeyComboBox.DataSource = keyValues.ToList();
            rightKeyComboBox.DataSource = keyValues.ToList();

            // TODO: Create and use settings in the project's Properties.Settings.settings file
            // Setting: AudioDevice (int)
            // Setting: LeftKey (System.Windows.Forms.Keys)
            // Setting: RightKey (System.Windows.Forms.Keys)
            // audioDeviceComboBox.SelectedIndex = Properties.Settings.Default.AudioDevice;
            // leftKeyComboBox.SelectedItem = Properties.Settings.Default.LeftKey;
            // rightKeyComboBox.SelectedItem = Properties.Settings.Default.RightKey;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // TODO: Create and use settings in the project's Properties.Settings.settings file
             Properties.Settings.Default.AudioDevice = audioDeviceComboBox.SelectedIndex;
            Properties.Settings.Default.LeftKey = (Keys)leftKeyComboBox.SelectedItem;
            Properties.Settings.Default.RightKey = (Keys)rightKeyComboBox.SelectedItem;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
