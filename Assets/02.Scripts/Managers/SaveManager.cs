using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveManager
{
    private string path = "";
    private readonly string fileName = "SAVE_DATA";

    public void Init()
    {
        path = Application.dataPath + "/SAVE_DATA_FILE";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    #region Save&Load
    public void SaveJson<T>(string createPath, string fileName, T value)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        string json = JsonUtility.ToJson(value, true);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public void SaveJson<T>(T value)
    {
        SaveJson<T>(path, fileName, value);
    }

    public T LoadJsonFile<T>(string loadPath, string fileName) where T : new()
    {
        if (File.Exists(string.Format("{0}/{1}.json", loadPath, fileName)))
        {
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<T>(jsonData);
        }
        SaveJson<T>(loadPath, fileName, new T());
        return LoadJsonFile<T>(loadPath, fileName);
    }

    public T LoadJsonFile<T>() where T : new()
    {
        return LoadJsonFile<T>(path, fileName);
    }
    #endregion
}
