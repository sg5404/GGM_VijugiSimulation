using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region CORE
    UIManager _ui = new UIManager();
    PoolManager _pool = new PoolManager();
    SaveManager _save = new SaveManager();
    SoundManager _sound = new SoundManager();
    SceneManagerEX _scene = new SceneManagerEX();
    ResourceManager _resource = new ResourceManager();

    public static UIManager UI { get { return Instance._ui; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static SaveManager Save { get { return Instance._save; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static SceneManagerEX Scene { get { return Instance._scene; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    #endregion

    void Awake()
    {
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
            s_instance._save.Init();
        }
    }

    // �� ��ȯ �� ȣ��
    public static void Clear()
    {
        Scene.Clear();

        Pool.Clear();
    }
}
