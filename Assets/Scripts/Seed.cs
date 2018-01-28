using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {
	public Object treePrefab;
	public float digestionPeriod = 3f;
	public float treeGrowDelay = 2f;
	bool canGrow = false;
	ObjectGravity objGrav;

	// Use this for initialization
	void Start () {
		objGrav = GetComponent<ObjectGravity>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Planet" && gameObject.tag == "PoopSeed") {
			canGrow = true;
		}
	}

	IEnumerator WaitForPlanet () {
		while (!canGrow) {
			yield return new WaitForEndOfFrame();
		}
	}

	public IEnumerator GrowTree () {
		//wait until we hit the ground once (hopefully we'll stay there)
		yield return WaitForPlanet();
		//wait for some time before spawning tree and setting its planet, and parent
		yield return new WaitForSeconds(treeGrowDelay);
		GameObject tree = (GameObject) GameObject.Instantiate(treePrefab, transform.position, Quaternion.identity);//, (objGrav.closestPlanet.transform.position - transform.position).normalize);
        tree.GetComponent<TreeCreator>().segmentCount = gameObject.GetComponent<ObjectGravity>().closestPlanet.GetComponent<TreesOnPlanet>().currentSegmentCount();
        GameObject.Destroy(gameObject);
		TreeCreator tc = tree.GetComponent<TreeCreator>();
		tc.planet = objGrav.closestPlanet;
		//tree.transform.parent = tc.planet.transform;
	}
}
