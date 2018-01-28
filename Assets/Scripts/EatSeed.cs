using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSeed : MonoBehaviour {
	public GameObject buttReferenceObj;
	Queue<GameObject> stomach;
	float timeSincePoop = 0f;
	public float seedEatDistance = 0.5f;
	NearestPlanet np;
	GameObject nearestPlanet;
	SpriteRenderer sr;
	PlayerColors pc;
	public Color targetColor = Color.white;
	float colorTransitionTime = 24f;
	float currentTransitionTime = 0f;
    public bool isPooping = false;
    AudioController aud;

	// Use this for initialization
	void Start () {
		stomach = new Queue<GameObject>();
		np = gameObject.GetComponent<NearestPlanet>();
		sr = gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
		pc = gameObject.GetComponent<PlayerColors>();
		nearestPlanet = np.closestPlanet;
        aud = GetComponent<AudioController>();
	}

	// Update is called once per frame
	void Update () {
		//when we get to a new planet we reset the colors we are
		if (np.closestPlanet != nearestPlanet) {
			pc.ingestedColors.Clear();
			nearestPlanet = np.closestPlanet;
		}

        targetColor = nearestPlanet.GetComponent<PlanetDescriptor>().planetBirdColor;
        currentTransitionTime += Time.deltaTime;
		float colorShiftPercent = Mathf.Min(1.0f, currentTransitionTime/colorTransitionTime)/3*Mathf.Min(3, pc.ingestedColors.Count);
		//Debug.Log(colorShiftPercent);
		//Debug.Log(pc.ingestedColors.Count);
		if (sr.color != targetColor) {
			sr.color = Color.Lerp(sr.color, targetColor, colorShiftPercent);
		}
		else {
			currentTransitionTime = 0f;
		}

		timeSincePoop += Time.deltaTime;
		GameObject[] seeds = GameObject.FindGameObjectsWithTag("Seed");
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

		//eat seed
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
				SpriteRenderer sr1 = seed.GetComponent<SpriteRenderer>();
				sr1.color = Color.white;
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
