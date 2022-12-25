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
            // �⺸�� ������� ����

            if (Input.GetKeyDown(KeyCode.F))
            {
                interaction_Nurse(); // �������� �ٲٱ�
            }
        }

        
    }

    void interaction_Nurse() //?�호?�용
    {
        foreach (Pokemon poke in gameInfo.PlayerInfo.PokemonList)
        {
            poke.Heal(poke.MaxHp);
            //?�기???��?가 ?�었?�때 ?��???말하???�크립트�?받아?�???�수 ?�행
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
