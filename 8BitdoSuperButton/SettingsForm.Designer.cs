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
            this.leftModifiersGroupBox = new System.Windows.Forms.GroupBox();
            this.leftShiftCheckBox = new System.Windows.Forms.CheckBox();
            this.leftAltCheckBox = new System.Windows.Forms.CheckBox();
            this.leftCtrlCheckBox = new System.Windows.Forms.CheckBox();
            this.rightModifiersGroupBox = new System.Windows.Forms.GroupBox();
            this.rightShiftCheckBox = new System.Windows.Forms.CheckBox();
            this.rightAltCheckBox = new System.Windows.Forms.CheckBox();
            this.rightCtrlCheckBox = new System.Windows.Forms.CheckBox();
            this.leftModifiersGroupBox.SuspendLayout();
            this.rightModifiersGroupBox.SuspendLayout();
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
            this.leftKeyComboBox.Size = new System.Drawing.Size(134, 21);
            this.leftKeyComboBox.TabIndex = 3;
            // 
            // rightKeyLabel
            // 
            this.rightKeyLabel.AutoSize = true;
            this.rightKeyLabel.Location = new System.Drawing.Point(12, 96);
            this.rightKeyLabel.Name = "rightKeyLabel";
            this.rightKeyLabel.Size = new System.Drawing.Size(92, 13);
            this.rightKeyLabel.TabIndex = 4;
            this.rightKeyLabel.Text = "Right Button Key:";
            // 
            // rightKeyComboBox
            // 
            this.rightKeyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rightKeyComboBox.FormattingEnabled = true;
            this.rightKeyComboBox.Location = new System.Drawing.Point(116, 93);
            this.rightKeyComboBox.Name = "rightKeyComboBox";
            this.rightKeyComboBox.Size = new System.Drawing.Size(134, 21);
            this.rightKeyComboBox.TabIndex = 5;
            // 
            // saveButton
            // 
            // moved down to sit below the larger modifier groups
            this.saveButton.Location = new System.Drawing.Point(15, 200);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // leftModifiersGroupBox
            // 
            this.leftModifiersGroupBox.Controls.Add(this.leftShiftCheckBox);
            this.leftModifiersGroupBox.Controls.Add(this.leftAltCheckBox);
            this.leftModifiersGroupBox.Controls.Add(this.leftCtrlCheckBox);
            this.leftModifiersGroupBox.Location = new System.Drawing.Point(256, 39);
            this.leftModifiersGroupBox.Name = "leftModifiersGroupBox";
            // Increased width so checkboxes have more room and don't overlap
            this.leftModifiersGroupBox.Size = new System.Drawing.Size(220, 56);
            this.leftModifiersGroupBox.TabIndex = 7;
            this.leftModifiersGroupBox.TabStop = false;
            this.leftModifiersGroupBox.Text = "Modifiers";
            // 
            // leftShiftCheckBox
            // 
            this.leftShiftCheckBox.AutoSize = true;
            // place modifiers in a single row with spacing for easier clicking
            this.leftShiftCheckBox.Location = new System.Drawing.Point(152, 22);
            this.leftShiftCheckBox.Name = "leftShiftCheckBox";
            this.leftShiftCheckBox.Size = new System.Drawing.Size(47, 17);
            this.leftShiftCheckBox.TabIndex = 2;
            this.leftShiftCheckBox.Text = "Shift";
            this.leftShiftCheckBox.UseVisualStyleBackColor = true;
            // 
            // leftAltCheckBox
            // 
            this.leftAltCheckBox.AutoSize = true;
            this.leftAltCheckBox.Location = new System.Drawing.Point(80, 22);
            this.leftAltCheckBox.Name = "leftAltCheckBox";
            this.leftAltCheckBox.Size = new System.Drawing.Size(38, 17);
            this.leftAltCheckBox.TabIndex = 1;
            this.leftAltCheckBox.Text = "Alt";
            this.leftAltCheckBox.UseVisualStyleBackColor = true;
            // 
            // leftCtrlCheckBox
            // 
            this.leftCtrlCheckBox.AutoSize = true;
            this.leftCtrlCheckBox.Location = new System.Drawing.Point(12, 22);
            this.leftCtrlCheckBox.Name = "leftCtrlCheckBox";
            this.leftCtrlCheckBox.Size = new System.Drawing.Size(41, 17);
            this.leftCtrlCheckBox.TabIndex = 0;
            this.leftCtrlCheckBox.Text = "Ctrl";
            this.leftCtrlCheckBox.UseVisualStyleBackColor = true;
            // 
            // rightModifiersGroupBox
            // 
            this.rightModifiersGroupBox.Controls.Add(this.rightShiftCheckBox);
            this.rightModifiersGroupBox.Controls.Add(this.rightAltCheckBox);
            this.rightModifiersGroupBox.Controls.Add(this.rightCtrlCheckBox);
            this.rightModifiersGroupBox.Location = new System.Drawing.Point(256, 93);
            this.rightModifiersGroupBox.Name = "rightModifiersGroupBox";
            // match the left group box size and layout
            this.rightModifiersGroupBox.Size = new System.Drawing.Size(220, 56);
            this.rightModifiersGroupBox.TabIndex = 8;
            this.rightModifiersGroupBox.TabStop = false;
            this.rightModifiersGroupBox.Text = "Modifiers";
            // 
            // rightShiftCheckBox
            // 
            this.rightShiftCheckBox.AutoSize = true;
            this.rightShiftCheckBox.Location = new System.Drawing.Point(152, 22);
            this.rightShiftCheckBox.Name = "rightShiftCheckBox";
            this.rightShiftCheckBox.Size = new System.Drawing.Size(47, 17);
            this.rightShiftCheckBox.TabIndex = 2;
            this.rightShiftCheckBox.Text = "Shift";
            this.rightShiftCheckBox.UseVisualStyleBackColor = true;
            // 
            // rightAltCheckBox
            // 
            this.rightAltCheckBox.AutoSize = true;
            this.rightAltCheckBox.Location = new System.Drawing.Point(80, 22);
            this.rightAltCheckBox.Name = "rightAltCheckBox";
            this.rightAltCheckBox.Size = new System.Drawing.Size(38, 17);
            this.rightAltCheckBox.TabIndex = 1;
            this.rightAltCheckBox.Text = "Alt";
            this.rightAltCheckBox.UseVisualStyleBackColor = true;
            // 
            // rightCtrlCheckBox
            // 
            this.rightCtrlCheckBox.AutoSize = true;
            this.rightCtrlCheckBox.Location = new System.Drawing.Point(12, 22);
            this.rightCtrlCheckBox.Name = "rightCtrlCheckBox";
            this.rightCtrlCheckBox.Size = new System.Drawing.Size(41, 17);
            this.rightCtrlCheckBox.TabIndex = 0;
            this.rightCtrlCheckBox.Text = "Ctrl";
            this.rightCtrlCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            // increased height so the modifier groups and Save button fit comfortably
            this.ClientSize = new System.Drawing.Size(500, 245);
            // make the form non-resizable
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Controls.Add(this.rightModifiersGroupBox);
            this.Controls.Add(this.leftModifiersGroupBox);
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
            this.leftModifiersGroupBox.ResumeLayout(false);
            this.leftModifiersGroupBox.PerformLayout();
            this.rightModifiersGroupBox.ResumeLayout(false);
            this.rightModifiersGroupBox.PerformLayout();
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
        private System.Windows.Forms.GroupBox leftModifiersGroupBox;
        private System.Windows.Forms.CheckBox leftShiftCheckBox;
        private System.Windows.Forms.CheckBox leftAltCheckBox;
        private System.Windows.Forms.CheckBox leftCtrlCheckBox;
        private System.Windows.Forms.GroupBox rightModifiersGroupBox;
        private System.Windows.Forms.CheckBox rightShiftCheckBox;
        private System.Windows.Forms.CheckBox rightAltCheckBox;
        private System.Windows.Forms.CheckBox rightCtrlCheckBox;
    }
}
