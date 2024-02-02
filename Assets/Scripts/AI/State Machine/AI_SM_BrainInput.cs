using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AI_SM_BrainInput
{
    [Header("Idle State Data")]
    [SerializeField, Range(0.0f, 10.0f)] public float idleDuration = 2.0f;


    [Header("Flying State Data")]
    [SerializeField, Range(0.0f, 10.0f)] public float flyingDuration = 5.0f;




    [Header("General")]
    [SerializeField] public DragonAI dragonAI;
    [SerializeField] public Transform dragonTransform;
    [SerializeField] public Transform playMidArea;
}



public class AI_SM_BrainOutput
{
    [SerializeField] public bool idle = false;
    [SerializeField] public bool flying = false;
}