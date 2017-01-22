﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class Bounce : MonoBehaviour {
  public float TimeTilNextBounce;
  
  private float velY;
  private Vector2 contactCompare;
  private Rigidbody2D rigidbody;

	// Use this for initialization
  void Start () {
    rigidbody = GetComponent<Rigidbody2D>();
  }
	
  // Update is called once per frame
  void Update () {
    Debug.Log ("1: "+MusicManager.instance.GetTimeToNext (0));
    Debug.Log ("2: "+MusicManager.instance.GetTimeToNext (0));
	  TimeTilNextBounce = MusicManager.instance.GetTimeToNext (0);
  }
  
  void OnCollisionEnter2D(Collision2D coll) {

    velY = -0.5f * Physics2D.gravity.y * TimeTilNextBounce;

    rigidbody.velocity = new Vector2(coll.relativeVelocity.x, velY);
  }
}
