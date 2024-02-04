using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragonGameManager : MonoBehaviour
{
    public static DragonGameManager instance;

    [SerializeField, Range(0, 20)] private int m_maxGold = 10;
    [SerializeField] private int m_currentGoldLeft;
    [SerializeField] private int m_dragonKilled = 0;

    [SerializeField] private TextMeshProUGUI m_GoldLeftUI;
    [SerializeField] private TextMeshProUGUI m_DragonKilledUI;

    public int CurrentGoldLeft {  get { return m_currentGoldLeft;} }
    public int MaxGold { get { return m_maxGold; } }
    public int DragonKilled { get {  return m_dragonKilled; } } 


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);    
    }


    private void Start()
    {
        m_currentGoldLeft = m_maxGold;
        UpdateGoldLeft();
        UpdateDragonKilled();
    }


    public void PickGold()
    {
        m_currentGoldLeft--;
        UpdateGoldLeft();
    }

    public void DragonADied()
    {
        m_dragonKilled++;
        UpdateDragonKilled();   
    }

    public void GoldReturned()
    {
        m_currentGoldLeft++;
        UpdateGoldLeft();
    }


    private void UpdateGoldLeft()
    {
        m_GoldLeftUI.text = "Gold Left: " + m_currentGoldLeft.ToString("0");
    }

    private void UpdateDragonKilled()
    {
        m_DragonKilledUI.text = "Dragon Killed: " + m_dragonKilled.ToString("0");
    }

}
