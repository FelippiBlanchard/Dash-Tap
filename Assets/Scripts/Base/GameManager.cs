using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onLose;
    [SerializeField] private UnityEvent onLoseDying;
    [SerializeField] private UnityEvent onLoseKilling;

    [SerializeField] private UnityEvent onPause;
    [SerializeField] private UnityEvent onResume;

    private void Start()
    {
        onStart.Invoke();
    }
    private void Lose()
    {
        onLose.Invoke();
    }
    public void LoseDying()
    {
        Lose();
        onLoseDying.Invoke();
    }
    public void LoseKilling()
    {
        Lose();
        onLoseKilling.Invoke();
    }
    public void Pause()
    {
        onPause.Invoke();
    }
    public void Resume()
    {
        onResume.Invoke();
    }

}
