﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		SceneManager.LoadScene ("demoscene");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			SceneManager.LoadScene ("demoscene");
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			// quit game
			Application.Quit();
		}
	}
}
