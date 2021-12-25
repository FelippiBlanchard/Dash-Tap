using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTapType : MonoBehaviour
{
    [SerializeField] private TYPE type;
    public TYPE GetTYPE()
    {
        return type;
    }

    private void OnEnable()
    {
        if(type != TYPE.PLAYER)
        {
            
        }
    }

    public enum TYPE { PLAYER, ATTACKER, PROTECTED }
}
