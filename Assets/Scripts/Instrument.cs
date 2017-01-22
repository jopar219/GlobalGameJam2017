using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Instrument : MonoBehaviour {

	public string key;
	public Image img;
	public InputField inputField;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator lightUp(){
		img.color = Color.red;
		yield return new WaitForSeconds (0.2f);
		img.color = Color.white;
	}

	public void LightUp(){
		StartCoroutine (lightUp());
	}
	public void OnChange(string val){
		key = ""+val[0];
	}	
}
