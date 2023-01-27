using Microsoft.Win32;

namespace Windows_Menu_Programs_Adder;

internal sealed class WProgram
{
    public string File { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Name { get => RemoveFileType(File); }
    public string DisplayName { get => GetDisplayName(Path); }
    public string Type { get => GetFileType(File); }
    public string FolderName { get => GetFolderName(Path); }
    public string SpecialName { get => Name + "     | " + DisplayName; }

    static string RemoveFileType(string path)
    {
        int index = path.LastIndexOf('.');
        if (index > 0)
        {
            return path[..index];
        }
        return path;
    }

    static string GetFileType(string path)
    {
        int index = path.LastIndexOf('.');
        if (index > 0)
        {
            return path[index..];
        }
        return string.Empty;
    }

    static string GetFolderName(string path)
    {
        string? directory = System.IO.Path.GetDirectoryName(path);
        string? name = string.Empty;
        if (directory != null)
            name = System.IO.Path.GetFileName(directory);
        if (name == null)
            name = string.Empty;
        return name;
    }

    static string GetDisplayName(string path)
    {
        RegistryKey? uapps = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
        if (uapps == null)
            return string.Empty;
        foreach (string keyname in uapps.GetSubKeyNames())
        {
            RegistryKey? uapp = uapps.OpenSubKey(keyname);
            if (uapp == null)
                continue;
            string? installLocation = uapp.GetValue("InstallLocation")?.ToString()?.TrimStart('"')?.TrimEnd('"');

            if (string.IsNullOrEmpty(installLocation))
                continue;

            if (!string.IsNullOrEmpty(installLocation) && path.Contains(installLocation))
            {
                return installLocation;
                string? displayName = uapp.GetValue("DisplayName")?.ToString()?.TrimStart('"')?.TrimEnd('"');
                if (!string.IsNullOrEmpty(displayName))
                    return displayName;
            }
        }
        return string.Empty;
    }

    public override string ToString()
    {
        return $"{File}|{Path}";
    }

}
