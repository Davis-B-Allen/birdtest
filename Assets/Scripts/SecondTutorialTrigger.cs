using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTutorialTrigger : MonoBehaviour {
    GameObject player;
    public GameObject EatTutorial;
    bool completed;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        completed = false;
    }

    // Update is called once per frame
    void Update () {
        if (!completed && Vector3.Distance(player.transform.position, transform.position) < 5f) {
            EatTutorial.GetComponent<FadeInOut>().startWithTrigger();
            completed = true;
        }
    }
}
