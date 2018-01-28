using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    Animator anim;
    public int currentState;
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Each State number coresponds to a bird action
        // 0 = Idle
        // 1 = Walking
        // 2 = Jumping
        // 3 = Pecking

        currentState = anim.GetInteger("State");

	    if ((Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyDown(KeyCode.A))) 
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetInteger("State", 3);
        }
        if (!Input.anyKey)  //If no key is being pressed...
        {
            anim.SetInteger("State", 0);
        }
    }
}
