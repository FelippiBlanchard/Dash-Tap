using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreParticle : MonoBehaviour
{
    [SerializeField] private GameObject prefabPlusScore;
    [SerializeField] private Transform target;

    public void InstantiateScore(int n)
    {
        var obj = Instantiate(prefabPlusScore, target.position, target.rotation, transform);
        obj.GetComponent<TextMeshProUGUI>().text = '+' + n.ToString();
        StartCoroutine(DestroyOBJ(obj));
    }
    public IEnumerator DestroyOBJ(GameObject obj)
    {
        obj.transform.DOMoveY(target.position.y + 1, 1f);
        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<TextMeshProUGUI>().DOFade(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(obj);
    }
}
