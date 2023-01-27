using IWshRuntimeLibrary;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;

namespace Windows_Menu_Programs_Adder;

internal sealed partial class MainForm : Form
{
    BindingList<WProgram> programs = new BindingList<WProgram>();
    BindingList<WProgram> checkedPrograms = new BindingList<WProgram>();
    Dictionary<int, string> customeNames = new Dictionary<int, string>();
    public string ChosenFolderPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    public MainForm()
    {
        InitializeComponent();

        ((ListBox)ProgramsCheckedListBox).DataSource = programs;
        ((ListBox)ProgramsCheckedListBox).DisplayMember = "SpecialName";
        ((ListBox)ProgramsCheckedListBox).ValueMember = "Path";
        CheckedProgramsListBox.DataSource = checkedPrograms;
        CheckedProgramsListBox.DisplayMember = "Name";
        CheckedProgramsListBox.ValueMember = "Path";
        MenuComboBox.SelectedIndex = 0;
        ChosenPathTextBox.Text = ChosenFolderPath;
        if (!IsAdministrator())
            AllUsersCheckBox.Text += " (requires Admin)";
        else
            AllUsersCheckBox.Checked = true;
        ShortcutTextBoxSetDefaultValue();
        FillPrograms();

        string[] args = Environment.GetCommandLineArgs();
        if (args.Length >= 2)
        {
            List<string> argsCheckedSplit = new List<string>();
            argsCheckedSplit = args[1].Split('?').ToList();
            foreach (string programString in argsCheckedSplit)
            {
                string[] wProgramString = programString.Split("|");
                WProgram wProgram = new WProgram { File = wProgramString[0], Path = wProgramString[1] };

                int index = -1;
                try
                {
                    index = ProgramsCheckedListBox.Items.IndexOf(ProgramsCheckedListBox.Items.Cast<WProgram>().First(x => x.Path == wProgram.Path));
                }
                catch { }
                if (index == -1)
                    MessageBox.Show($"Path: '{wProgram.Path}' of Program '{wProgram.File}' wasn't found", "Not Found Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    ProgramsCheckedListBox.SetItemChecked(index, true);
            }

            if (args.Length >= 3)
            {
                List<string> argsNamesSplit = new List<string>();
                argsNamesSplit = args[2].Split('?').ToList();

                Dictionary<int, string> dictionary = new Dictionary<int, string>();
                foreach (var nameString in argsNamesSplit)
                {
                    string[] keyValue = nameString.TrimStart('[').TrimEnd(']').Split(',');
                    dictionary.Add(int.Parse(keyValue[0]), keyValue[1].Trim());
                }
                customeNames = dictionary;
                CheckedProgramsListBox_SelectedIndexChanged(this, new EventArgs());
            }
        }
    }

    void FillPrograms()
    {
        programs.Clear();
        RegistryKey? apps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths");
        if (apps == null)
            return;
        foreach (string keyname in apps.GetSubKeyNames())
        {
            RegistryKey? app = apps.OpenSubKey(keyname);
            if (app == null)
                continue;
            object? value = app.GetValue(string.Empty);
            if (value == null)
                continue;
            string? path = value.ToString()?.TrimStart('"')?.TrimEnd('"');
            List<WProgram> found = programs.Where(x => x.Path == path).ToList();
            if (found.Count != 0 || string.IsNullOrEmpty(path))
                continue;
            WProgram wProgram = new WProgram()
            {
                File = keyname,
                Path = path,
            };
            programs.Add(wProgram);
        }
    }

    void PinToTaskBar(WProgram wProgram, string name)
    {
        string file;
        if (string.IsNullOrEmpty(name))
            file = wProgram.File;
        else
            file = name + wProgram.Type;
        string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Internet Explorer", "Quick Launch", "User Pinned", "TaskBar", ReplaceFileType(file, "lnk"));
        CreateShortcut(wProgram.Path, shortcutPath);

        using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Taskband", true))
        {
            if (key != null)
                key.SetValue(wProgram.Name, shortcutPath);
        }
    }

    void AddToDesktop(WProgram wProgram, string? name)
    {
        string file;
        if (string.IsNullOrEmpty(name))
            file = wProgram.File;
        else
            file = name + wProgram.Type;
        string shortcutPath;
        if (AllUsersCheckBox.Checked)
            shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), ReplaceFileType(file, "lnk"));
        else
            shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ReplaceFileType(file, "lnk"));
        CreateShortcut(wProgram.Path, shortcutPath);
    }

    void AddToStartMenu(WProgram wProgram, string? name)
    {
        string file;
        if (string.IsNullOrEmpty(name))
            file = wProgram.File;
        else
            file = name + wProgram.Type;
        string shortcutPath;
        if (AllUsersCheckBox.Checked)
            shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), ReplaceFileType(file, "lnk"));
        else
            shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), ReplaceFileType(file, "lnk"));
        CreateShortcut(wProgram.Path, shortcutPath);
    }

    void AddToStartUp(WProgram wProgram, string? name)
    {
        string file;
        if (string.IsNullOrEmpty(name))
            file = wProgram.File;
        else
            file = name + wProgram.Type;
        string shortcutPath;
        if (AllUsersCheckBox.Checked)
            shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup), ReplaceFileType(file, "lnk"));
        else
            shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), ReplaceFileType(file, "lnk"));
        CreateShortcut(wProgram.Path, shortcutPath);
    }

    void AddToCustom(WProgram wProgram, string? name, string shortcutPath)
    {
        string file;
        if (string.IsNullOrEmpty(name))
            file = wProgram.File;
        else
            file = name + wProgram.Type;
        CreateShortcut(wProgram.Path, shortcutPath);
    }

    string ReplaceFileType(string path, string newFileType)
    {
        int index = path.LastIndexOf('.');
        if (index > 0)
        {
            return path.Substring(0, index) + "." + newFileType;
        }
        return path;
    }


    void CreateShortcut(string targetPath, string shortcutPath)
    {
        WshShell shell = new WshShell();
        IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
        shortcut.TargetPath = targetPath;
        shortcut.Save();
    }

    void RestartExplorer()
    {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = "taskkill.exe",
            Arguments = "-f -im explorer.exe",
            WindowStyle = ProcessWindowStyle.Hidden,
            CreateNoWindow = true
        };
        process.Start();
        process.WaitForExit();
        Process.Start("explorer.exe");
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        switch (MenuComboBox.SelectedIndex)
        {
            case 0:
                foreach (WProgram wProgram in checkedPrograms)
                {
                    string? name;
                    customeNames.TryGetValue(checkedPrograms.IndexOf(wProgram), out name);
                    AddToDesktop(wProgram, name);
                }
                break;
            case 1:
                foreach (WProgram wProgram in checkedPrograms)
                {
                    string? name;
                    customeNames.TryGetValue(checkedPrograms.IndexOf(wProgram), out name);
                    AddToStartMenu(wProgram, name);
                }
                break;
            case 2:
                foreach (WProgram wProgram in checkedPrograms)
                {
                    string? name;
                    customeNames.TryGetValue(checkedPrograms.IndexOf(wProgram), out name);
                    AddToStartUp(wProgram, name);
                }
                break;
            case 3:
                foreach (WProgram wProgram in checkedPrograms)
                {
                    string? name;
                    customeNames.TryGetValue(checkedPrograms.IndexOf(wProgram), out name);
                    string file;
                    if (string.IsNullOrEmpty(name))
                        file = wProgram.File;
                    else
                        file = name + wProgram.Type;
                    string shortcutPath = Path.Combine(ChosenFolderPath, ReplaceFileType(file, "lnk"));
                    CreateShortcut(wProgram.Path, shortcutPath);
                }
                break;
        }
        foreach (int itemIndex in ProgramsCheckedListBox.CheckedIndices)
            ProgramsCheckedListBox.SetItemChecked(itemIndex, false);
    }

    private void MenuComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (MenuComboBox.SelectedIndex)
        {
            default: AllUsersCheckBox.Visible = true; ChooseFolderButton.Visible = false; ChosenPathTextBox.Visible = false; break;
            case 3: AllUsersCheckBox.Visible = false; ChooseFolderButton.Visible = true; ChosenPathTextBox.Visible = true; break;
        }
    }

    private void ProgramsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        ShortcutLabel.Visible = !(ProgramsCheckedListBox.CheckedItems.Count <= 1 && e.NewValue == CheckState.Unchecked);
        ShortcutTextBox.Visible = !(ProgramsCheckedListBox.CheckedItems.Count <= 1 && e.NewValue == CheckState.Unchecked);

        if (e.NewValue == CheckState.Checked)
            checkedPrograms.Add((WProgram)ProgramsCheckedListBox.Items[e.Index]);
        else
        {
            WProgram wProgramToDelete = (WProgram)ProgramsCheckedListBox.Items[e.Index];
            customeNames.Remove(checkedPrograms.IndexOf(wProgramToDelete));
            checkedPrograms.Remove(wProgramToDelete);
        }
    }

    private void ProgramsCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ProgramsCheckedListBox.SelectedIndex = -1;
    }

    private void CheckedProgramsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        string? name;
        customeNames.TryGetValue(CheckedProgramsListBox.SelectedIndex, out name);
        ShortcutTextBox.Text = name;

        ShortcutTextBoxSetDefaultValue();
    }

    private void ShortcutTextBox_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ShortcutTextBox.Text) || ShortcutTextBox.Text == "(Default)")
            customeNames.Remove(CheckedProgramsListBox.SelectedIndex);
        else
            customeNames[CheckedProgramsListBox.SelectedIndex] = ShortcutTextBox.Text;
    }

    private void ShortcutTextBox_Enter(object sender, EventArgs e)
    {
        if (ShortcutTextBox.Text == "(Default)")
        {
            ShortcutTextBox.ForeColor = Color.Black;
            ShortcutTextBox.Text = string.Empty;
        }
        if (!string.IsNullOrEmpty(ShortcutTextBox.Text) && ShortcutTextBox.Text != "(Default)")
            BeginInvoke(delegate { ((TextBox)sender).SelectAll(); });
    }

    private void ShortcutTextBox_Leave(object sender, EventArgs e)
    {
        ShortcutTextBoxSetDefaultValue();
    }

    void ShortcutTextBoxSetDefaultValue()
    {
        if (string.IsNullOrWhiteSpace(ShortcutTextBox.Text))
        {
            ShortcutTextBox.ForeColor = Color.Gray;
            ShortcutTextBox.Text = "(Default)";
        }
        else if (ShortcutTextBox.Text == "(Default)")
            ShortcutTextBox.ForeColor = Color.Gray;
        else if (ShortcutTextBox.Text != "(Default)")
            ShortcutTextBox.ForeColor = Color.Black;
    }

    private void OpenPathButton_Click(object sender, EventArgs e)
    {
        if (CheckedProgramsListBox.SelectedItem != null)
        {
            string path = ((WProgram)CheckedProgramsListBox.SelectedItem).Path;
            Process.Start("explorer.exe", "/select, \"" + path + "\"");
        }
    }

    bool IsAdministrator()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    bool StartAsAdmin(string fileName)
    {
        DialogResult result = MessageBox.Show("This can only be used when running as Administrator. Restart program as Administrator?", "Restart as Admin?", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        if (result == DialogResult.No)
            return false;
        string test = string.Join("?", customeNames);
        Process process = new Process
        {
            StartInfo =
            {
                FileName = fileName,
                UseShellExecute = true,
                Verb = "runas",
            }
        };
        if (checkedPrograms.Count != 0 && customeNames.Count == 0)
            process.StartInfo.Arguments = $"\"{string.Join("?", checkedPrograms)}\"";
        else if (checkedPrograms.Count != 0 && customeNames.Count != 0)
            process.StartInfo.Arguments = $"\"{string.Join("?", checkedPrograms)}\" \"{string.Join("?", customeNames)}\"";
        try
        {
            process.Start();
        }
        catch
        {
            return false;
        }
        Close();
        return true;
    }

    private void AllUsersCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (AllUsersCheckBox.Checked && !IsAdministrator())
            if (!StartAsAdmin(Application.ExecutablePath))
                AllUsersCheckBox.Checked = false;
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        if (Properties.Settings.Default.Maximised)
        {
            Location = Properties.Settings.Default.Location;
            WindowState = FormWindowState.Maximized;
            Size = Properties.Settings.Default.Size;
        }
        else
        {

            Size = Properties.Settings.Default.Size;
            if (Properties.Settings.Default.Location.IsEmpty)
                Location = new Point(Screen.FromControl(this).Bounds.Width / 2 - Size.Width / 2, Screen.FromControl(this).Bounds.Height / 2 - Size.Height / 2);
            else
                Location = Properties.Settings.Default.Location;
        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (WindowState == FormWindowState.Normal)
        {
            Properties.Settings.Default.Location = Location;
            Properties.Settings.Default.Size = Size;
            Properties.Settings.Default.Maximised = false;
        }
        else
        {
            Properties.Settings.Default.Location = RestoreBounds.Location;
            Properties.Settings.Default.Size = RestoreBounds.Size;
            Properties.Settings.Default.Maximised = WindowState == FormWindowState.Maximized;
        }
        Properties.Settings.Default.Save();
    }

    private void RefreshIconsButton_Click(object sender, EventArgs e)
    {
        System.IO.File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IconCache.db"));
        RestartExplorer();
    }

    private void ChooseFolderButton_Click(object sender, EventArgs e)
    {
        using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
        {
            if (!string.IsNullOrEmpty(ChosenFolderPath))
                folderBrowserDialog.InitialDirectory = ChosenFolderPath;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                ChosenFolderPath = folderBrowserDialog.SelectedPath;
                ChosenPathTextBox.Text = ChosenFolderPath;
            }
        }
    }

    private void ChosenPathTextBox_Enter(object sender, EventArgs e)
    {
        BeginInvoke(delegate { ((TextBox)sender).SelectAll(); });
    }
}