using UnityEngine;
using System.Collections;

public class ColissionCameraShake : MonoBehaviour {
  public float Duration;
  public Camera MainCamera;
  public float MagnitudeScaleFactor;

  private float elapsed;
  private Vector3 originalCamPos;
  private float magnitude;

  IEnumerator Shake() {
    elapsed = 0.0f;
    magnitude = Mathf.Sqrt(gameObject.GetComponent<Rigidbody2D>().velocity.y) * MagnitudeScaleFactor;

    while (elapsed < Duration) {
      originalCamPos = MainCamera.transform.position;
      
      elapsed += Time.deltaTime;          
      
      float percentComplete = elapsed / Duration;         
      float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
      
      // map value to [-1, 1]
      float x = Random.value * 2.0f - 1.0f;
      float y = Random.value * 2.0f - 1.0f;
      x *= magnitude * damper;
      y *= magnitude * damper;
      
      MainCamera.transform.position = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);
          
      yield return null;
    }
    
    MainCamera.transform.position = originalCamPos;
  }

  void OnCollisionEnter2D(Collision2D coll) {
    if(coll.gameObject.tag == "Ground") {
      StartCoroutine(Shake());
    }
  }
}
