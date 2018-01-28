using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTutorialTrigger : MonoBehaviour {
    GameObject player;
    public GameObject jumpTutorial;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(player.transform.position, transform.position) < 1f) {
            jumpTutorial.GetComponent<FadeInOut>().startWithTrigger();
        }
	}
}
