using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDasher : DashController
{
    [Space(10)]
    [Header("Events Collision")]
    [SerializeField] private UnityEvent onCollisionATTACKER;
    [SerializeField] private UnityEvent onCollisionPROTECTED;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colidiu");
        DashTapType.TYPE collisionType = collision.gameObject.gameObject.GetComponent<DashTapType>().GetTYPE();
        switch (collisionType)
        {
            case DashTapType.TYPE.ATTACKER:
                Debug.Log("attacker");
                OnCollisionATTACKER(collision.gameObject);
                onCollisionATTACKER.Invoke();
                break;
            case DashTapType.TYPE.PROTECTED:
                Debug.Log("protected");
                OnCollisionPROTECTED(collision.gameObject);
                onCollisionPROTECTED.Invoke();
                break;
        }
    }


    private void OnCollisionATTACKER(GameObject collisionObject)
    {
        switch (mode)
        {
            case(MODE.DASHING):
                collisionObject.GetComponent<Death>().Die();
                break;
            case (MODE.VULNERABLE):
                GetComponent<Death>().Die();
                break;
        }
    }

    private void OnCollisionPROTECTED(GameObject collisionObject)
    {
        switch (mode)
        {
            case (MODE.DASHING):
                collisionObject.GetComponent<Death>().Die();
                break;
            case (MODE.VULNERABLE):
                break;
        }
    }

}
