using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleHueShift : MonoBehaviour {
	public Text beat1;
	public Text the;
	public Text beat2;

	public Image img1;
	public Image img2;
	public Image img3;
	private Color color = new Color(0.95f, 0.89f, 0.05f);

	public GameObject ball;

	float hue;
	float saturation;
	float value;

	private Rigidbody2D ballRigidbody;

	// Use this for initialization
	void Start () {
		ballRigidbody = ball.GetComponent<Rigidbody2D>();
		Color.RGBToHSV(color, out hue, out saturation, out value);
	}
	
	// Update is called once per frame
	void Update () {
		float ballPos = Mathf.Ceil(ball.transform.position.x);
		float rango = 30.0f;
		
		hue = ballPos / rango;
		while(hue > 1) {
		  hue -= 1;
		}

		Color newColor = Color.HSVToRGB(hue, saturation, value);

		beat1.GetComponent<Outline>().effectColor = newColor;
		beat2.GetComponent<Outline>().effectColor = newColor;
		the.GetComponent<Outline>().effectColor = newColor;

		img1.color = newColor;
		img2.color = newColor;
		img3.color = newColor;

		ballRigidbody.AddForce(new Vector2(12, 0));

		if(Input.GetKey(KeyCode.Space)) {
			Destroy(gameObject);
		}
	}
}
