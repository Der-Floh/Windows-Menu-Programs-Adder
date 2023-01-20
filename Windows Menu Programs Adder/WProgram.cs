namespace Windows_Menu_Programs_Adder;

internal sealed class WProgram
{
    public string File { get; set; }
    public string Path { get; set; }
    public string Name { get => RemoveFileType(File); }
    public string Type { get => GetFileType(File); }

    string RemoveFileType(string path)
    {
        int index = path.LastIndexOf('.');
        if (index > 0)
        {
            return path.Substring(0, index);
        }
        return path;
    }

    string GetFileType(string path)
    {
        int index = path.LastIndexOf('.');
        if (index > 0)
        {
            return path.Substring(index);
        }
        return "";
    }

    public override string ToString()
    {
        return $"{File}|{Path}";
    }

}
