using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PickUpTargets
{
    public Vector3 secondaryTarget;
    public Vector3 primaryTarget;
}


public class SM_Engage : StateMachine
{

    private PickUpTargets m_Targets;
    private float m_Speed;

    private Vector3 vecToTarget = Vector3.zero;

    enum CurrentTarget
    {
        secondary,
        primary,
        Neutral
    }

    private CurrentTarget m_CurrentTarget = CurrentTarget.secondary;

    public SM_Engage(AI_SM_BrainInput input, AI_SM_BrainOutput output, PickUpTargets targets) : base(input, output)
    {
        sm_name = "Engage State";
        sm_event = SM_Event.Enter;
        sm_duration = 0.0f;
        sm_state = SM_State.Engage;
        m_Targets = targets;
    }



    protected override void Enter()
    {
        sm_output.dive = true;
        m_Speed = sm_input.engageSpeed;

        vecToTarget = m_Targets.secondaryTarget - sm_input.dragonTransform.position;
        vecToTarget.Normalize();

        Debug.DrawRay(sm_input.dragonTransform.position, vecToTarget, Color.red, 5.0f);


        base.Enter();
    }


    protected override void Update()
    {
        //if (sm_duration > m_IdleDuration)
        //    TriggerExit(new SM_Flying(sm_input, sm_output));

        sm_input.dragonTransform.position += vecToTarget * m_Speed * Time.deltaTime;

        float distanceToSec = Vector3.Distance(sm_input.dragonTransform.position, m_Targets.secondaryTarget);
        float distanceToPri = Vector3.Distance(sm_input.dragonTransform.position, m_Targets.primaryTarget);

        switch(m_CurrentTarget)
        {
            case CurrentTarget.secondary:

                if(Mathf.Abs(distanceToSec) <= 1.0f)
                {
                    vecToTarget = m_Targets.primaryTarget - sm_input.dragonTransform.position;
                    vecToTarget.Normalize();

                    Debug.DrawRay(sm_input.dragonTransform.position, vecToTarget, Color.red, 5.0f);

                    m_CurrentTarget = CurrentTarget.primary;
                    
                }

                break;
            case CurrentTarget.primary:

                if (Mathf.Abs(distanceToPri) <= 1.0f)
                {
                    vecToTarget = Vector3.up;

                    Debug.DrawRay(sm_input.dragonTransform.position, vecToTarget, Color.red, 5.0f);

                    m_CurrentTarget = CurrentTarget.Neutral;
                }

                break;
            case CurrentTarget.Neutral:

                //m_Speed *= 2.0f;
                TriggerExit(new SM_OnCollected(sm_input, sm_output));
                break;
        }

        sm_output.currentEngageVector = vecToTarget;

        sm_output.goingLeft = (vecToTarget.x < 0) ? true : false;
        sm_output.goingRight = (vecToTarget.x > 0) ? true : false;

        base.Update();
    }

    protected override void Exit() 
    { 
        sm_output.dive = false;

        //WARNING: Comment below to take direction over to next state.
        //sm_output.goingLeft = false;
        //sm_output.goingRight = false;
        base.Exit(); 
    }


}
