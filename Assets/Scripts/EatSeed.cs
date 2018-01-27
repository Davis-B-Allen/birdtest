using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSeed : MonoBehaviour {
	GameObject[] seeds;

	// Use this for initialization
	void Start () {
		seeds = GameObject.FindGameObjectsWithTag("Seed");
	}
	
	// Update is called once per frame
	void Update () {
		seeds = GameObject.FindGameObjectsWithTag("Seed");
		float minDist = 100f;
		GameObject minDistObj;

		//find closest planet
		foreach (GameObject obj in seeds) {
			float dist = Vector3.Distance(gameObject.transform.position, obj.transform.position);

			//update current closest object seen so far
			if (dist <= minDist) {
				minDist = dist;
				minDistObj = obj;
			}
		}

		if (minDistObj != null && Input.GetKeyDown("s")) {
			GameObject.Destroy(minDistObj);
		}
	}
}
