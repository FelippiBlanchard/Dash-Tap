using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class DashController : MonoBehaviour
{

    [Header("Dash Settings")]
    [SerializeField] protected float timeDash;
    [SerializeField] protected float timeBeforeDash;
    [SerializeField] protected float timeAfterDash;
    [SerializeField] protected STATE state;
    [SerializeField] protected MODE mode;

    [Header("Events")]
    [SerializeField] private UnityEvent onStartDash;
    [SerializeField] private UnityEvent onFinishDash;
    [SerializeField] private UnityEvent onActiveDash;
    [SerializeField] private UnityEvent onDesactiveDash;

    [Space(20)]


    private Coroutine coroutine;

    public void Dash()
    {
        Vector2 position = AreaDash.Instance.positionLastClick;

        if (isStateEnabled())
        {
            if(coroutine != null) StopCoroutine(coroutine);

            coroutine = StartCoroutine(DashCoroutine(position));
        }

    }

    private IEnumerator DashCoroutine(Vector2 position)
    {
        DesactiveState(); //não pode dar dash

        SetMode(MODE.DASHING); //tornar invisivel e capaz de matar
        yield return new WaitForSeconds(timeBeforeDash);


        transform.DOMove(position, timeDash).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(timeDash);

        ActiveState(); //pode dar dash

        yield return new WaitForSeconds(timeAfterDash);
        SetMode(MODE.VULNERABLE); //tornar visível e capaz de morrer

    }

    private void SetMode(MODE mode)
    {
        this.mode = mode;

        switch (mode)
        {
            case MODE.DASHING:
                onStartDash.Invoke();
                break;
            case MODE.VULNERABLE:
                onFinishDash.Invoke();
                break;
            default:
                break;
        }
    }

    private bool isStateEnabled()
    {
        return state == STATE.ENABLED;
    }
    public void ActiveState()
    {
        state = STATE.ENABLED;
        onActiveDash.Invoke();  
    }
    public void DesactiveState()
    {
        state = STATE.ENABLED;
        onDesactiveDash.Invoke();
    }
    protected enum MODE { VULNERABLE, DASHING }
    protected enum STATE { ENABLED, DISABLED}
}