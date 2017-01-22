using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class BTBPlayerController : MonoBehaviour {

  public float MoveForce;
  public float MaxSpeed;
  public float MinSpeed;
  public float XVelocityScaling;

  private Rigidbody2D playerRigidbody;

  void Start () {
    playerRigidbody = GetComponent<Rigidbody2D>();
  }
  
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			if (playerRigidbody.velocity.x < MaxSpeed) {
				playerRigidbody.AddForce (new Vector2 (MoveForce, 0));
			} else {
				playerRigidbody.velocity = new Vector2(MaxSpeed, playerRigidbody.velocity.y);
			}
		} else if (playerRigidbody.velocity.x > MinSpeed) {
			if (Mathf.Abs (playerRigidbody.velocity.x) > MinSpeed + 0.01) {
				playerRigidbody.velocity = new Vector2 (MinSpeed+(playerRigidbody.velocity.x-MinSpeed) * XVelocityScaling, playerRigidbody.velocity.y);
			} else {
				playerRigidbody.velocity = new Vector2 (MinSpeed, playerRigidbody.velocity.y);
			}
		} else if(playerRigidbody.velocity.x < MinSpeed){
			playerRigidbody.velocity = new Vector2(MinSpeed, playerRigidbody.velocity.y);
		}

      //Movement to the left
      if(Input.GetKey(KeyCode.LeftArrow)) {
        if(playerRigidbody.velocity.x > -MaxSpeed) {
          playerRigidbody.AddForce(new Vector2(-MoveForce, 0));
        }
      }
  }
}
