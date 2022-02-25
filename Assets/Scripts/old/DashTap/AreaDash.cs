using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AreaDash : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent onClick;

    public Vector2 positionUILastClick;
    public Vector2 positionWorldLastClick;
    public static AreaDash Instance { get; private set; }


    private void Start()
    {
        Instance = this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        positionUILastClick = eventData.position;
        positionWorldLastClick = eventData.pointerCurrentRaycast.worldPosition;

        onClick.Invoke();
    }


}
