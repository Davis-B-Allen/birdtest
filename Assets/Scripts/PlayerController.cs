using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float playerSpeed = 1.0f;
    public float jumpHeight = 1.0f;
    public bool onGround;
    public bool peck = false;
    public bool jumping;
    Rigidbody2D rb;
	
    // Use this for initialization
	void Start ()
    {
    rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update ()
    {
      if (Input.GetKeyDown(KeyCode.A)) //move left
        {
            rb.velocity = new Vector2 (-playerSpeed, 0);
        }
      if (Input.GetKeyDown(KeyCode.D)) //move right
        {
            rb.velocity = new Vector2 (playerSpeed, 0);
        }
      //else if ((!Input.GetKeyDown(KeyCode.A)) || (!Input.GetKeyDown(KeyCode.A)))
        //{
          //  rb.velocity = new Vector2(0, 0);
        //}
      if (Input.GetKeyDown(KeyCode.S)) //peck
        {
            peck = true;
        }
      else if (!Input.GetKeyDown(KeyCode.S)) //if not holding button, not pecking anymore
        {
            peck = false;
        }
      if (Input.GetKeyDown(KeyCode.Space)) //jump upward, no arc though?
        {
            jumping = true;
            rb.velocity = new Vector2(0, jumpHeight);
        }
	}
}
