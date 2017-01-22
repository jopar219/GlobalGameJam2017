using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleHueShift : MonoBehaviour {
	public Text beat1;
	public Text the;
	public Text beat2;
	public Text score;

	public Image img1;
	public Image img2;
	public Image img3;
	private Color color = new Color(0.95f, 0.89f, 0.05f);

	public GameObject ball;

	float hue;
	float saturation;
	float value;

	private bool flag = false;
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

		if(!flag) {
			beat1.GetComponent<Outline>().effectColor = newColor;
			beat2.GetComponent<Outline>().effectColor = newColor;
			the.GetComponent<Outline>().effectColor = newColor;

			img1.color = newColor;
			img2.color = newColor;
			img3.color = newColor;
		}
		else {
			score.GetComponent<Outline>().effectColor = newColor;
			score.text = "Score: " + gameManager.Instance.points;
		}

		ballRigidbody.AddForce(new Vector2(12, 0));

		if(Input.GetKey(KeyCode.Space)) {
			flag = true;
			Destroy(beat1.gameObject);
			Destroy(beat2.gameObject);
			Destroy(the.gameObject);
			Destroy(img1.gameObject);
			Destroy(img2.gameObject);
			Destroy(img3.gameObject);
		}
	}
}
