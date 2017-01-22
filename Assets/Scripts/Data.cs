using UnityEngine;
using System.Collections;

[System.Serializable]
public class TrackDataWrapper{
	public TrackDataWrapper(TrackData[] data){
		this.data = data;
	}
	[SerializeField]
	public TrackData[] data;
}

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