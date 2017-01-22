using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class BTBCameraController : MonoBehaviour {
  public Transform TargetTransform;
  public float MinYThreshold;
  public float MaxXThreshold;
  public float MinXThreshold;
  public float MaxOrthographicSizeThreshold;

  private Camera mainCamera;
  private float minOrthograficSize;
  private float maxOrthograficSize;
  private float oldYTransform;
  private float topYTransform;

  void Start() {
    mainCamera = GetComponent<Camera>();
    oldYTransform = transform.position.y;
    minOrthograficSize = mainCamera.orthographicSize;
    maxOrthograficSize = mainCamera.orthographicSize;
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
        if(mainCamera.orthographicSize < MaxOrthographicSizeThreshold) {
          mainCamera.orthographicSize = minOrthograficSize + TargetTransform.position.y - MinYThreshold;
          topYTransform = transform.position.y;
          maxOrthograficSize = mainCamera.orthographicSize;
        }
      } else {
        if(TargetTransform.position.y < topYTransform) {
          mainCamera.orthographicSize = maxOrthograficSize + TargetTransform.position.y - topYTransform;
        }
      }

      oldYTransform = transform.position.y;
    }
  }
}
