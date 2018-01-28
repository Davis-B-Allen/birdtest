using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;

	public Animator animBody;
	public Animator animDetails;

	public Transform planet;
	public Transform lastPlanet;
	public float forceAmountForJump = 1200f;
	public Transform groundCheck;
	public float maxTangentialSpeed = 20f;
	public float smoothTransitionSpeed = 2f;
	public float transitionErrorMargin = 10f;
	public float jumpVelocity = 30f;
	public bool canControl = true;
	public float forceAmountForRotation = 200f;

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

		tangentialVelocity = Vector3.zero;
		centrifugalVelocity = Vector3.zero;
		np = GetComponent<NearestPlanet>();

		animBody = transform.Find ("birdBody").GetComponent<Animator> ();
		animDetails = transform.Find ("birdDetails").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		lastPlanet = planet;
		planet = np.closestPlanet.transform;

		// Dumping this here cuz I don't want to add anything else to demoscene
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("splashscene");
		}

		if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && grounded) {
			jump = true;
		}
		directionOfPlanetFromPlayer = transform.position - planet.position;
		directionOfPlayerFromPlanet = (transform.position-planet.position).normalized;
		Vector3 clockWiseTangent = Vector3.Cross(directionOfPlanetFromPlayer, Vector3.forward);

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, (1 << LayerMask.NameToLayer ("Ground")) | (1 << LayerMask.NameToLayer ("Passable platform")));

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
	}

	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");

		if (grounded) {
			if (h == 0) {
				animBody.SetInteger("State", 0);
				animDetails.SetInteger("State", 0);
			} else {
				animBody.SetInteger("State", 1);
				animDetails.SetInteger("State", 1);
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				animBody.SetInteger("State", 3);
				animDetails.SetInteger("State", 3);
			}
		} else {
			animBody.SetInteger("State", 2);
			animDetails.SetInteger("State", 2);
		}

		// initial values for tangential and centrifugal components of velocity
		Vector3 targetTangentialVelocity = tangentialVelocity;
		Vector3 targetCentrifugalVelocity = centrifugalVelocity;

		// left/right
		if (canControl && (h * tangentialVelocity.magnitude < maxTangentialSpeed)) {
			rb2d.AddForce (transform.right * h * forceAmountForRotation);
		}

		if (tangentialVelocity.magnitude > maxTangentialSpeed) {
			// if tangential velocity is getting bigger than some max, cap it
			targetTangentialVelocity = tangentialVelocity * (maxTangentialSpeed / tangentialVelocity.magnitude);
		}

		// jump
		if (canControl && jump) {
			// set target centrifugal velocity to desired jump velocity
			targetCentrifugalVelocity = directionOfPlayerFromPlanet.normalized * jumpVelocity;
			jump = false;
		}

		// apply target velocity composited from tangential and centrifugal components to player
		Vector3 targetVelocity = targetCentrifugalVelocity + targetTangentialVelocity;
		rb2d.velocity = targetVelocity;

		// flip bird depending on horizontal direction of player input
		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();
	}

	void Flip() {
		facingRight = !facingRight;
		transform.localScale = Vector3.Scale (transform.localScale, new Vector3 (-1,1,1) );
	}
}
