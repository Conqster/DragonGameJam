using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class HealthSystem
{

    [SerializeField, Range(0.0f, 10.0f)] private float health;

    public float Health { get { return health; } }

    public void DealDamage(float value)
    {
        
        if (health > 0.0f)
            health -= value;
    }
}
