using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPanel : MonoBehaviour
{
    [SerializeField] private float timeToDisable;
    void Start()
    {
        var firstBeginPanel = PlayerPrefs.GetInt("FirstBeginPanel", 0) == 0;
        if (firstBeginPanel)
        {
            StartCoroutine(Disable());
            PlayerPrefs.SetInt("FirstBeginPanel", 1);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }
}
