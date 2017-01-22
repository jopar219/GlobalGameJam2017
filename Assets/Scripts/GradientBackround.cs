using UnityEngine;
using System.Collections;

public class GradientBackround : MonoBehaviour {

  private static Color[] colors = new Color[] {
    new Color(0.95f, 0.89f, 0.05f), 
    new Color(0.0f, 0.0f, 0.0f)
  };

  float hue;
  float saturation;
  float value;

  private Color bottomColor = colors[0];
  private Color topColor = colors[1];

  public int gradientLayer = 7;
  public GameObject ball;
  private Mesh mesh;

  void Awake() {
    Color.RGBToHSV(colors[0], out hue, out saturation, out value);

    gradientLayer = Mathf.Clamp(gradientLayer, 0, 31);

    if (!GetComponent<Camera>()) {
      Debug.LogError ("Must attach GradientBackground script to the camera");
      return;
    }
    
    GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
    GetComponent<Camera>().cullingMask = GetComponent<Camera>().cullingMask & ~(1 << gradientLayer);
    Camera gradientCam = new GameObject("Gradient Cam", typeof(Camera)).GetComponent<Camera>();
    gradientCam.depth = GetComponent<Camera>().depth - 1;
    gradientCam.cullingMask = 1 << gradientLayer;
    
    mesh = new Mesh();
    mesh.vertices = new Vector3[4] {
      new Vector3(-100f, .577f, 1f), 
      new Vector3(100f, .577f, 1f), 
      new Vector3(-100f, -.577f, 1f), 
      new Vector3(100f, -.577f, 1f)
    };

    mesh.triangles = new int[6] {0, 1, 2, 1, 3, 2};

    mesh.colors = new Color[4] {
      topColor, 
      topColor, 
      bottomColor, 
      bottomColor
    };
 
    Shader shader = Shader.Find("Gradient");
    Material mat = new Material(shader);
    GameObject gradientPlane = new GameObject("Gradient Plane", typeof(MeshFilter), typeof(MeshRenderer));
 
    ((MeshFilter)gradientPlane.GetComponent(typeof(MeshFilter))).mesh = mesh;
    gradientPlane.GetComponent<Renderer>().material = mat;
    gradientPlane.layer = gradientLayer;
  }
  // Use this for initialization
  void Start () {
  
  }
  
  // Update is called once per frame
  void Update () {
    float ballPos = Mathf.Ceil(ball.transform.position.y);
    float rango = 200.0f;
    
    hue = ballPos / rango;
    while(hue > 1) {
      hue -= 1;
    }

    bottomColor = Color.HSVToRGB(hue, saturation, value);
    UpdateGradients();
  }

  void UpdateGradients() {
    mesh.colors = new Color[4] {topColor, topColor, bottomColor, bottomColor};
  }
}
