using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Transform planet;
	public float cwMag;
	public float forceAmountForJump = 30f;

	private float forceAmountForRotation = 100f;
	private Vector3 directionOfPlanetFromPlayer;
	private Vector3 directionOfPlayerFromPlanet;
	private bool jumpForce;
	private bool cwForce;
	private Rigidbody2D rb2d;

	public float maxTangentialSpeed = 5f;
	private Vector3 tangentialVelocity;
	private Vector3 centrifugalVelocity;
	private NearestPlanet np;

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
		if (Input.GetKey(KeyCode.Space))
			jumpForce = true;
		cwForce = false;
		if (Input.GetKey(KeyCode.RightArrow))
			cwForce = true;
		directionOfPlanetFromPlayer = transform.position - planet.position;
		directionOfPlayerFromPlanet = (transform.position-planet.position).normalized;
		Vector3 clockWiseTangent = Vector3.Cross(directionOfPlanetFromPlayer, Vector3.forward);
		transform.right = clockWiseTangent;

		centrifugalVelocity = Vector3.Project (rb2d.velocity, directionOfPlayerFromPlanet.normalized);
		tangentialVelocity = Vector3.Project (rb2d.velocity, clockWiseTangent.normalized);
		planet = np.closestPlanet.transform;
	}

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		if (h * tangentialVelocity.magnitude < maxTangentialSpeed) {
			rb2d.AddForce (transform.right * h * forceAmountForRotation);

//			rb2d.AddForce (Vector2.right * h * moveForce);
		}
		if (jumpForce) {
			rb2d.AddForce (directionOfPlayerFromPlanet * forceAmountForJump);
		}
//		if (cwForce) {
//			rb2d.AddForce (transform.right * forceAmountForRotation);
//		}
		cwMag = rb2d.velocity.magnitude;

		if (tangentialVelocity.magnitude > maxTangentialSpeed) {
			Vector3 tv2 = tangentialVelocity * (maxTangentialSpeed / tangentialVelocity.magnitude);
			Vector3 v2 = centrifugalVelocity + tv2;
			rb2d.velocity = v2;
		}
	}
}
