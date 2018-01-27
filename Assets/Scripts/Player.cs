using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Transform planet;

	private float forceAmountForRotation = 5f;
	private float forceAmountForJump = 30f;
	private Vector3 directionOfPlanetFromPlayer;
	private Vector3 directionOfPlayerFromPlanet;
	private bool allowForce;
	private bool allowForce2;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		directionOfPlanetFromPlayer = Vector3.zero;
		directionOfPlayerFromPlanet = Vector3.zero;
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		allowForce = false;
		if (Input.GetKey(KeyCode.Space))
			allowForce = true;
		allowForce2 = false;
		if (Input.GetKey(KeyCode.RightArrow))
			allowForce2 = true;
		directionOfPlanetFromPlayer = transform.position - planet.position;
		directionOfPlayerFromPlanet = (transform.position-planet.position).normalized;
		transform.right = Vector3.Cross(directionOfPlanetFromPlayer, Vector3.forward);
	}

	void FixedUpdate () {
		if (allowForce) {
//			rb2d.AddForce (transform.right * forceAmountForRotation);
			rb2d.AddForce (directionOfPlayerFromPlanet * forceAmountForJump);
		}
		if (allowForce2) {
			rb2d.AddForce (transform.right * forceAmountForRotation);
		}
	}
}
