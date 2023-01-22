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
        MenuComboBox = new ComboBox();
        AddButton = new Button();
        AllUsersCheckBox = new CheckBox();
        ProgramsCheckedListBox = new CheckedListBox();
        ShortcutTextBox = new TextBox();
        ShortcutLabel = new Label();
        CheckedProgramsListBox = new ListBox();
        OpenPathButton = new Button();
        RefreshIconsButton = new Button();
        ChooseFolderButton = new Button();
        SuspendLayout();
        // 
        // MenuComboBox
        // 
        MenuComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        MenuComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        MenuComboBox.FormattingEnabled = true;
        MenuComboBox.Items.AddRange(new object[] { "Desktop", "Start Menu", "Startup", "Chosen Folder" });
        MenuComboBox.Location = new Point(324, 12);
        MenuComboBox.Name = "MenuComboBox";
        MenuComboBox.Size = new Size(134, 23);
        MenuComboBox.TabIndex = 2;
        MenuComboBox.SelectedIndexChanged += MenuComboBox_SelectedIndexChanged;
        // 
        // AddButton
        // 
        AddButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        AddButton.Cursor = Cursors.Hand;
        AddButton.Location = new Point(243, 11);
        AddButton.Name = "AddButton";
        AddButton.Size = new Size(75, 23);
        AddButton.TabIndex = 3;
        AddButton.Text = "Add to ->";
        AddButton.UseVisualStyleBackColor = true;
        AddButton.Click += AddButton_Click;
        // 
        // AllUsersCheckBox
        // 
        AllUsersCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        AllUsersCheckBox.AutoSize = true;
        AllUsersCheckBox.Location = new Point(346, 41);
        AllUsersCheckBox.Name = "AllUsersCheckBox";
        AllUsersCheckBox.Size = new Size(112, 19);
        AllUsersCheckBox.TabIndex = 4;
        AllUsersCheckBox.Text = "Add for all Users";
        AllUsersCheckBox.UseVisualStyleBackColor = true;
        AllUsersCheckBox.CheckedChanged += AllUsersCheckBox_CheckedChanged;
        // 
        // ProgramsCheckedListBox
        // 
        ProgramsCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        ProgramsCheckedListBox.CheckOnClick = true;
        ProgramsCheckedListBox.FormattingEnabled = true;
        ProgramsCheckedListBox.Location = new Point(12, 12);
        ProgramsCheckedListBox.Name = "ProgramsCheckedListBox";
        ProgramsCheckedListBox.Size = new Size(225, 292);
        ProgramsCheckedListBox.TabIndex = 5;
        ProgramsCheckedListBox.ItemCheck += ProgramsCheckedListBox_ItemCheck;
        ProgramsCheckedListBox.SelectedIndexChanged += ProgramsCheckedListBox_SelectedIndexChanged;
        // 
        // ShortcutTextBox
        // 
        ShortcutTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        ShortcutTextBox.Cursor = Cursors.IBeam;
        ShortcutTextBox.Location = new Point(332, 123);
        ShortcutTextBox.Name = "ShortcutTextBox";
        ShortcutTextBox.Size = new Size(126, 23);
        ShortcutTextBox.TabIndex = 6;
        ShortcutTextBox.Visible = false;
        ShortcutTextBox.TextChanged += ShortcutTextBox_TextChanged;
        ShortcutTextBox.Enter += ShortcutTextBox_Enter;
        ShortcutTextBox.Leave += ShortcutTextBox_Leave;
        // 
        // ShortcutLabel
        // 
        ShortcutLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        ShortcutLabel.AutoSize = true;
        ShortcutLabel.Location = new Point(243, 126);
        ShortcutLabel.Name = "ShortcutLabel";
        ShortcutLabel.Size = new Size(87, 15);
        ShortcutLabel.TabIndex = 7;
        ShortcutLabel.Text = "Shortcut Name";
        ShortcutLabel.Visible = false;
        // 
        // CheckedProgramsListBox
        // 
        CheckedProgramsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        CheckedProgramsListBox.FormattingEnabled = true;
        CheckedProgramsListBox.ItemHeight = 15;
        CheckedProgramsListBox.Location = new Point(243, 150);
        CheckedProgramsListBox.Name = "CheckedProgramsListBox";
        CheckedProgramsListBox.Size = new Size(215, 154);
        CheckedProgramsListBox.TabIndex = 8;
        CheckedProgramsListBox.SelectedIndexChanged += CheckedProgramsListBox_SelectedIndexChanged;
        // 
        // OpenPathButton
        // 
        OpenPathButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        OpenPathButton.Cursor = Cursors.Hand;
        OpenPathButton.Location = new Point(243, 93);
        OpenPathButton.Name = "OpenPathButton";
        OpenPathButton.Size = new Size(127, 23);
        OpenPathButton.TabIndex = 9;
        OpenPathButton.Text = "Show in Explorer";
        OpenPathButton.UseVisualStyleBackColor = true;
        OpenPathButton.Click += OpenPathButton_Click;
        // 
        // RefreshIconsButton
        // 
        RefreshIconsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        RefreshIconsButton.Cursor = Cursors.Hand;
        RefreshIconsButton.Location = new Point(243, 65);
        RefreshIconsButton.Name = "RefreshIconsButton";
        RefreshIconsButton.Size = new Size(103, 23);
        RefreshIconsButton.TabIndex = 10;
        RefreshIconsButton.Text = "Refresh Icons";
        RefreshIconsButton.UseVisualStyleBackColor = true;
        RefreshIconsButton.Click += RefreshIconsButton_Click;
        // 
        // ChooseFolderButton
        // 
        ChooseFolderButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        ChooseFolderButton.Cursor = Cursors.Hand;
        ChooseFolderButton.Location = new Point(352, 65);
        ChooseFolderButton.Name = "ChooseFolderButton";
        ChooseFolderButton.Size = new Size(106, 23);
        ChooseFolderButton.TabIndex = 11;
        ChooseFolderButton.Text = "Choose Folder";
        ChooseFolderButton.UseVisualStyleBackColor = true;
        ChooseFolderButton.Visible = false;
        ChooseFolderButton.Click += ChooseFolderButton_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(470, 313);
        Controls.Add(ChooseFolderButton);
        Controls.Add(RefreshIconsButton);
        Controls.Add(OpenPathButton);
        Controls.Add(CheckedProgramsListBox);
        Controls.Add(ShortcutLabel);
        Controls.Add(ShortcutTextBox);
        Controls.Add(ProgramsCheckedListBox);
        Controls.Add(AllUsersCheckBox);
        Controls.Add(AddButton);
        Controls.Add(MenuComboBox);
        MinimumSize = new Size(455, 317);
        Name = "MainForm";
        Text = "Windows Menu Programs Adder";
        FormClosing += MainForm_FormClosing;
        Load += MainForm_Load;
        ResumeLayout(false);
        PerformLayout();
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
}