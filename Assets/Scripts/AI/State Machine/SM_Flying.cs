using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Flying : StateMachine
{
    private float m_FlyDuration = 3.0f;
    private float m_Speed = 5.0f;

    public SM_Flying(AI_SM_BrainInput input, AI_SM_BrainOutput output) : base(input, output)
    {
        sm_name = "Flying State";
        sm_event = SM_Event.Enter;
        sm_duration = 0.0f;
        sm_state = SM_State.Flying;
    }


    protected override void Enter()
    {
        m_FlyDuration = sm_input.flyingDuration;

        int randInt = Random.Range(0, 100);
        m_Speed = (randInt % 2 == 0) ? m_Speed : -m_Speed;

        Debug.Log("Speed: " +  m_Speed);


        base.Enter();
    }


    protected override void Update()
    {
        if (sm_duration > m_FlyDuration)
            TriggerExit(new SM_Idle(sm_input, sm_output));

        Vector3 move = sm_input.dragonTransform.right;

        sm_input.dragonTransform.position += move * m_Speed * Time.deltaTime;

        float distance = Vector3.Distance(sm_input.dragonTransform.position, sm_input.playMidArea.position);

        if (Mathf.Abs(distance) > 10.0f)
        {
            m_Speed = -m_Speed;
        }




        base.Update();
    }

    protected override void Exit() { base.Exit(); }



}
