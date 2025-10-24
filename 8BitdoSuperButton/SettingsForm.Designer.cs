namespace _8BitdoSuperButton
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.audioDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.leftKeyComboBox = new System.Windows.Forms.ComboBox();
            this.rightKeyComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.audioDeviceLabel = new System.Windows.Forms.Label();
            this.leftKeyLabel = new System.Windows.Forms.Label();
            this.rightKeyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // audioDeviceLabel
            // 
            this.audioDeviceLabel.AutoSize = true;
            this.audioDeviceLabel.Location = new System.Drawing.Point(12, 15);
            this.audioDeviceLabel.Name = "audioDeviceLabel";
            this.audioDeviceLabel.Size = new System.Drawing.Size(98, 13);
            this.audioDeviceLabel.TabIndex = 0;
            this.audioDeviceLabel.Text = "Audio Input Device:";
            // 
            // audioDeviceComboBox
            // 
            this.audioDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.audioDeviceComboBox.FormattingEnabled = true;
            this.audioDeviceComboBox.Location = new System.Drawing.Point(116, 12);
            this.audioDeviceComboBox.Name = "audioDeviceComboBox";
            this.audioDeviceComboBox.Size = new System.Drawing.Size(256, 21);
            this.audioDeviceComboBox.TabIndex = 1;
            // 
            // leftKeyLabel
            // 
            this.leftKeyLabel.AutoSize = true;
            this.leftKeyLabel.Location = new System.Drawing.Point(12, 42);
            this.leftKeyLabel.Name = "leftKeyLabel";
            this.leftKeyLabel.Size = new System.Drawing.Size(85, 13);
            this.leftKeyLabel.TabIndex = 2;
            this.leftKeyLabel.Text = "Left Button Key:";
            // 
            // leftKeyComboBox
            // 
            this.leftKeyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leftKeyComboBox.FormattingEnabled = true;
            this.leftKeyComboBox.Location = new System.Drawing.Point(116, 39);
            this.leftKeyComboBox.Name = "leftKeyComboBox";
            this.leftKeyComboBox.Size = new System.Drawing.Size(256, 21);
            this.leftKeyComboBox.TabIndex = 3;
            // 
            // rightKeyLabel
            // 
            this.rightKeyLabel.AutoSize = true;
            this.rightKeyLabel.Location = new System.Drawing.Point(12, 69);
            this.rightKeyLabel.Name = "rightKeyLabel";
            this.rightKeyLabel.Size = new System.Drawing.Size(92, 13);
            this.rightKeyLabel.TabIndex = 4;
            this.rightKeyLabel.Text = "Right Button Key:";
            // 
            // rightKeyComboBox
            // 
            this.rightKeyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rightKeyComboBox.FormattingEnabled = true;
            this.rightKeyComboBox.Location = new System.Drawing.Point(116, 66);
            this.rightKeyComboBox.Name = "rightKeyComboBox";
            this.rightKeyComboBox.Size = new System.Drawing.Size(256, 21);
            this.rightKeyComboBox.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(15, 100);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 150);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.rightKeyComboBox);
            this.Controls.Add(this.rightKeyLabel);
            this.Controls.Add(this.leftKeyComboBox);
            this.Controls.Add(this.leftKeyLabel);
            this.Controls.Add(this.audioDeviceComboBox);
            this.Controls.Add(this.audioDeviceLabel);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Icon = new System.Drawing.Icon("icon.ico");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox audioDeviceComboBox;
        private System.Windows.Forms.ComboBox leftKeyComboBox;
        private System.Windows.Forms.ComboBox rightKeyComboBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label audioDeviceLabel;
        private System.Windows.Forms.Label leftKeyLabel;
        private System.Windows.Forms.Label rightKeyLabel;
    }
}
