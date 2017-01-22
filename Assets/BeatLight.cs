using UnityEngine;
using System.Collections;

public class BeatLight : MonoBehaviour {

	Light light;

	public int instrument = 0;
	public float factor = 1f;
	public float maxIntensity = 1f;
	public float minIntensity = 3f;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
		MusicManager.instance.Subscribe (LightUp, instrument);
	}

	IEnumerator lightUp(){
		while (light.intensity < maxIntensity) {
			light.intensity += Time.fixedDeltaTime*factor;
			yield return new WaitForFixedUpdate ();	
		}
		while (light.intensity > minIntensity) {
			light.intensity -= Time.fixedDeltaTime*factor;
			yield return new WaitForFixedUpdate ();	
		}
	}

	// Update is called once per frame
	public void LightUp () {
		Debug.Log ("lit");
		StartCoroutine (lightUp());
	}
}
