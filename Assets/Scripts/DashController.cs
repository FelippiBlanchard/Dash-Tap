using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
//using DG.Tweening;

public class DashController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private float timeDash;
    private float downtime;
    private STATE state;

    [Header("Events")]


    private Coroutine coroutine;

    public void OnPointerDown(PointerEventData eventData)
    {
        Dash(eventData.position);
    }

    private void Dash(Vector2 position)
    {
        coroutine = StartCoroutine(DashCoroutine(position));
    }
    private IEnumerator DashCoroutine(Vector2 position)
    {
        DesactiveState();
        yield return new WaitForSeconds(downtime);


        //transform.DOLocalMove(position, timeDash);
        yield return new WaitForSeconds(timeDash);



        yield return new WaitForSeconds(downtime);
        ActiveState();

    }

    public void ActiveState()
    {
        state = STATE.ENABLED;
    }
    public void DesactiveState()
    {
        state = STATE.ENABLED;
    }
    private enum STATE { ENABLED, DISABLED }
}