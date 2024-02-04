using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    [SerializeField] DragonAI dragonAI;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        dragonAI = transform.parent.GetComponent<DragonAI>();
        anim = gameObject.GetComponent<Animator>();

        //dragonAI.BrainOutput.
    }

    // Update is called once per frame
    void Update()
    {
        if (dragonAI.BrainOutput.idle == false)
        {
            anim.SetBool("ExitIdle", false);
        }

        if (dragonAI.BrainOutput.dive == true)
        {
            anim.SetBool("DiveLeft", true);
        }
        //if (dragonAI.BrainOutput.goingLeft==true)
        //{
        //    anim.SetBool("FlyingLeft", true);
        //}
        //if (dragonAI.BrainOutput.goingRight==true)
        //{
        //    anim.SetBool("FlyingRight", true);

        //}
        if (dragonAI.BrainOutput.liftOff == true)
        {
            anim.SetBool("Stealing", true);

        }
        anim.SetBool("FlyingRight", dragonAI.BrainOutput.goingRight);
        anim.SetBool("FlyingLeft", dragonAI.BrainOutput.goingLeft);


    }
}
