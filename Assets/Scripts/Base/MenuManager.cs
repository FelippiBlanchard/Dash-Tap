using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onPlay;
    [SerializeField] private UnityEvent onTutorial;
    [SerializeField] private UnityEvent onExtras;
    [SerializeField] private UnityEvent onSettings;


    private void Start()
    {
        onStart.Invoke();
    }

    public void Play()
    {
        onPlay.Invoke();
    }

    public void Tutorial()
    {
        onTutorial.Invoke();
    }

    public void Extras()
    {
        onExtras.Invoke();
    }

    public void Settings()
    {
        onSettings.Invoke();
    }

}
