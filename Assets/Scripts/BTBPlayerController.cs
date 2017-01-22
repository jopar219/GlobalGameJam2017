using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class BTBPlayerController : MonoBehaviour {

  public float MoveForce;
  public float MaxSpeed;
  public float XVelocityScaling;

  private bool isPushing;
  private Rigidbody2D rigidbody;

  void Start () {
    rigidbody = GetComponent<Rigidbody2D>();
  }
  
	void Update () {
    isPushing = false;

	  if(Input.GetKey(KeyCode.Space)) {
      if(rigidbody.velocity.x < MaxSpeed) {
        rigidbody.AddForce(new Vector2(MoveForce, 0));
      }
    } else if(rigidbody.velocity.x > 0){
      if(Mathf.Abs(rigidbody.velocity.x) > 0.01) {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x * XVelocityScaling , rigidbody.velocity.y);
      } else {
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
      }
    }

    //Movement to the left
    if(Input.GetKey(KeyCode.LeftArrow)) {
      if(rigidbody.velocity.x > -MaxSpeed) {
        rigidbody.AddForce(new Vector2(-MoveForce, 0));
      }

      isPushing = true;
    }
  }
}
