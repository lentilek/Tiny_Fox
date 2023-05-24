using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    //Fetch the Animator
    Animator m_Animator;
    // Use this for deciding if the GameObject can jump or not
    bool Walk;

    void Start()
    {
        //This gets the Animator, which should be attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        // The GameObject cannot jump
        Walk = false;
    }

    void Update()
    {

        //If the GameObject is not jumping, send that the Boolean “Jump” is false to the Animator. The jump animation does not play.
        if (Walk == false)
            m_Animator.SetBool("Walk", false);

        //The GameObject is jumping, so send the Boolean as enabled to the Animator. The jump animation plays.
        if (Walk == true)
            m_Animator.SetBool("Walk", true);
    }
}