using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBowAnimation : MonoBehaviour
{

    public Animator bowAnimation;
    public PlayerControl playerControl;
    bool activateTest;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            activateTest = !activateTest;
            bowAnimation.SetBool("FireButtonDown",activateTest);
        }
    }
}
