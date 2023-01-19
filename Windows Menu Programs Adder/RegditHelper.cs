using Microsoft.Win32;

namespace Windows_Menu_Programs_Adder;

public static class RegeditHelper
{
    public static string ReadCurrentUserKeyValue(in string path, in string keyName)
    {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(path, false);

        string data = key.GetValue(keyName).ToString();

        key.Close();
        return data;
    }

    public static string ReadLocalMachineKeyValue(in string path, in string keyName)
    {
        RegistryKey key = Registry.LocalMachine.OpenSubKey(path, false);

        string data = key.GetValue(keyName).ToString();

        key.Close();
        return data;
    }

    public static void SetCurrentUserKeyValue(in string path, in string keyName, in string value)
    {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);

        key.SetValue(keyName, value);

        key.Close();
    }

    public static void SetLocalMachineKeyValue(in string path, in string keyName, in string value)
    {
        RegistryKey key = Registry.LocalMachine.OpenSubKey(path, true);

        key.SetValue(keyName, value);

        key.Close();
    }
}
