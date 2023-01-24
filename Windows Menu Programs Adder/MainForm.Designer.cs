namespace Windows_Menu_Programs_Adder;

sealed partial class MainForm
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
            this.CheckedProgramsListBox = new System.Windows.Forms.ListBox();
            this.OpenPathButton = new System.Windows.Forms.Button();
            this.RefreshIconsButton = new System.Windows.Forms.Button();
            this.ChooseFolderButton = new System.Windows.Forms.Button();
            this.ChosenPathTextBox = new System.Windows.Forms.TextBox();
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
            "Startup",
            "Chosen Folder"});
            this.MenuComboBox.Location = new System.Drawing.Point(324, 12);
            this.MenuComboBox.Name = "MenuComboBox";
            this.MenuComboBox.Size = new System.Drawing.Size(134, 23);
            this.MenuComboBox.TabIndex = 2;
            this.MenuComboBox.SelectedIndexChanged += new System.EventHandler(this.MenuComboBox_SelectedIndexChanged);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddButton.Location = new System.Drawing.Point(243, 11);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 3;
            this.AddButton.Text = "Add to ->";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // AllUsersCheckBox
            // 
            this.AllUsersCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AllUsersCheckBox.AutoSize = true;
            this.AllUsersCheckBox.Location = new System.Drawing.Point(346, 41);
            this.AllUsersCheckBox.Name = "AllUsersCheckBox";
            this.AllUsersCheckBox.Size = new System.Drawing.Size(112, 19);
            this.AllUsersCheckBox.TabIndex = 4;
            this.AllUsersCheckBox.Text = "Add for all Users";
            this.AllUsersCheckBox.UseVisualStyleBackColor = true;
            this.AllUsersCheckBox.CheckedChanged += new System.EventHandler(this.AllUsersCheckBox_CheckedChanged);
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
            this.ProgramsCheckedListBox.Size = new System.Drawing.Size(225, 292);
            this.ProgramsCheckedListBox.TabIndex = 5;
            this.ProgramsCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ProgramsCheckedListBox_ItemCheck);
            this.ProgramsCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.ProgramsCheckedListBox_SelectedIndexChanged);
            // 
            // ShortcutTextBox
            // 
            this.ShortcutTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShortcutTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ShortcutTextBox.Location = new System.Drawing.Point(332, 123);
            this.ShortcutTextBox.Name = "ShortcutTextBox";
            this.ShortcutTextBox.Size = new System.Drawing.Size(126, 23);
            this.ShortcutTextBox.TabIndex = 6;
            this.ShortcutTextBox.Visible = false;
            this.ShortcutTextBox.TextChanged += new System.EventHandler(this.ShortcutTextBox_TextChanged);
            this.ShortcutTextBox.Enter += new System.EventHandler(this.ShortcutTextBox_Enter);
            this.ShortcutTextBox.Leave += new System.EventHandler(this.ShortcutTextBox_Leave);
            // 
            // ShortcutLabel
            // 
            this.ShortcutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShortcutLabel.AutoSize = true;
            this.ShortcutLabel.Location = new System.Drawing.Point(243, 126);
            this.ShortcutLabel.Name = "ShortcutLabel";
            this.ShortcutLabel.Size = new System.Drawing.Size(87, 15);
            this.ShortcutLabel.TabIndex = 7;
            this.ShortcutLabel.Text = "Shortcut Name";
            this.ShortcutLabel.Visible = false;
            // 
            // CheckedProgramsListBox
            // 
            this.CheckedProgramsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckedProgramsListBox.FormattingEnabled = true;
            this.CheckedProgramsListBox.ItemHeight = 15;
            this.CheckedProgramsListBox.Location = new System.Drawing.Point(243, 150);
            this.CheckedProgramsListBox.Name = "CheckedProgramsListBox";
            this.CheckedProgramsListBox.Size = new System.Drawing.Size(215, 154);
            this.CheckedProgramsListBox.TabIndex = 8;
            this.CheckedProgramsListBox.SelectedIndexChanged += new System.EventHandler(this.CheckedProgramsListBox_SelectedIndexChanged);
            // 
            // OpenPathButton
            // 
            this.OpenPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenPathButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenPathButton.Location = new System.Drawing.Point(243, 93);
            this.OpenPathButton.Name = "OpenPathButton";
            this.OpenPathButton.Size = new System.Drawing.Size(127, 23);
            this.OpenPathButton.TabIndex = 9;
            this.OpenPathButton.Text = "Show in Explorer";
            this.OpenPathButton.UseVisualStyleBackColor = true;
            this.OpenPathButton.Click += new System.EventHandler(this.OpenPathButton_Click);
            // 
            // RefreshIconsButton
            // 
            this.RefreshIconsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshIconsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RefreshIconsButton.Location = new System.Drawing.Point(243, 65);
            this.RefreshIconsButton.Name = "RefreshIconsButton";
            this.RefreshIconsButton.Size = new System.Drawing.Size(103, 23);
            this.RefreshIconsButton.TabIndex = 10;
            this.RefreshIconsButton.Text = "Refresh Icons";
            this.RefreshIconsButton.UseVisualStyleBackColor = true;
            this.RefreshIconsButton.Click += new System.EventHandler(this.RefreshIconsButton_Click);
            // 
            // ChooseFolderButton
            // 
            this.ChooseFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseFolderButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChooseFolderButton.Location = new System.Drawing.Point(352, 65);
            this.ChooseFolderButton.Name = "ChooseFolderButton";
            this.ChooseFolderButton.Size = new System.Drawing.Size(106, 23);
            this.ChooseFolderButton.TabIndex = 11;
            this.ChooseFolderButton.Text = "Choose Folder";
            this.ChooseFolderButton.UseVisualStyleBackColor = true;
            this.ChooseFolderButton.Visible = false;
            this.ChooseFolderButton.Click += new System.EventHandler(this.ChooseFolderButton_Click);
            // 
            // ChosenPathTextBox
            // 
            this.ChosenPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChosenPathTextBox.Location = new System.Drawing.Point(243, 37);
            this.ChosenPathTextBox.Name = "ChosenPathTextBox";
            this.ChosenPathTextBox.ReadOnly = true;
            this.ChosenPathTextBox.Size = new System.Drawing.Size(215, 23);
            this.ChosenPathTextBox.TabIndex = 12;
            this.ChosenPathTextBox.Visible = false;
            this.ChosenPathTextBox.TextChanged += new System.EventHandler(this.MenuComboBox_SelectedIndexChanged);
            this.ChosenPathTextBox.Enter += new System.EventHandler(this.ChosenPathTextBox_Enter);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 313);
            this.Controls.Add(this.ChooseFolderButton);
            this.Controls.Add(this.RefreshIconsButton);
            this.Controls.Add(this.OpenPathButton);
            this.Controls.Add(this.CheckedProgramsListBox);
            this.Controls.Add(this.ShortcutLabel);
            this.Controls.Add(this.ShortcutTextBox);
            this.Controls.Add(this.ProgramsCheckedListBox);
            this.Controls.Add(this.AllUsersCheckBox);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.MenuComboBox);
            this.Controls.Add(this.ChosenPathTextBox);
            this.MinimumSize = new System.Drawing.Size(455, 317);
            this.Name = "MainForm";
            this.Text = "Windows Menu Programs Adder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
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
    private ListBox CheckedProgramsListBox;
    private Button OpenPathButton;
    private Button RefreshIconsButton;
    private Button ChooseFolderButton;
    private TextBox ChosenPathTextBox;
}