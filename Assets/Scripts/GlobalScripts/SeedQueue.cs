using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedQueue : MonoBehaviour {
    Queue<GameObject> seedQueue;
    bool idle;
    public float timeUntilAction;
    public float fullTime;
	// Use this for initialization
	void Start () {
        seedQueue = new Queue<GameObject>();
        idle = true;
        timeUntilAction = 0;
        fullTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if (idle && seedQueue.Count > 0) {
            idle = false;
            GameObject topSeed = seedQueue.Peek();
            SeedQueueProperties properties = topSeed.GetComponent<SeedQueueProperties>();
            timeUntilAction = properties.queueTime;
            fullTime = properties.queueTime;
        } else if (timeUntilAction > 0) {
            timeUntilAction -= Time.deltaTime;
        } else if (timeUntilAction == 0 && !idle) {
            dequeueSeed();
        }
	}

    void dequeueSeed () {
        GameObject seed = seedQueue.Dequeue();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        seed.transform.position = player.transform.position;
        SeedQueueProperties properties = seed.GetComponent<SeedQueueProperties>();
        properties.readyToSprout = true;
        idle = true;
    }

    void enqueueSeed (GameObject seed) {
        seedQueue.Enqueue(seed);
        seed.SetActive(false);
    }
}
