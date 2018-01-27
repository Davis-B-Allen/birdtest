using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Transform planet;
	public Transform lastPlanet;
	public float cwMag;
	public float forceAmountForJump = 30f;
	public Transform groundCheck;
	public float maxTangentialSpeed = 5f;
	public float smoothTransitionSpeed = 2f;
	public float transitionErrorMargin = 10f;
	public bool canControl = true;

	private float forceAmountForRotation = 100f;
	private Vector3 directionOfPlanetFromPlayer;
	private Vector3 directionOfPlayerFromPlanet;
	private Rigidbody2D rb2d;
	private Vector3 tangentialVelocity;
	private Vector3 centrifugalVelocity;
	private NearestPlanet np;
	private bool grounded = false;
	private bool jump = false;
	private bool smoothTransition = false;
	private float transitionTime = 0f;

	// Use this for initialization
	void Start () {
		directionOfPlanetFromPlayer = Vector3.zero;
		directionOfPlayerFromPlanet = Vector3.zero;
		rb2d = GetComponent<Rigidbody2D> ();
		cwMag = 0f;

		tangentialVelocity = Vector3.zero;
		centrifugalVelocity = Vector3.zero;
		np = GetComponent<NearestPlanet>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space) && grounded) {
			jump = true;
		}
		directionOfPlanetFromPlayer = transform.position - planet.position;
		directionOfPlayerFromPlanet = (transform.position-planet.position).normalized;
		Vector3 clockWiseTangent = Vector3.Cross(directionOfPlanetFromPlayer, Vector3.forward);

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (planet != lastPlanet && lastPlanet != null) {
			smoothTransition = true;
			transitionTime = 0f;
		}

		if (smoothTransition) {
			canControl = false;
			transitionTime += Time.deltaTime;
			float percentComplete = transitionTime/smoothTransitionSpeed;
			if (Vector3.Angle(transform.right, clockWiseTangent) <= transitionErrorMargin) {
				smoothTransition = false;
				canControl = true;
			}
			else {
				Vector3 v1 = new Vector3(transform.right.x, transform.right.y, 0);
				Vector3 v2 = new Vector3(clockWiseTangent.x, clockWiseTangent.y, 0);
				transform.right = Vector3.Slerp(v1, v2, percentComplete);
			}
		}
		else {
			// Change rotation of birdPlayer so that it's "standing" on the surface of the planet
			transform.right = clockWiseTangent;
		}

		centrifugalVelocity = Vector3.Project (rb2d.velocity, directionOfPlayerFromPlanet.normalized);
		tangentialVelocity = Vector3.Project (rb2d.velocity, clockWiseTangent.normalized);
		lastPlanet = planet;
		planet = np.closestPlanet.transform;
	}

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");

		if (canControl && (h * tangentialVelocity.magnitude < maxTangentialSpeed)) {
			rb2d.AddForce (transform.right * h * forceAmountForRotation);
		}

		if (canControl && jump) {
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
