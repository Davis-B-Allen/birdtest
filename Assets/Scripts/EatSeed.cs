using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSeed : MonoBehaviour {
	GameObject[] seeds;
	public GameObject buttReferenceObj;
	Queue<GameObject> stomach;
	float timeSincePoop = 0f;
	public float seedEatDistance = 0.5f;
    public bool isPooping = false;
    AudioController aud;

	// Use this for initialization
	void Start () {
		seeds = GameObject.FindGameObjectsWithTag("Seed");
		stomach = new Queue<GameObject>();
        aud = GetComponent<AudioController>();
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

		if ((minDistObj != gameObject) && Input.GetKeyDown(KeyCode.S)) {
			stomach.Enqueue(minDistObj);
			minDistObj.active = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerColors>().ingestedColors.Add(minDistObj.GetComponent<Seed>().color);
        }

		if (stomach.Count == 0){
			timeSincePoop = 0;
		}
		else {
			if (timeSincePoop >= stomach.Peek().GetComponent<Seed>().digestionPeriod) {
				Seed seedScript = stomach.Peek().GetComponent<Seed>();
				GameObject seed = stomach.Dequeue();
				seed.transform.position = buttReferenceObj.transform.position;
				seed.gameObject.tag = "PoopSeed";
				seed.active = true;
				StartCoroutine(seedScript.GrowTree());
				timeSincePoop = 0;
                isPooping = true;
                aud.poopSoundplayed = false;
                if (aud.poopSoundplayed == true)
                {
                    isPooping = false;
                }
			}
		}
	}
}
