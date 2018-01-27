using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSeed : MonoBehaviour {
	GameObject[] seeds;
	GameObject buttReferenceObj;
	Queue<GameObject> stomach;
	float timeSincePoop = 0f;
	public float seedEatDistance = 0.5f;

	// Use this for initialization
	void Start () {
		seeds = GameObject.FindGameObjectsWithTag("Seed");
	}
	
	// Update is called once per frame
	void Update () {
		timeSincePoop += Time.deltaTime;
		seeds = GameObject.FindGameObjectsWithTag("Seed");
		float minDist = 100f;
		GameObject minDistObj = gameObject;

		//find closest seed
		foreach (GameObject obj in seeds) {
			float dist = Vector3.Distance(gameObject.transform.position, obj.transform.position);

			//update current closest object seen so far
			if (dist <= seedEatDistance) {
				minDist = dist;
				minDistObj = obj;
			}
		}

		if (minDistObj != gameObject && Input.GetKeyDown("s")) {
			stomach.Enqueue(minDistObj);
			minDistObj.active = false;
		}

		Seed nextSeed = stomach.Peek().GetComponent<Seed>();

		if (stomach.Count == 0){
			timeSincePoop = 0;
		}
		else if (timeSincePoop >= nextSeed.digestionPeriod) {
			GameObject seed = stomach.Dequeue();
			seed.transform.position = buttReferenceObj.transform.position;
			seed.gameObject.tag = "PoopSeed";
			seed.active = true;
			StartCoroutine(nextSeed.GrowTree());
			timeSincePoop = 0;
		}
	}
}
