using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboScore : MonoBehaviour
{
    [SerializeField] private FloatScriptableVariable time;
    [SerializeField] private FloatScriptableVariable scoreMultiplication;

    private void Update()
    {
        if(time.value > 0)
            time.value -= Time.deltaTime;
    }
    public void SetOneMore()
    {
        if(time.value > 0)
        {
        }
    }

}
