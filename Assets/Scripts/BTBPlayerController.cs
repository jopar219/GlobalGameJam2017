using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class BTBPlayerController : MonoBehaviour {

  public float MoveForce;
  public float MaxSpeed;
  public float XVelocityScaling;
  public float TimeTilNextBounce;

  private bool isPushing;
  private Rigidbody2D rigidbody;
  private float velY;
  private Vector2 contactCompare;

  void Start () {
    rigidbody = GetComponent<Rigidbody2D>();
  }
  
	void Update () {
    isPushing = false;

	  if(Input.GetKey(KeyCode.RightArrow)) {
      if(rigidbody.velocity.x < MaxSpeed) {
        rigidbody.AddForce(new Vector2(MoveForce, 0));
      }

      isPushing = true;
    }

    //Movement to the left
    /*
    if(Input.GetKey(KeyCode.LeftArrow)) {
      if(rigidbody.velocity.x > -MaxSpeed) {
        rigidbody.AddForce(new Vector2(-MoveForce, 0));
      }

      isPushing = true;
    }
    */
    
    if(!isPushing) {
      if(Mathf.Abs(rigidbody.velocity.x) > 0.01) {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x * XVelocityScaling , rigidbody.velocity.y);
      } else {
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
      }
    }
  }

  void OnCollisionEnter2D(Collision2D other) {

    foreach(ContactPoint2D ballHit in other.contacts)
    {
      if(contactCompare == null) {
        contactCompare = ballHit.point;
      }
      else {
        if(contactCompare.magnitude < ballHit.point.magnitude) {
          contactCompare = ballHit.point;
        }
      }
    }
    
    //Correct overlap
    float groundPos = (other.collider.transform.position.y + (other.collider.transform.localScale.y / 2));
    float newYPos = 2 * groundPos - contactCompare.y + gameObject.GetComponent<CircleCollider2D>().radius;

    transform.position = new Vector2(transform.position.x, newYPos);

    velY = -0.5f * Physics2D.gravity.y * TimeTilNextBounce;
    rigidbody.velocity = new Vector2(rigidbody.velocity.x, velY);
  }
}
