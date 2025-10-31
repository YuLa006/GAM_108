using System.IO;
using UnityEngine;

public class Account
{
    public string ID;
    public string Name;
    public int Diamond;
    public int LevelMax;
}
public class AccountManager : MonoBehaviour
{
    private string path;

    private void Start()
    {
        path = @"D:\GAM105\GAM105 - Lập trình game 2D\Projects\OnTapCode\Assets\Account\AccountData.txt";

        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }
    }

    public void AddAccount(string Id, string name, int diamond, int levelMax)
    {
        path = @"D:\GAM105\GAM105 - Lập trình game 2D\Projects\OnTapCode\Assets\Account\AccountData.txt";
            
        var line = Id +"\t"+name+"\t"+diamond+"\t"+levelMax;
        File.AppendAllText(path, line +"\n");
    }

    public void ReadAccount()
    {
        path = @"D:\GAM105\GAM105 - Lập trình game 2D\Projects\OnTapCode\Assets\Account\AccountData.txt";
        
        var line = File.ReadAllLines(path);
        for (int i = 0; i < line.Length; i++)
        {
            var temp = line[i].Split('\t');

        }
    }
}
