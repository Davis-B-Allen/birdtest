using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColors : MonoBehaviour {
    public List<Color> ingestedColors;
	// Use this for initialization
	void Start () {
        ingestedColors = new List<Color>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Color pullRandomColor () {
        return ingestedColors[Random.Range(0, ingestedColors.Count - 1)];
    }
}
