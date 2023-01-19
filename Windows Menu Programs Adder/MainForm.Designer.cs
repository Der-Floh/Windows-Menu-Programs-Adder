namespace Windows_Menu_Programs_Adder
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuComboBox = new System.Windows.Forms.ComboBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.AllUsersCheckBox = new System.Windows.Forms.CheckBox();
            this.ProgramsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ShortcutTextBox = new System.Windows.Forms.TextBox();
            this.ShortcutLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MenuComboBox
            // 
            this.MenuComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MenuComboBox.FormattingEnabled = true;
            this.MenuComboBox.Items.AddRange(new object[] {
            "Desktop",
            "Start Menu",
            "Taskbar"});
            this.MenuComboBox.Location = new System.Drawing.Point(293, 120);
            this.MenuComboBox.Name = "MenuComboBox";
            this.MenuComboBox.Size = new System.Drawing.Size(134, 23);
            this.MenuComboBox.TabIndex = 2;
            this.MenuComboBox.SelectedIndexChanged += new System.EventHandler(this.MenuComboBox_SelectedIndexChanged);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.Location = new System.Drawing.Point(212, 119);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 3;
            this.AddButton.Text = "Add to ->";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // AllUsersCheckBox
            // 
            this.AllUsersCheckBox.AutoSize = true;
            this.AllUsersCheckBox.Location = new System.Drawing.Point(293, 149);
            this.AllUsersCheckBox.Name = "AllUsersCheckBox";
            this.AllUsersCheckBox.Size = new System.Drawing.Size(112, 19);
            this.AllUsersCheckBox.TabIndex = 4;
            this.AllUsersCheckBox.Text = "Add for all Users";
            this.AllUsersCheckBox.UseVisualStyleBackColor = true;
            // 
            // ProgramsCheckedListBox
            // 
            this.ProgramsCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgramsCheckedListBox.CheckOnClick = true;
            this.ProgramsCheckedListBox.FormattingEnabled = true;
            this.ProgramsCheckedListBox.Location = new System.Drawing.Point(12, 12);
            this.ProgramsCheckedListBox.Name = "ProgramsCheckedListBox";
            this.ProgramsCheckedListBox.Size = new System.Drawing.Size(194, 256);
            this.ProgramsCheckedListBox.TabIndex = 5;
            this.ProgramsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ProgramsCheckedListBox_ItemCheck);
            // 
            // ShortcutTextBox
            // 
            this.ShortcutTextBox.Location = new System.Drawing.Point(301, 91);
            this.ShortcutTextBox.Name = "ShortcutTextBox";
            this.ShortcutTextBox.Size = new System.Drawing.Size(126, 23);
            this.ShortcutTextBox.TabIndex = 6;
            this.ShortcutTextBox.Visible = false;
            // 
            // ShortcutLabel
            // 
            this.ShortcutLabel.AutoSize = true;
            this.ShortcutLabel.Location = new System.Drawing.Point(212, 94);
            this.ShortcutLabel.Name = "ShortcutLabel";
            this.ShortcutLabel.Size = new System.Drawing.Size(83, 15);
            this.ShortcutLabel.TabIndex = 7;
            this.ShortcutLabel.Text = "Shortcut Label";
            this.ShortcutLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 278);
            this.Controls.Add(this.ShortcutLabel);
            this.Controls.Add(this.ShortcutTextBox);
            this.Controls.Add(this.ProgramsCheckedListBox);
            this.Controls.Add(this.AllUsersCheckBox);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.MenuComboBox);
            this.Name = "MainForm";
            this.Text = "Windows Menu Programs Adder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComboBox MenuComboBox;
        private Button AddButton;
        private CheckBox AllUsersCheckBox;
        private CheckedListBox ProgramsCheckedListBox;
        private TextBox ShortcutTextBox;
        private Label ShortcutLabel;
    }
}