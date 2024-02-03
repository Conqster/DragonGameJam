using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{

    private Vector3 force;
    [SerializeField, Range(0.0f, 100.0f)] private float speed = 10.0f;
    [SerializeField, Range(0, 5)] private int damage = 2;


    // Update is called once per frame
    void Update()
    {
        transform.position += force * speed * Time.deltaTime;
    }


    public void ForceDirection(Vector3 dir)
    {
        force = dir;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Dragon"))
            if(other.TryGetComponent<DragonAI>(out DragonAI dragon))
                dragon.HealthSystem.DealDamage(damage);


        //Invoke("SelfDestruct", 1.5f);
        SelfDestruct(); 
    }





    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
