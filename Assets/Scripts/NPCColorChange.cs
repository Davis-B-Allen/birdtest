using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCColorChange : MonoBehaviour {
	GameObject player;
	SpriteRenderer playerSprite;
	public float colorChangeTime = 4f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerSprite = player.transform.GetChild(1).GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= 10) {
			StartCoroutine(ChangeColor(playerSprite.color));
		}
	}

	IEnumerator ChangeColor (Color target) {
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
		float accum = 0;
		while (sr.color != target) {
			accum += Time.deltaTime;
			float percentComplete = accum / colorChangeTime;
			Color.Lerp(sr.color, target, accum);
			yield return new WaitForEndOfFrame();
		}
	}
}
