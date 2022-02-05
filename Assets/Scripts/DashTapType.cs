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

    public void Desactive()
    {
        type = TYPE.NONE;
    }


    public enum TYPE { PLAYER, ATTACKER, PROTECTED, NONE }
}
