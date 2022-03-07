using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class AreaDash : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject limitationDashArea;
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

        Vector3[] corners = new Vector3[4];
        limitationDashArea.GetComponent<RectTransform>().GetWorldCorners(corners);
        if (positionWorldLastClick.x < 0)
        {
            positionWorldLastClick.x = positionWorldLastClick.x < corners[0].x ? corners[0].x : positionWorldLastClick.x;
        }
        else
        {
            positionWorldLastClick.x = positionWorldLastClick.x > corners[3].x ? corners[3].x : positionWorldLastClick.x;
        }

        if (positionWorldLastClick.y < 0)
        {
            positionWorldLastClick.y = positionWorldLastClick.y < corners[0].y ? corners[0].y : positionWorldLastClick.y;
        }
        else
        {
            positionWorldLastClick.y = positionWorldLastClick.y > corners[1].y ? corners[1].y : positionWorldLastClick.y;
        }
        onClick.Invoke();
    }


}
