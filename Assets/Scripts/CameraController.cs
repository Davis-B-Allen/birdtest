using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float zoomInSize = 20;
	public float zoomOutSize = 100;
	public float duration = 0.5f;

	private bool zoomedIn = true;
	private bool transitioning = false;
	private float elapsed = 0.5f;


	// Use this for initialization
	void Start () {
		zoomedIn = true;
		Camera.main.orthographicSize = zoomInSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z) && !transitioning) {
			StartCameraZoom ();
		}
		if (transitioning) {
			elapsed += Time.deltaTime / duration;
			if (zoomedIn) {
				Camera.main.orthographicSize = Mathf.Lerp (zoomInSize, zoomOutSize, elapsed);
			} else {
				Camera.main.orthographicSize = Mathf.Lerp (zoomOutSize, zoomInSize, elapsed);
			}
			if (elapsed > 1.0f) {
				transitioning = false;
				zoomedIn = !zoomedIn;
			}
		}
	}

	void StartCameraZoom() {
		elapsed = 0f;
		transitioning = true;
//		if (zoomedIn) {
//			Camera.main.orthographicSize = zoomInSize;
//		} else {
//			Camera.main.orthographicSize = zoomOutSize;
//		}
	}
}
