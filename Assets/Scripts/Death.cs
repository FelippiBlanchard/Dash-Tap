using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    [SerializeField] private float timeDying;

    [SerializeField] private bool autoKill;
    [SerializeField] private float timeToAutoKill;

    [Header("Events")]
    [SerializeField] private UnityEvent onDie;

    private void Start()
    {
        if (autoKill)
        {
            StartCoroutine(AutoKillCoroutine());
        }
    }
    private IEnumerator AutoKillCoroutine()
    {
        yield return new WaitForSeconds(timeToAutoKill);
        Destroy(gameObject);
    }

    public void Die()
    {
        onDie.Invoke();
        StartCoroutine(DeathCoroutine());
        //Animação Morrer
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(timeDying);
        Destroy(gameObject);
    }

}
