using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Playing,
    Pause,
    GameOver
}


public class DragonGameManager : MonoBehaviour
{
    public static DragonGameManager instance;

    [SerializeField, Range(0, 20)] private int m_maxGold = 10;
    [SerializeField] private int m_currentGoldLeft;
    [SerializeField] private int m_dragonKilled = 0;

    [SerializeField] private TextMeshProUGUI m_GoldLeftUI;
    [SerializeField] private TextMeshProUGUI m_DragonKilledUI;

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject deathPanel;

    [SerializeField] private GameState m_GameState;

    public event EventHandler OnGoldTrigger;


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

  
        deathPanel.SetActive(false);
    }



    private void Update()
    {
        

        switch(m_GameState)
        {
            case GameState.Menu:
                deathPanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                m_DragonKilledUI.gameObject.SetActive(false);
                m_GoldLeftUI.gameObject.SetActive(false);
                Time.timeScale = 0.0f;
                break;
            case GameState.Playing:
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                PlayingBehaviour();
                deathPanel.SetActive(false);
                mainMenuPanel.SetActive(false);
                m_DragonKilledUI.gameObject.SetActive(true);
                m_GoldLeftUI.gameObject.SetActive(true);
                break;
            case GameState.Pause:
                Cursor.lockState = CursorLockMode.None;
                PauseBehaviour();
                deathPanel.SetActive(false);
                mainMenuPanel.SetActive(false);
                m_DragonKilledUI.gameObject.SetActive(true);
                m_GoldLeftUI.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
                break;
            case GameState.GameOver:
                Cursor.lockState = CursorLockMode.None;
                deathPanel.SetActive(true);
                mainMenuPanel.SetActive(false);
                m_DragonKilledUI.gameObject.SetActive(true);
                m_GoldLeftUI.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
                break;
        }
    }


    private void PlayingBehaviour()
    {
        if(Input.GetKey(KeyCode.Escape))
            m_GameState = GameState.Pause;
    }

    private void PauseBehaviour()
    {
        if (Input.GetKey(KeyCode.Escape))
            m_GameState = GameState.Playing;

    }


    public void PickGold()
    {
        FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.PickupCoin, transform.position, 1f);
        m_currentGoldLeft--;
        UpdateGoldLeft();


    }

    public void DragonADied()
    {
        FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.DragonDeath, transform.position, 1f);
        m_dragonKilled++;
        UpdateDragonKilled();

      
    }

    public void GoldReturned()
    {
        FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.GoldReturn, transform.position, 0.6f);
        m_currentGoldLeft++;
        UpdateGoldLeft();
    }


    private void UpdateGoldLeft()
    {
        OnGoldTrigger?.Invoke(this, EventArgs.Empty);
        m_GoldLeftUI.text = "Gold Left: " + m_currentGoldLeft.ToString("0");

        if (m_currentGoldLeft == 0 && m_GameState != GameState.GameOver)
            m_GameState = GameState.GameOver;
    }

    private void UpdateDragonKilled()
    {
        m_DragonKilledUI.text = "Dragons Killed: " + m_dragonKilled.ToString("0");
    }

    public void StartGame()
    {
        m_GameState = GameState.Playing;
        m_currentGoldLeft = m_maxGold;


        UpdateGoldLeft();
        //SceneManager.LoadScene("Dragons");

    }

    public void MainMenu()
    {
        m_GameState = GameState.Menu;

        GameObject[] dragons = GameObject.FindGameObjectsWithTag("Dragon");

        if(dragons != null && dragons.Length > 0)
            foreach(var d  in dragons)
                Destroy(d);

        //SceneManager.LoadScene("Main_Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
