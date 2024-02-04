using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject mainMenuPanel;


    void Start()
    {
      mainMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
