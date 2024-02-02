using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidTest : MonoBehaviour
{

    [Header("Debugger")]
    [SerializeField, Range(0.0f, 10.0f)] private float radiusTest = 10.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusTest);
    }
}
