using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ListenerTimeEvent : MonoBehaviour
{
    [SerializeField] private ScriptableTimeEvent _timeEvent;

    [SerializeField] private UnityEvent<float> onTriggerEvent;

    private void Awake()
    {
        _timeEvent.AddListener(onTriggerEvent);
    }
}
