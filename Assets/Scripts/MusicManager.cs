using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public static MusicManager instance = null;

	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			DestroyObject(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
