using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaMidCheck : MonoBehaviour
{

    private DragonAI dragonAI;

    [Header("Debugger")]
    [SerializeField, Range(0.0f, 10.0f)] private float radius = 10.0f;

    public float Radius { get { return radius; }}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
