using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    AudioSource aud;
	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource>()
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.W))
        {
            aud.
        }
	}
}
