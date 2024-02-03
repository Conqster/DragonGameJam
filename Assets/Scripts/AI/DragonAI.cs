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

    [Header("Behaviour")]
    [SerializeField] private HealthSystem m_HealthSystem;

    public HealthSystem HealthSystem { get { return m_HealthSystem; }}


    private void Start()
    {

        if (m_BrainInput.playMidArea == null)
            m_BrainInput.playMidArea = GameObject.FindGameObjectWithTag("MidPoint").transform;


        m_DragonSM = new SM_Idle(m_BrainInput, m_BrainOutput);
    }


    private void Update()
    {
        m_DragonSM = m_DragonSM.Process();
        m_Dragon_SM_Data = m_DragonSM.GetStateMachineData();


        if (!m_BrainInput.canMove)
            MoveInPlace();
    }



    private void LateUpdate()
    {
        if (m_HealthSystem.Health <= 0)
            Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillZone"))
            Destroy(gameObject);

        if(!m_BrainInput.canMove)
            if(other.gameObject.CompareTag("PlayZone"))
            {
                transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                m_BrainInput.canMove = true;    
            }
    }



    private void MoveInPlace()
    {
        transform.position += transform.forward * m_BrainInput.flySpeed * Time.deltaTime;
    }


    public AI_SM_BrainOutput BrainOutput { get { return m_BrainOutput; } }
    public AI_SM_BrainOutput GetBrainOutput() { return m_BrainOutput; }
}
