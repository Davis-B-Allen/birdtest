using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesOnPlanet : MonoBehaviour {
    public List<GameObject> planetTrees;
    public int multiplier = 2;
	// Use this for initialization
	void Start () {
        planetTrees = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int currentSegmentCount () {
        return (planetTrees.Count + 1) * multiplier;
    }
}
