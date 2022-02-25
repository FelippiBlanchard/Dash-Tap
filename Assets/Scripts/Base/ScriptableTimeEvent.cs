using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TimeEvent", menuName = "Events/TimeEvent", order = 1)]
public class ScriptableTimeEvent : ScriptableObject
{
    public List<UnityEvent<float>> events;

    public void Trigger(float time)
    {
        foreach (var evento in events)
        {
            evento.Invoke(time);
        }
    }

    public void AddListener(UnityEvent<float> e)
    {
        events.Add(e);
    }
    public void RemoveListener(UnityEvent<float> e)
    {
        events.Remove(e);
    }
}
