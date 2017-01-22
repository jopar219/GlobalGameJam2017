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
    Debug.Log ("1: "+MusicManager.instance.GetTimeToNext (0));
    Debug.Log ("2: "+MusicManager.instance.GetTimeToNext (0));
	TimeTilNextBounce = MusicManager.instance.GetTimeToNext (0);
  }
  
  void OnCollisionEnter2D(Collision2D coll) {
    /*
    foreach(ContactPoint2D ballHit in coll.contacts) {
      if(contactCompare == null) {
        contactCompare = ballHit.point;
      } else if(contactCompare.magnitude < ballHit.point.magnitude) {
        contactCompare = ballHit.point;
      }
    }
    */

    //Correct overlap
    //float groundPos = (coll.collider.transform.position.y + (coll.collider.transform.localScale.y / 2));
    //float newYPos = 2 * groundPos - contactCompare.y + gameObject.GetComponent<CircleCollider2D>().radius;

    //transform.position = new Vector2(transform.position.x, newYPos);
	
		Physics2D.gravity = new Vector2(Physics2D.gravity.x, -(1/Mathf.Pow(TimeTilNextBounce,1.7f))*60);
    velY = -0.5f * Physics2D.gravity.y * TimeTilNextBounce;

    rigidbody.velocity = new Vector2(coll.relativeVelocity.x, velY);
  }
}
