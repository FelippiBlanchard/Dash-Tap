using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AreaDash : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent onClick;

    public Vector2 positionLastClick;
    public static AreaDash Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        positionLastClick = eventData.position;
        onClick.Invoke();
    }
}
