using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCreator : MonoBehaviour {

    public GameObject treeTrunk;
    public GameObject treeSegment;
    Vector3 quadrant;
    public string growthType;
    public float segmentInterval;
    public float segmentCount;

	void Start () {

        GameObject gravity = GameObject.FindGameObjectWithTag("Gravity");
        quadrant = new Vector3(1, 1, 0);
        float yDisplacement = transform.position.y - gravity.transform.position.y;
        float xDisplacement = transform.position.x - gravity.transform.position.x;
        float slope = yDisplacement / xDisplacement;
        float zRotation = Mathf.Rad2Deg * (Mathf.Atan(slope) - Mathf.PI / 2);
        if (xDisplacement < 0) {
            zRotation += 180f;
            quadrant.x = -1;
        }
        if (yDisplacement < 0) {
            quadrant.y = -1;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
        if (growthType == "straightUp") {
            straightUp();
        }
	}
	
	void Update () {
		
	}

    void straightUp () {
        GameObject trunk = Instantiate(treeTrunk);
        trunk.transform.SetParent(transform);
        trunk.transform.localPosition = Vector3.zero;
        trunk.transform.rotation = transform.rotation;
        for (int i = 0; i < segmentCount; i++) {
            GameObject nextSegment = Instantiate(treeSegment);
            nextSegment.transform.SetParent(transform);
            float hypotenuse = segmentInterval * (i + 1);
            float xCoord = 0 * hypotenuse;
            float yCoord = 1 * hypotenuse;
            Vector3 segmentPosition = new Vector3(xCoord, yCoord);
            nextSegment.transform.localPosition = segmentPosition;
            nextSegment.transform.rotation = transform.rotation;
        }
    }


}
