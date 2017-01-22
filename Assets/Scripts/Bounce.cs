using UnityEngine;
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
	
	}
  
  void OnCollisionEnter2D(Collision2D coll) {

    velY = -0.5f * Physics2D.gravity.y * TimeTilNextBounce;

    rigidbody.velocity = new Vector2(coll.relativeVelocity.x, velY);
  }
}
