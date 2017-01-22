using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public struct TrackData{
	public TrackData(float time, int index){
		this.time = time;
		this.index = index;
	}
	[SerializeField]
	public float time;
	[SerializeField]
	public int index;
}

[System.Serializable]
public class Track{
	[SerializeField]
	public TrackData[] data = new TrackData[0];

	public Track(string path){
		Load (path);
	}
	public Track(TrackData[] data){
		this.data = data;
	}
	public void Load(string path){
		Debug.Log ("loading from: " + path);
		string data = File.ReadAllText(path);
		Debug.Log (data);
		TrackData[] tmp = JsonUtility.FromJson<Track> (data).data;
		if (tmp != null) {
			this.data = tmp;
		}
	}

	public void Save(string path){
		Debug.Log ("saving to: " + path);
		string data = JsonUtility.ToJson (this);
		Debug.Log (data);
		File.WriteAllText(path, data);
	}


}