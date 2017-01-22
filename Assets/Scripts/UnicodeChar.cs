using System.Text.RegularExpressions;
using System.Globalization;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnicodeChar : MonoBehaviour {

	public string unicode;
	// Use this for initialization
	void Start () {
		var result = Regex.Replace(
			unicode,
			@"\\[Uu]([0-9A-Fa-f]{4})",
			m => char.ToString(
				(char)ushort.Parse(m.Groups[1].Value, NumberStyles.AllowHexSpecifier)));

		Debug.Log (result);
		GetComponent<Text> ().text = result;
	}
	
	public void SetUnicode(string code){
		unicode = code;
		Start();
	}
}
