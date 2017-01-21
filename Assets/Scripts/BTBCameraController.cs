using UnityEngine;
using System.Collections;

public class BTBCameraController : MonoBehaviour {
  private float initialY;

	// Use this for initialization
	void Start () {
	  initialY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
    transform.position = new Vector2(transform.position.x, initialY);
	}
}
