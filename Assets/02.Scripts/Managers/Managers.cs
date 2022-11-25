using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Managers : MonoSingleton<Managers>
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region CORE
    PoolManager _pool = new PoolManager();
    SoundManager _sound = new SoundManager();
    SceneManagerEX _scene = new SceneManagerEX();
    ResourceManager _resource = new ResourceManager();

    public static PoolManager Pool { get { return Instance._pool; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static SceneManagerEX Scene { get { return Instance._scene; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    #endregion

    private string PATH = "";
    private readonly string FILE_NAME = "SAVE_DATA";
    void Start()
    {
        PATH = Application.dataPath + "/SAVE_DATA_FILE";
        if (!Directory.Exists(PATH))
            Directory.CreateDirectory(PATH);

        Init();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
            s_instance._pool.Init();
        }
    }

    // 씬 전환 시 호출
    public static void Clear()
    {
        Scene.Clear();

        Pool.Clear();
    }

    #region Save&Load
    /// <summary>
    /// 세이브 함수
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="createPath"></param>
    /// <param name="fileName"></param>
    /// <param name="value"></param>
    public void SaveJson<T>(string createPath, string fileName, T value)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        string json = JsonUtility.ToJson(value, true);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    /// <summary>
    /// 세이브 함수. 근데 Managers에 있는 기본 파일 형식으로 저장
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    public void SaveJson<T>(T value)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", PATH, FILE_NAME), FileMode.Create);
        string json = JsonUtility.ToJson(value, true);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    /// <summary>
    /// 로드 함수
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="loadPath"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 로드 함수. 근데 Managers에 있는 기본 파일 형식으로 저장
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadJsonFile<T>() where T : new()
    {
        if (File.Exists(string.Format("{0}/{1}.json", PATH, FILE_NAME)))
        {
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", PATH, FILE_NAME), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<T>(jsonData);
        }
        SaveJson<T>(PATH, FILE_NAME, new T());
        return LoadJsonFile<T>(PATH, FILE_NAME);
    }
    #endregion
}
