using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDasher : DashController
{
    [Space(10)]
    [Header("Events Collision")]
    [SerializeField] private UnityEvent onKillATTACKER;
    [SerializeField] private UnityEvent onKillPROTECTED;
    [SerializeField] private UnityEvent onCollisionPROTECTED;
    [SerializeField] private UnityEvent onCollisionATTACKER;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        DashTapType.TYPE collisionType = collision.gameObject.gameObject.GetComponent<DashTapType>().GetTYPE();
        switch (collisionType)
        {
            case DashTapType.TYPE.ATTACKER:
                OnCollisionATTACKER(collision.gameObject);
                break;
            case DashTapType.TYPE.PROTECTED:
                OnCollisionPROTECTED(collision.gameObject);
                break;
        }
    }


    private void OnCollisionATTACKER(GameObject collisionObject)
    {
        switch (mode)
        {
            case(MODE.DASHING):
                onKillATTACKER.Invoke();
                collisionObject.GetComponent<Death>().Die();
                break;
            case (MODE.VULNERABLE):
                onCollisionATTACKER.Invoke();
                Debug.Log("morreu");
                GetComponent<Death>().Die();
                break;
        }
    }

    private void OnCollisionPROTECTED(GameObject collisionObject)
    {
        switch (mode)
        {
            case (MODE.DASHING):
                onKillPROTECTED.Invoke();
                collisionObject.GetComponent<Death>().Die();
                break;
            case (MODE.VULNERABLE):
                onCollisionPROTECTED.Invoke();
                break;
        }
    }

}
