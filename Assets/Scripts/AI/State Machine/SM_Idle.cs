using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SM_Idle : StateMachine
{
    private float m_IdleDuration = 3.0f;

    public SM_Idle(AI_SM_BrainInput input, AI_SM_BrainOutput output) : base(input, output)
    {
        sm_name = "Idle State";
        sm_event = SM_Event.Enter;
        sm_duration = 0.0f;
        sm_state = SM_State.Idle;
    }


    protected override void Enter()
    {
        sm_output.idle = true;
        m_IdleDuration = sm_input.idleDuration;



        base.Enter();
    }


    protected override void Update()
    {
        if (sm_duration > m_IdleDuration)
            if(sm_input.canMove)
                TriggerExit(new SM_Flying(sm_input, sm_output));


        base.Update();
    }

    protected override void Exit() 
    {
        sm_output.idle = false;
        base.Exit(); 
    }
}
