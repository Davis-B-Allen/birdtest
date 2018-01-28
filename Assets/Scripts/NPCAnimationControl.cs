using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationControl : MonoBehaviour {
    Animator anim;
    public int currentNPCstate;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        currentNPCstate = anim.GetInteger("NPCState");

		if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetInteger("NPCState", 1);
        }
	}
}
