using IWshRuntimeLibrary;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace Windows_Menu_Programs_Adder;

internal sealed partial class MainForm : Form
{
    BindingList<WProgram> programs = new BindingList<WProgram>();
    BindingList<WProgram> checkedPrograms = new BindingList<WProgram>();
    Dictionary<int, string> customeNames = new Dictionary<int, string>();

    public MainForm()
    {
        InitializeComponent();

        ((ListBox)ProgramsCheckedListBox).DataSource = programs;
        ((ListBox)ProgramsCheckedListBox).DisplayMember = "Name";
        ((ListBox)ProgramsCheckedListBox).ValueMember = "Path";
        CheckedProgramsListBox.DataSource = checkedPrograms;
        CheckedProgramsListBox.DisplayMember = "Name";
        CheckedProgramsListBox.ValueMember = "Path";
        MenuComboBox.SelectedIndex = 0;
        if (!IsAdministrator())
            AllUsersCheckBox.Text += " (requires Admin)";
        else
            AllUsersCheckBox.Checked = true;
        ShortcutTextBoxSetDefaultValue();
        FillPrograms();

        string[] args = Environment.GetCommandLineArgs();
        if ((IsRunInsideVS() && args.Length >= 2) || (!IsRunInsideVS() && args.Length >= 1))
        {
            List<string> argsCheckedSplit = new List<string>();
            if (IsRunInsideVS())
                argsCheckedSplit = args[1].Split('?').ToList();
            else
                argsCheckedSplit = args[0].Split('?').ToList();
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

            if ((IsRunInsideVS() && args.Length >= 3) || (!IsRunInsideVS() && args.Length >= 2))
            {
                List<string> argsNamesSplit = new List<string>();
                if (IsRunInsideVS())
                    argsNamesSplit = args[2].Split('?').ToList();
                else
                    argsNamesSplit = args[1].Split('?').ToList();

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
        RegistryKey apps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths", false);
        foreach (string keyname in apps.GetSubKeyNames())
        {
            RegistryKey app = apps.OpenSubKey(keyname);
            if (app == null)
                continue;
            object? value = app.GetValue("");
            if (value == null)
                continue;
            string path = value.ToString().TrimStart('"').TrimEnd('"');
            List<WProgram> found = programs.Where(x => x.Path == path).ToList();
            if (found.Count != 0)
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

        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Taskband", true))
        {
            key.SetValue(wProgram.Name, shortcutPath);
        }
    }

    void AddToDesktop(WProgram wProgram, string name)
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

    void AddToStartMenu(WProgram wProgram, string name)
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
            WindowStyle = ProcessWindowStyle.Hidden
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
                    string name;
                    customeNames.TryGetValue(checkedPrograms.IndexOf(wProgram), out name);
                    AddToDesktop(wProgram, name);
                }
                break;
            case 1:
                foreach (WProgram wProgram in checkedPrograms)
                {
                    string name;
                    customeNames.TryGetValue(checkedPrograms.IndexOf(wProgram), out name);
                    AddToStartMenu(wProgram, name);
                }
                break;
            case 2:
                foreach (WProgram wProgram in checkedPrograms)
                {
                    string name;
                    customeNames.TryGetValue(checkedPrograms.IndexOf(wProgram), out name);
                    PinToTaskBar(wProgram, name);
                }
                RestartExplorer(); break;
        }
    }

    private void MenuComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (MenuComboBox.SelectedIndex)
        {
            default: AllUsersCheckBox.Visible = true; break;
            case 2: AllUsersCheckBox.Visible = false; break;
        }
    }

    private void ProgramsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        ShortcutLabel.Visible = !(ProgramsCheckedListBox.CheckedItems.Count <= 1 && e.NewValue == CheckState.Unchecked);
        ShortcutTextBox.Visible = !(ProgramsCheckedListBox.CheckedItems.Count <= 1 && e.NewValue == CheckState.Unchecked);

        if (e.NewValue == CheckState.Checked)
            checkedPrograms.Add((WProgram)ProgramsCheckedListBox.Items[e.Index]);
        else
            checkedPrograms.Remove((WProgram)ProgramsCheckedListBox.Items[e.Index]);
    }

    private void ProgramsCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ProgramsCheckedListBox.SelectedIndex = -1;
    }

    private void CheckedProgramsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name;
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
            ShortcutTextBox.Text = "";
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
        string path = ((WProgram)CheckedProgramsListBox.SelectedItem).Path;
        Process.Start("explorer.exe", "/select, \"" + path + "\"");
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
                Arguments = $"\"{string.Join("?", checkedPrograms)}\" \"{string.Join("?", customeNames)}\""
            }
        };
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

    bool IsRunInsideVS()
    {
        return Environment.GetCommandLineArgs().Contains(Assembly.GetExecutingAssembly().Location);
    }
}