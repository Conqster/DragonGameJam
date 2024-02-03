using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{

    private Vector3 force;
    [SerializeField, Range(0.0f, 100.0f)] private float speed = 10.0f;
    [SerializeField, Range(0, 5)] private int damage = 2;



    private float lifeSpan = 0.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position += force * speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        lifeSpan += Time.deltaTime;

        if(lifeSpan > 5.0f)
            Destroy(gameObject);    
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

        if (other.gameObject.CompareTag("PlayZone"))
            return;

        SelfDestruct(); 
    }





    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
