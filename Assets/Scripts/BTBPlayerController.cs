using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class BTBPlayerController : MonoBehaviour {

  public float MoveForce;

  private Rigidbody2D rigidbody;

  private float velY;
  public float TimeTilNextBounce;

  private Vector2 contactCompare;

  void Start () {
    rigidbody = GetComponent<Rigidbody2D>();
  }
  
  void Update () {
    if(Input.GetKey(KeyCode.RightArrow)) {
      rigidbody.AddForce(new Vector2(20, 0));
    }

    if(Input.GetKey(KeyCode.LeftArrow)) {
      rigidbody.AddForce(new Vector2(-20, 0));
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
    
    float groundPos = (other.collider.transform.position.y + (other.collider.transform.localScale.y / 2));
    float newYPos = 2 * groundPos - contactCompare.y + gameObject.GetComponent<CircleCollider2D>().radius;

    transform.position = new Vector2(transform.position.x, newYPos);

    velY = -0.5f * Physics2D.gravity.y * TimeTilNextBounce;
    rigidbody.velocity = new Vector2(rigidbody.velocity.x, velY);
  }
}
