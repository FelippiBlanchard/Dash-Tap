using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{

    [SerializeField] private bool active = true;
    [SerializeField] private float speed;

    private void Update()
    {
        if(active)
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
    }
}
