using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
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
    void Start()
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
        }
    }

    // 씬 전환 시 호출
    public static void Clear()
    {
        Scene.Clear();

        Pool.Clear();
    }
}
