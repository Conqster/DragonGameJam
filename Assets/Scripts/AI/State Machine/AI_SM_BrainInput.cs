using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AI_SM_BrainInput
{
    [Header("Idle State Data")]
    [SerializeField, Range(0.0f, 10.0f)] public float idleDuration = 2.0f;


    [Header("Flying State Data")]
    [SerializeField, Range(0.0f, 10.0f)] public float flySpeed = 5.0f;
    [SerializeField, Range(0.0f, 10.0f)] public float maxEngageTime = 6.0f;
    [SerializeField, Range(0.0f, 10.0f)] public float minEngageTime = 6.0f;


    [Header("Engage State Data")]
    [SerializeField, Range(0.0f, 10.0f)] public float engageSpeed = 6.0f;

    [Header("OnCollected State Data")]
    [SerializeField, Range(0.0f, 20.0f)] public float onCollectedSpeed = 6.0f;


    [Header("General")]
    [SerializeField] public DragonAI dragonAI;
    [SerializeField] public bool canMove = false;
    [SerializeField] public LayerMask goldLM;
    [SerializeField] public LayerMask wallLM;
    [SerializeField, Range(0.0f, 100.0f)] public float maxDistForRaycast = 15.0f;
    [SerializeField] public Transform dragonTransform;
    [SerializeField] public Transform playMidArea;
    [SerializeField] public Transform coinsObject;

    [Header("Sine Wave")]
    [SerializeField, Range(0.0f, 15.0f)] public float amp = 1.0f;
    [SerializeField, Range(0.0f, 15.0f)] public float freq = 1.0f;
    [SerializeField, Range(0.0f, 15.0f)] public float phase = 0.0f;
}


[System.Serializable]
public class AI_SM_BrainOutput
{
    [SerializeField] public bool idle = false;
    [SerializeField] public bool flying = false;

    [SerializeField] public bool goingLeft = false;
    [SerializeField] public bool goingRight = false;
    [SerializeField] public bool die = false;
    [SerializeField] public bool dive = false;
    [SerializeField] public bool liftOff = false;

    [SerializeField] public Vector3 currentEngageVector = Vector3.zero;
}