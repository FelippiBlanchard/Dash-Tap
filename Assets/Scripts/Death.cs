using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    [SerializeField] private float timeToDie;

    [Header("Events")]
    [SerializeField] private UnityEvent onDie;


    public void Die()
    {
        onDie.Invoke();
        StartCoroutine(DeathCoroutine());
        //Animação Morrer
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);
    }

}
