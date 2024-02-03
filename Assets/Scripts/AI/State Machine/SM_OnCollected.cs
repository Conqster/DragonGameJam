using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_OnCollected : StateMachine
{
    private float m_Speed = 5.0f;
    private Vector3 move = Vector3.up;

    public SM_OnCollected(AI_SM_BrainInput input, AI_SM_BrainOutput output) : base(input, output)
    {
        sm_name = "On Collected State";
        sm_event = SM_Event.Enter;
        sm_duration = 0.0f;
        sm_state = SM_State.Collected;
    }


    protected override void Enter()
    {
        sm_output.liftOff = true;
        m_Speed = sm_input.onCollectedSpeed;


        base.Enter();
    }


    protected override void Update()
    {
        sm_input.dragonTransform.position += move * m_Speed * Time.deltaTime;

        base.Update();
    }

    protected override void Exit()
    {
        sm_output.liftOff = false;
        base.Exit();
    }

}
