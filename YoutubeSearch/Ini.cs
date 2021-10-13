using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public partial class Ini
{
    public static string FilePath { get; set; }   // iniファイルパス

    [DllImport("KERNEL32.DLL", CharSet = CharSet.Auto)]
    public static extern uint GetPrivateProfileString(
        string lpAppName, string lpKeyName,
        string lpDefault, StringBuilder lpReturnedString,
        uint nSize, string lpFileName);

    [DllImport("KERNEL32.DLL", CharSet = CharSet.Auto)]
    public static extern uint GetPrivateProfileInt(
        string lpAppName, string lpKeyName,
        int nDefault, string lpFileName);

    [DllImport("KERNEL32.DLL")]
    public static extern uint WritePrivateProfileString(
        string lpAppName, string lpKeyName,
        string lpString, string lpFileName);

    /// <summary>
    /// iniファイル指定
    /// </summary>
    /// <param name="path"></param>
    public Ini(string path = "")
    {
        if (path == "")
        {
            // パス指定なし
            // 製品名.ini に設定
            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SettingInfo.ini");
        }
        else
        {
            // パス指定あり
            FilePath = path;
        }
    }

    /// <summary>
    /// 値の取得（string）
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetValueString(string section, string key)
    {
        var sb = new StringBuilder(1024);
        GetPrivateProfileString(section, key, "", sb, (uint)(sb.Capacity), FilePath);
        return sb.ToString();
    }

    /// <summary>
    /// 値の取得（int）
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static int GetValueInt(string section, string key)
    {
        return (int)GetPrivateProfileInt(section, key, 0, FilePath);
    }

    /// <summary>
    /// 値の設定（string）
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <param name="val"></param>
    public static void SetValue(string section, string key, string val)
    {
        WritePrivateProfileString(section, key, val, FilePath);
    }

    /// <summary>
    /// 値の設定（int）
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <param name="val"></param>
    public static void SetValue(string section, string key, int val)
    {
        SetValue(section, key, val.ToString());
    }
}
