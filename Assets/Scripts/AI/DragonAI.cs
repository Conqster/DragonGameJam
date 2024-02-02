using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    private StateMachine m_DragonSM;

    [Header("Dragon AI Brain Input Data")]
    [SerializeField] private AI_SM_BrainInput m_BrainInput;

    [Header("Dragon AI Brain Output Data")]
    [SerializeField] private AI_SM_BrainOutput m_BrainOutput;
    [SerializeField] private StateMachineData m_Dragon_SM_Data;




    private void Start()
    {
        m_DragonSM = new SM_Idle(m_BrainInput, m_BrainOutput);
    }


    private void Update()
    {
        m_DragonSM = m_DragonSM.Process();
        m_Dragon_SM_Data = m_DragonSM.GetStateMachineData();
    }





}
