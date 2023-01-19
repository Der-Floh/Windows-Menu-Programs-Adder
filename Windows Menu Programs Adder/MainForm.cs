using IWshRuntimeLibrary;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;

namespace Windows_Menu_Programs_Adder;

public partial class MainForm : Form
{
    BindingList<WProgram> programs = new BindingList<WProgram>();
    BindingList<WProgram> startMenuPrograms = new BindingList<WProgram>();
    BindingList<WProgram> taskBarPrograms = new BindingList<WProgram>();

    public MainForm()
    {
        InitializeComponent();
        ((ListBox)ProgramsCheckedListBox).DataSource = programs;
        ((ListBox)ProgramsCheckedListBox).DisplayMember = "Name";
        ((ListBox)ProgramsCheckedListBox).ValueMember = "Path";
        MenuComboBox.SelectedIndex = 0;
        FillPrograms();
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
            WProgram wProgram = new WProgram()
            {
                File = keyname,
                Path = value.ToString(),
            };
            programs.Add(wProgram);
        }
    }

    void PinToTaskBar(WProgram wProgram)
    {
        string file = wProgram.File;
        if (ProgramsCheckedListBox.CheckedItems.Count == 1)
            file = ShortcutTextBox.Text + wProgram.Type;
        string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Internet Explorer", "Quick Launch", "User Pinned", "TaskBar", ReplaceFileType(file, "lnk"));
        CreateShortcut(wProgram.Path, shortcutPath);

        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Taskband", true))
        {
            key.SetValue(wProgram.Name, shortcutPath);
        }
    }

    void AddToDesktop(WProgram wProgram)
    {
        string file = wProgram.File;
        if (ProgramsCheckedListBox.CheckedItems.Count == 1)
            file = ShortcutTextBox.Text + wProgram.Type;
        string shortcutPath;
        if (AllUsersCheckBox.Checked)
            shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), ReplaceFileType(file, "lnk"));
        else
            shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), ReplaceFileType(file, "lnk"));
        CreateShortcut(wProgram.Path, shortcutPath);
    }

    void AddToStartMenu(WProgram wProgram)
    {
        string file = wProgram.File;
        if (ProgramsCheckedListBox.CheckedItems.Count == 1)
            file = ShortcutTextBox.Text + wProgram.Type;
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
                foreach (WProgram wProgram in ProgramsCheckedListBox.CheckedItems)
                {
                    AddToDesktop(wProgram);
                }
                break;
            case 1:
                foreach (WProgram wProgram in ProgramsCheckedListBox.CheckedItems)
                {
                    AddToStartMenu(wProgram);
                }
                break;
            case 2:
                foreach (WProgram wProgram in ProgramsCheckedListBox.CheckedItems)
                {
                    PinToTaskBar(wProgram);
                }
                RestartExplorer(); break;
        }
    }

    private void MenuComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (MenuComboBox.SelectedIndex)
        {
            default: AllUsersCheckBox.Visible = false; break; //todo true
            case 2: AllUsersCheckBox.Visible = false; break;
        }
    }

    private void ProgramsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        ShortcutLabel.Visible = ProgramsCheckedListBox.CheckedItems.Count == 1;
        ShortcutTextBox.Visible = ProgramsCheckedListBox.CheckedItems.Count == 1;
    }
}