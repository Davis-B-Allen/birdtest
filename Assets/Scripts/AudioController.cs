using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    AudioSource aud;
    EatSeed poopSource;
    public bool poopSoundplayed = false;
    public AudioClip jumpSound;
    public AudioClip peckSound;
    public AudioClip walkSound;
    public AudioClip poopSound;
	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();
        poopSource = GetComponent<EatSeed>();
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
        if ((Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.D)))
        {
            aud.PlayOneShot(walkSound);
        }
        if ((poopSource.isPooping == true) && (poopSoundplayed == false))
        {
            aud.PlayOneShot(poopSound);
            poopSoundplayed = true;
        }
        if (poopSource.isPooping = false)
        {
            poopSoundplayed = false;
        }
	}
}
