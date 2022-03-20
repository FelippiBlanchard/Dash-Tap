using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Killer Mode")]
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onBegin;
    [SerializeField] private UnityEvent onLose;

    [Header("Campaign Mode")]
    [SerializeField] private UnityEvent onStartC;
    [SerializeField] private UnityEvent onBeginC;
    [SerializeField] private UnityEvent onLoseC;


    [SerializeField] private ScriptableModeGame mode;


    private void Start()
    {
        switch (mode.mode)
        {
            case ScriptableModeGame.MODE.Killer:
                onStart.Invoke();
                break;
            case ScriptableModeGame.MODE.Campaign:
                onStartC.Invoke();
                break;
        }
        
    }

    public void Begin()
    {
        switch (mode.mode)
        {
            case ScriptableModeGame.MODE.Killer:
                onBegin.Invoke();
                break;
            case ScriptableModeGame.MODE.Campaign:
                onBeginC.Invoke();
                break;
        }
    }
    public void Lose()
    {
        switch (mode.mode)
        {
            case ScriptableModeGame.MODE.Killer:
                onLose.Invoke();
                break;
            case ScriptableModeGame.MODE.Campaign:
                onLoseC.Invoke();
                break;
        }
    }


}
