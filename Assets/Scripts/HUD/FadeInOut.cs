using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {
    public float delay;
    public float fadeInTime;
    public float appearanceDuration;
    public float fadeOutTime;
    public bool triggerStart;
    float timer;
    string action;
    public List<KeyCode> dismissKeys;
	// Use this for initialization
	void Start () {
        timer = 0;
        if (triggerStart) {
            action = "waiting";
        } else {
            action = "delay";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (action == "delay") {
            delayFade();
        } else if (action == "fade in") {
            fadeIn();
        } else if (action == "remain") {
            //remain();
        } else if (action == "fade out") {
            fadeOut();
        } else if (action == "destroy") {
            //Object.Destroy(gameObject);
           
        }
        bool dismissed = false;
        foreach (KeyCode code in dismissKeys) {
            if (Input.GetKeyDown(code)) {
                dismissed = true;
            }
        }
        if (dismissed && action != "fade out") {
            startFade();
        }
	}

    void delayFade () {
        if (timer == delay || delay < 1) {
            action = "fade in";
            timer = 0;
        } else {
            timer++;
        }
    }

    void fadeIn () {
        if (timer == fadeInTime) {
            action = "remain";
        } else {
            timer++;
            Image image = gameObject.GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, timer / fadeInTime);
        }
    }

    //void remain () {
    //    if (timer == appearanceDuration) {
    //        action = "fade out";
    //        timer = 0;
    //    } else {
    //        timer++;
    //    }
    //}

    void fadeOut () {
        if (timer > fadeOutTime) {
            timer = fadeOutTime;
        } else if (timer < 1) {
            action = "destroy";
        } else {
            timer--;
            Image image = gameObject.GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, timer / fadeOutTime);
        }
    }

    public void startFade () {
        action = "fade out";
    }

    public void startWithTrigger () {
        action = "fade in";
    }
}
