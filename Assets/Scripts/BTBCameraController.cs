using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class BTBCameraController : MonoBehaviour {
  public Transform TargetTransform;
  public float MinYThreshold;
  public float MaxXThreshold;
  public float MinXThreshold;
  public float MaxOrthographicSizeThreshold;
  public float ZoomingFactor;

  private Camera mainCamera;
  private float minOrthograficSize;
  private float maxOrthograficSize;
  private float oldYPosition;
  private float topYPosition;
  private float originalYPosition;

  void Start() {
    mainCamera = GetComponent<Camera>();
    originalYPosition = transform.position.y;
    oldYPosition = transform.position.y;
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
      transform.position = new Vector3(transform.position.x, originalYPosition, transform.position.z);
      mainCamera.orthographicSize = minOrthograficSize;
    } else {
      transform.position = new Vector3(transform.position.x, TargetTransform.position.y - (MinYThreshold - originalYPosition), transform.position.z);
      if(transform.position.y > oldYPosition) {
        if(mainCamera.orthographicSize < MaxOrthographicSizeThreshold) {
          mainCamera.orthographicSize = minOrthograficSize + (TargetTransform.position.y - (MinYThreshold - originalYPosition)) * ZoomingFactor;
          topYPosition = transform.position.y + (MinYThreshold - originalYPosition);
          maxOrthograficSize = mainCamera.orthographicSize;
        }
      } else {
        if(TargetTransform.position.y < topYPosition) {
          mainCamera.orthographicSize = maxOrthograficSize + (TargetTransform.position.y - topYPosition) * ZoomingFactor;
        }
      }

      oldYPosition = transform.position.y;
    }
  }
}
