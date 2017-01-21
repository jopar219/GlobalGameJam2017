using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class BTBPlayerController : MonoBehaviour {

  public float MoveForce;

  private Rigidbody2D rigidbody;

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
}
