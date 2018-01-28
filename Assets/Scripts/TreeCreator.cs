using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCreator : MonoBehaviour {

    public GameObject treeTrunk;
    public GameObject treeSegment;
    public GameObject planet;
    public GameObject seed;
    Vector3 quadrant;
    public string growthType;
    //public float segmentInterval; 
    public float segmentCount;
    bool creating;
    Vector3 destination;
    Vector3 directionBuried;
    float growthSpeed;

	void Start () {
        creating = true;
        quadrant = new Vector3(1, 1, 0);
        float yDisplacement = transform.position.y - planet.transform.position.y;
        float xDisplacement = transform.position.x - planet.transform.position.x;
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
        destination = transform.position;
        //transform.position = planet.transform.position - transform.position / 2;
        float xDirection = Mathf.Sin(Mathf.Deg2Rad * zRotation);
        float yDirection = Mathf.Cos(Mathf.Deg2Rad * zRotation);
        float obscuringDistance = ((segmentCount + 1) * 3.0f);
        directionBuried = new Vector3(xDirection, -yDirection);
        transform.position += (directionBuried * obscuringDistance);
        growthSpeed = obscuringDistance / 50f;
        //transform.localScale = new Vector3(.1f, 1f);

        if (growthType == "straightUp") {
            straightUp();
        }

	}
	
	void Update () {
        if (creating) {
            transform.position -= directionBuried * growthSpeed;
            if (transform.position == destination) {
                finishCreation();
            }
        }
    }

    void initialize (GameObject trunk, GameObject segment, GameObject planetObj, string type, float interval, float count) {
        treeTrunk = trunk;
        treeSegment = segment;
        planet = planetObj;
        growthType = type;
        //segmentInterval = interval;
        segmentCount = count;
    }

    void finishCreation () {
        creating = false;
        //transform.localScale = new Vector3(1, 1);
        dropSeeds();
    }

    void dropSeeds () {
        Vector3 right = transform.GetChild(1).right;
        Vector3 left = -1 * right;
        Vector3 seedSpawn = directionBuried * .2f;
        GameObject seed1 = Instantiate(seed);
        GameObject seed2 = Instantiate(seed);
		SpriteRenderer sr1 = seed1.GetComponent<SpriteRenderer>();
		SpriteRenderer sr2 = seed2.GetComponent<SpriteRenderer>();
        sr1.sortingOrder = 4;
        sr2.sortingOrder = 4;
        PlanetDescriptor pd = planet.GetComponent<PlanetDescriptor>();
        sr1.color = pd.colorOptions[Random.Range(0,pd.colorOptions.Count)];
		sr2.color = pd.colorOptions[Random.Range(0,pd.colorOptions.Count)];

        seed1.transform.position = transform.position + right * .5f;
        seed2.transform.position = transform.position + left * .5f;
        
    }

    void straightUp () {
        GameObject trunk = Instantiate(treeTrunk);
        trunk.transform.SetParent(transform);
        float interval = 3.0f;
        trunk.transform.localPosition = Vector3.zero;
        trunk.transform.rotation = transform.rotation;
        //trunk.transform.localScale *= interval;
        //float yCrunch = .9f;
        //float spriteHeight = treeSegment.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        //float lastHeight = spriteHeight;
        //float totalHeight = calculateTotalHeight(yCrunch, spriteHeight);
        //float lastY = totalHeight;


        for (int i = 0; i < segmentCount; i++) {
            GameObject nextSegment = Instantiate(treeSegment);
            nextSegment.transform.SetParent(transform);
            //GameObject centerBottom = nextSegment;
            //float currentHeight = lastHeight * yCrunch;
            //float currentY = lastY - currentHeight;
            //Debug.Log(currentY);
            //nextSegment.transform.localPosition = new Vector3(0, currentY, 0);
            //nextSegment.transform.localScale = new Vector3(nextSegment.transform.localScale.x, Mathf.Pow(yCrunch, i), nextSegment.transform.localScale.z);
            //lastHeight = currentHeight;
            //lastY = currentY;
            //float scalingPenalty;
            //float spacingPenalty;
            //if (i < segmentCount / 2) {
            //    scalingPenalty = i / 2.0f;
            //    spacingPenalty = 0.2f;
            //} else {
            //    scalingPenalty = i / 1.5f;
            //    spacingPenalty = 0.2f;

            //}
            //float scaleVectorX = nextSegment.transform.localScale.x * segmentCount / (scalingPenalty + 3.0f);
            //float scaleVectorY = nextSegment.transform.localScale.y + (1 + i / 2.0f);
            //float scaleVectorY = nextSegment.transform.localScale.y;
            //nextSegment.transform.localScale = new Vector3(scaleVectorX, scaleVectorY, 0);

            //float hypotenuse = lastPosition + (interval / (spacingPenalty * i + 1));
            //float yCoord = lastPosition + interval + 1;
            //lastPosition = yCoord;
            //Vector3 segmentPosition = new Vector3(0, yCoord);
            //nextSegment.transform.localPosition = segmentPosition;
            nextSegment.transform.rotation = transform.rotation;
            nextSegment.transform.localPosition = new Vector3(0, interval * i + 3);
        }
    }

    float calculateTotalHeight (float crunch, float height) {
        float result = 0;
        float lastHeight = height;
        for (int i = 0; i < segmentCount; i++) {
            result += lastHeight * crunch;
            lastHeight *= crunch;
        }
        return result;
    }


}
