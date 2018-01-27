using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravity : MonoBehaviour {
	GameObject[] planets;
	Rigidbody2D rb2d;
	public GameObject closestPlanet;
	public float gravityForce = 1f;

	void Start () {
		planets = GameObject.FindGameObjectsWithTag("Planet");
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update () {
		float minDist = 100f;
		GameObject minDistObj = gameObject;

		//find closest planet
		foreach (GameObject obj in planets) {
			float dist = Vector3.Distance(gameObject.transform.position, obj.transform.position);

			//update current closest object seen so far
			if (dist <= minDist) {
				minDist = dist;
				minDistObj = obj;
			}
		}

		closestPlanet = minDistObj;
	}

	void FixedUpdate () {
		if (closestPlanet != null) {
			rb2d.AddForce((closestPlanet.transform.position - transform.position).normalized * gravityForce);
		}
	}
}
