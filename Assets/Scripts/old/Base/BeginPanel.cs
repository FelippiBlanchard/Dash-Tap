using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class BeginPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float timeFade;
    [SerializeField] private CanvasGroup group;

    [Header("Events")]
    [SerializeField] private UnityEvent onTap;



    private void OnEnable()
    {
        group = GetComponent<CanvasGroup>();
        Show();
    }

    public void Show()
    {
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;
    }

    public void Hide()
    {
        group.DOFade(0, timeFade);
        group.blocksRaycasts = false;
        group.interactable = false;

        StartCoroutine(Desactive());
    }
    
    private IEnumerator Desactive()
    {
        yield return new WaitForSeconds(timeFade);
        gameObject.SetActive(false);
    }

    public void OnPointerDown (PointerEventData pointerData)
    {
        Hide();
        onTap.Invoke();
    }

}
