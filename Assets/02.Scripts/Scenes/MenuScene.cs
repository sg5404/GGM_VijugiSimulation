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
        GameInfo info = Managers.Save.LoadJsonFile<GameInfo>();
        if (info.PlayerInfo.PokemonList[0].Info != null)
        {
            Managers.Scene.LoadScene(Define.Scene.Map);
        }
        else
        {
            Managers.Scene.LoadScene(Define.Scene.Choice);
        }
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
