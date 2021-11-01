using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int StartAngle;
    [SerializeField] private int EndAngle;
    [SerializeField] private List<Transform> targets;


    public int[] GetAngles()
    {
        int[] angles = new int[targets.Count];
        angles[0] = StartAngle;
        angles[1] = EndAngle;
        return angles;
    }
    public int GetRandomAngle()
    {
        return Random.Range(StartAngle, EndAngle);
    }

    private void OnDrawGizmos()
    {
        try
        {
            targets[0].localPosition = new Vector3(Mathf.Cos((Mathf.Deg2Rad * StartAngle)), Mathf.Sin((Mathf.Deg2Rad * StartAngle)), 0) * 50;
            targets[1].localPosition = new Vector3(Mathf.Cos((Mathf.Deg2Rad * EndAngle)), Mathf.Sin((Mathf.Deg2Rad * EndAngle)), 0) * 50;
            Gizmos.DrawLine(transform.position, targets[0].position);
            Gizmos.DrawLine(transform.position, targets[1].position);
        }
        catch (System.Exception)
        {

        }
    }

}
