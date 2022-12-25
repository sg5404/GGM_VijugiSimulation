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
            // ±âº¸µå µş±ïµş±ï ¶ç¿ì±â

            if (Input.GetKeyDown(KeyCode.F))
            {
                interaction_Nurse(); // ÇÔÁ¤À¸·Î ¹Ù²Ù±â
            }
        }

        
    }

    void interaction_Nurse() //?í˜¸?‘ìš©
    {
        foreach (Pokemon poke in gameInfo.PlayerInfo.PokemonList)
        {
            poke.Heal(poke.MaxHp);
            //?¬ê¸°???ë?ê°€ ?ˆì—ˆ?„ë•Œ ?ë???ë§í•˜???¤í¬ë¦½íŠ¸ë¥?ë°›ì•„?€???¨ìˆ˜ ?¤í–‰
        }


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
