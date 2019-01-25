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
     TimeTilNextBounce = MusicManager.instance.GetTimeToNext (0);
  }
  
  void OnCollisionEnter2D(Collision2D coll) {
	
    Physics2D.gravity = new Vector2(Physics2D.gravity.x, -(1/Mathf.Pow(TimeTilNextBounce,1.7f))*60);
    velY = -0.5f * Physics2D.gravity.y * TimeTilNextBounce;

    rigidbody.velocity = new Vector2(-coll.relativeVelocity.x, velY);
  }
}
