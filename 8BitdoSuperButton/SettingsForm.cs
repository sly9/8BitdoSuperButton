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

            // Create and use settings in the project's Properties.Settings.settings file
            // Setting: AudioDevice (int)
            // Setting: LeftKey (System.Windows.Forms.Keys)
            // Setting: RightKey (System.Windows.Forms.Keys)
            // Setting: LeftCtrl (bool)
            // Setting: LeftAlt (bool)
            // Setting: LeftShift (bool)
            // Setting: RightCtrl (bool)
            // Setting: RightAlt (bool)
            // Setting: RightShift (bool)
            audioDeviceComboBox.SelectedIndex = Properties.Settings.Default.AudioDevice;
            leftKeyComboBox.SelectedItem = Properties.Settings.Default.LeftKey;
            rightKeyComboBox.SelectedItem = Properties.Settings.Default.RightKey;
            leftCtrlCheckBox.Checked = Properties.Settings.Default.LeftCtrl;
            leftAltCheckBox.Checked = Properties.Settings.Default.LeftAlt;
            leftShiftCheckBox.Checked = Properties.Settings.Default.LeftShift;
            rightCtrlCheckBox.Checked = Properties.Settings.Default.RightCtrl;
            rightAltCheckBox.Checked = Properties.Settings.Default.RightAlt;
            rightShiftCheckBox.Checked = Properties.Settings.Default.RightShift;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // TODO: Create and use settings in the project's Properties.Settings.settings file
             Properties.Settings.Default.AudioDevice = audioDeviceComboBox.SelectedIndex;
            Properties.Settings.Default.LeftKey = (Keys)leftKeyComboBox.SelectedItem;
            Properties.Settings.Default.RightKey = (Keys)rightKeyComboBox.SelectedItem;
            Properties.Settings.Default.LeftCtrl = leftCtrlCheckBox.Checked;
            Properties.Settings.Default.LeftAlt = leftAltCheckBox.Checked;
            Properties.Settings.Default.LeftShift = leftShiftCheckBox.Checked;
            Properties.Settings.Default.RightCtrl = rightCtrlCheckBox.Checked;
            Properties.Settings.Default.RightAlt = rightAltCheckBox.Checked;
            Properties.Settings.Default.RightShift = rightShiftCheckBox.Checked;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
