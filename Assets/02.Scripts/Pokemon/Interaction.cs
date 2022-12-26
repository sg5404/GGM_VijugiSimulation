using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private float _interactionRange = 5f;
    [SerializeField]
    private LayerMask _layerMask;

    GameInfo gameInfo = new GameInfo();

    void Start()
    {
        gameInfo = Managers.Save.LoadJsonFile<GameInfo>();
    }

    void Update()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, _interactionRange, _layerMask);
        if (colls.Length > 0)
        {
            // ±‚∫∏µÂ µ˛±Ôµ˛±Ô ∂ÁøÏ±‚

            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject heal = Managers.Resource.Instantiate("Effect/Heal");
                heal.transform.position = this.transform.position;
                interaction_Nurse();
            }
        }

        
    }

    void interaction_Nurse() //?ÅÌò∏?ëÏö©
    {
        foreach (Pokemon poke in gameInfo.PlayerInfo.PokemonList)
        {
            poke.Heal(poke.MaxHp);
        }
        Managers.Save.SaveJson<GameInfo>(gameInfo);

        MapScene scene = Managers.Scene.CurrentScene as MapScene;


    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _interactionRange);
        Gizmos.color = Color.white;
    }
#endif
}
