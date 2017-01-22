using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

  // Use this for initialization
  void Start () {
    gameManager.Instance.points = 0;
  }
  
  // Update is called once per frame
  void Update () {

  }

  void OnCollisionEnter2D(Collision2D other) {
    if(other.gameObject.tag == "Obstacle") {
      gameManager.Instance.points -= 50;
    }
    else {
      gameManager.Instance.points++;
    }
  }
}
