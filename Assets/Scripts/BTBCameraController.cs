using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class BTBCameraController : MonoBehaviour {
  public Transform TargetTransform;
  public float MinYThreshold;
  public float MaxXThreshold;
  public float MinXThreshold;

  private Camera camera;
  private float minOrthograficSize;
  private float maxOrthograficSize;
  private float minXTransform;
  private float oldYTransform;
  private float topYTransform;

  void Start() {
    camera = GetComponent<Camera>();
    oldYTransform = transform.position.y;
    minXTransform = camera.transform.position.x;
    minOrthograficSize = camera.orthographicSize;
    maxOrthograficSize = camera.orthographicSize;
  }

  void Update () {
    if(TargetTransform.position.x - transform.position.x >= MaxXThreshold) {
      transform.position = new Vector3(TargetTransform.position.x - MaxXThreshold, transform.position.y, transform.position.z);
    }
    
    if(TargetTransform.position.x - transform.position.x < MinXThreshold) {
      transform.position = new Vector3(TargetTransform.position.x - MinXThreshold, transform.position.y, transform.position.z);
    }
    
    if(TargetTransform.position.y < MinYThreshold) {
      transform.position = new Vector3(transform.position.x, MinYThreshold, transform.position.z);
    } else {
      transform.position = new Vector3(transform.position.x, TargetTransform.position.y, transform.position.z);
      
      if(transform.position.y > oldYTransform) {
        camera.orthographicSize = minOrthograficSize + TargetTransform.position.y - MinYThreshold;
        topYTransform = transform.position.y;
        maxOrthograficSize = camera.orthographicSize;
      } else {
        camera.orthographicSize = maxOrthograficSize + TargetTransform.position.y - topYTransform;
      }

      oldYTransform = transform.position.y;
    }
  }
}
