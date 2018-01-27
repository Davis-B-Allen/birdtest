using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	public Transform player;
	public float gravitationalForce = 30f;

	private Vector3 directionOfPlayerFromPlanet;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		directionOfPlayerFromPlanet = Vector3.zero;
		rb2d = player.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		directionOfPlayerFromPlanet = (transform.position-player.position).normalized;
		rb2d.AddForce (directionOfPlayerFromPlanet * gravitationalForce);
	}
}
