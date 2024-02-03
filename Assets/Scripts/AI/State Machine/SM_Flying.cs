using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Flying : StateMachine
{
    private float m_FlyDuration4Engage = 3.0f;
    private float m_Speed = 5.0f;

    private float y_Osci = 0.0f;

    private PlayAreaMidCheck m_PlayArea = null;
    private float moveAllowance = 9.0f;

    public SM_Flying(AI_SM_BrainInput input, AI_SM_BrainOutput output) : base(input, output)
    {
        sm_name = "Flying State";
        sm_event = SM_Event.Enter;
        sm_duration = 0.0f;
        sm_state = SM_State.Flying;
    }


    protected override void Enter()
    {
        //m_FlyDuration = sm_input.flyingDuration;

        m_FlyDuration4Engage = Random.Range(sm_input.minEngageTime, sm_input.maxEngageTime);
        Debug.Log("Engage in: " + m_FlyDuration4Engage);

        m_Speed = sm_input.flySpeed;

        int randInt = Random.Range(0, 100);
        m_Speed = (randInt % 2 == 0) ? m_Speed : -m_Speed;

        RandomNegation(ref m_Speed);

        if(sm_input.playMidArea.TryGetComponent<PlayAreaMidCheck>(out PlayAreaMidCheck playArea))
            m_PlayArea = playArea;


        base.Enter();
    }


    protected override void Update()
    {
        //if (sm_duration > m_FlyDuration)
        //    TriggerExit(new SM_Idle(sm_input, sm_output));

        if (sm_duration > m_FlyDuration4Engage)
        {
            if(GetATarget(out PickUpTargets targets))
            {
                TriggerExit(new SM_Engage(sm_input, sm_output, targets));
            }
        }


        //SineWave(ref y_Osci);
        Vector3 move = sm_input.dragonTransform.right;

        sm_input.dragonTransform.position += move * m_Speed * Time.deltaTime;

        float distance = Vector3.Distance(sm_input.dragonTransform.position, sm_input.playMidArea.position);



        if (m_PlayArea != null)
            moveAllowance = m_PlayArea.Radius;
        else
            moveAllowance = 9.0f;


        if (Mathf.Abs(distance) > moveAllowance)
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

        if(Physics.Raycast(sm_input.dragonTransform.position, -sm_input.dragonTransform.up, out hit,sm_input.maxDistForRaycast, sm_input.dragonLM))
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



    private void SineWave(ref float osci)
    {
        osci = sm_input.amp * Mathf.Sin((Time.deltaTime + sm_input.phase) * sm_input.freq);
        osci = Mathf.Clamp(osci, -1f, 1f);
        Debug.Log("My Osci: " + osci);
    }


}
