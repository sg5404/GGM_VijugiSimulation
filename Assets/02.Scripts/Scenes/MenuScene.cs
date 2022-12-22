using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : BaseScene
{
    protected override void Init()
    {
        SceneType = Define.Scene.Menu;
    }

    public void LoadMapScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Map);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public override void Clear()
    {

    }
}
