using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Transform planet;
	public float cwMag;
	public float forceAmountForJump = 30f;
	public Transform groundCheck;
	public float maxTangentialSpeed = 5f;

	private float forceAmountForRotation = 100f;
	private Vector3 directionOfPlanetFromPlayer;
	private Vector3 directionOfPlayerFromPlanet;
	private bool jumpForce;
	private Rigidbody2D rb2d;
	private Vector3 tangentialVelocity;
	private Vector3 centrifugalVelocity;
	private NearestPlanet np;
	private bool grounded = false;
	private bool jump = false;

	// Use this for initialization
	void Start () {
		directionOfPlanetFromPlayer = Vector3.zero;
		directionOfPlayerFromPlanet = Vector3.zero;
		rb2d = GetComponent<Rigidbody2D> ();
		cwMag = 0f;

		tangentialVelocity = Vector3.zero;
		centrifugalVelocity = Vector3.zero;
		np = GetComponent<NearestPlanet> ();
	}
	
	// Update is called once per frame
	void Update () {
		jumpForce = false;
		if (Input.GetKey (KeyCode.Space) && grounded) {
//			jumpForce = true;
			jump = true;
		}
		directionOfPlanetFromPlayer = transform.position - planet.position;
		directionOfPlayerFromPlanet = (transform.position-planet.position).normalized;
		Vector3 clockWiseTangent = Vector3.Cross(directionOfPlanetFromPlayer, Vector3.forward);

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		// Change rotation of birdPlayer so that it's "standing" on the surface of the planet
		transform.right = clockWiseTangent;

		centrifugalVelocity = Vector3.Project (rb2d.velocity, directionOfPlayerFromPlanet.normalized);
		tangentialVelocity = Vector3.Project (rb2d.velocity, clockWiseTangent.normalized);
		planet = np.closestPlanet.transform;
	}

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");

		if (h * tangentialVelocity.magnitude < maxTangentialSpeed) {
			rb2d.AddForce (transform.right * h * forceAmountForRotation);
		}

//		if (jumpForce) {
//			rb2d.AddForce (directionOfPlayerFromPlanet * forceAmountForJump);
//		}
//
		if (jump) {
			rb2d.AddForce (directionOfPlayerFromPlanet * forceAmountForJump);
			jump = false;
		}

		cwMag = rb2d.velocity.magnitude;

		if (tangentialVelocity.magnitude > maxTangentialSpeed) {
			Vector3 tv2 = tangentialVelocity * (maxTangentialSpeed / tangentialVelocity.magnitude);
			Vector3 v2 = centrifugalVelocity + tv2;
			rb2d.velocity = v2;
		}
	}
}
