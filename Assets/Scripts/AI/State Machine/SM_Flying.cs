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
        m_Speed = sm_input.flySpeed;

        int randInt = Random.Range(0, 100);
        m_Speed = (randInt % 2 == 0) ? m_Speed : -m_Speed;

        RandomNegation(ref m_Speed);

        Debug.Log("Speed: " +  m_Speed);


        base.Enter();
    }


    protected override void Update()
    {
        //if (sm_duration > m_FlyDuration)
        //    TriggerExit(new SM_Idle(sm_input, sm_output));

        if (sm_duration > m_FlyDuration)
        {
            if(GetATarget(out PickUpTargets targets))
            {
                TriggerExit(new SM_Engage(sm_input, sm_output, targets));
            }
        }


        Vector3 move = sm_input.dragonTransform.right;

        sm_input.dragonTransform.position += move * m_Speed * Time.deltaTime;

        float distance = Vector3.Distance(sm_input.dragonTransform.position, sm_input.playMidArea.position);

        if (Mathf.Abs(distance) > 9.0f)
        {
            m_Speed = -m_Speed;
        }




        base.Update();
    }

    protected override void Exit() { base.Exit(); }



    private bool GetATarget(out PickUpTargets targets)
    {
        RaycastHit hit;
        targets = new PickUpTargets();

        if(Physics.Raycast(sm_input.dragonTransform.position, -sm_input.dragonTransform.up, out hit))
        {
            float halfDistance = hit.distance * 0.5f;
            targets.primaryTarget = hit.point;
            float xDirection = halfDistance;

            RandomNegation(ref xDirection);

            targets.secondaryTarget = sm_input.dragonTransform.position - new Vector3(xDirection, halfDistance, 0.0f);

            Debug.DrawLine(sm_input.dragonTransform.position, hit.point, Color.blue, 10.0f);
            Debug.DrawLine(sm_input.dragonTransform.position, targets.secondaryTarget, Color.cyan, 10.0f);

            Vector3 vecToSec = targets.secondaryTarget - sm_input.dragonTransform.position;
            float length = vecToSec.magnitude;
            vecToSec.Normalize();

            if (Physics.Raycast(sm_input.dragonTransform.position, vecToSec, length))
            {
                targets.secondaryTarget = sm_input.dragonTransform.position - new Vector3(-xDirection, halfDistance, 0.0f);
                Debug.DrawLine(sm_input.dragonTransform.position, targets.secondaryTarget, Color.magenta, 10.0f);
            }


            return true;
        }



        return false;
    }



    private void SineWave()
    {

    }


}
