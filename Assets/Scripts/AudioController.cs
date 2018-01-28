using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    AudioSource aud;
    public AudioClip jumpSound;
    public AudioClip peckSound;
    public AudioClip wowSound;
	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.W))
        {
            aud.PlayOneShot(jumpSound);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            aud.PlayOneShot(peckSound);
        }
        if (Input.GetKeyDown(KeyCode.))
	}
}
