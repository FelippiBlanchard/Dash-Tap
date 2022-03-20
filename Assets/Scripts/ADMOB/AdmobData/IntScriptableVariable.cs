using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Int", menuName = "ScriptableVariable/Int", order = 1)]
public class IntScriptableVariable : ScriptableObject
{
    public int value;

    public void AddValue(int value = 1)
    {
        this.value += value;
    }
    public void SetValue(int value = 0)
    {
        this.value = value;
    }
}
