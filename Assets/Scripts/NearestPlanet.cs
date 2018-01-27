using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPlanet : MonoBehaviour {
	GameObject[] planets;
	public GameObject closestPlanet;

	// Use this for initialization
	void Start () {
		planets = GameObject.FindGameObjectsWithTag("Planet");
	}
	
	// Update is called once per frame
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
}
