using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]   
public class GoldBehaviour : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("GoldPile"))
        {
            DragonGameManager managerInstance = DragonGameManager.instance;
            managerInstance.GoldReturned();
            Destroy(gameObject);
        }
    }
}
