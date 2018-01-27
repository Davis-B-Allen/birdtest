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
	void Update ()
    {
        currentState = anim.GetInteger("State");

	    if (Input.GetKeyDown(KeyCode.D))
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
