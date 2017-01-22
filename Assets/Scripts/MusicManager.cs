using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour {

	[System.Serializable]
	public struct Song{
		[SerializeField] public AudioClip clip;
		[SerializeField] public string path;
	}

	public Song[] songs;
	Track[] data;

	public delegate void SubscribeHandler();
	Dictionary<int, List<SubscribeHandler>> subscribers = new Dictionary<int, List<SubscribeHandler>>();

	AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource> ();
		data = new Track[songs.Length];

		for (int i = 0; i < data.Length; i++) {
			data [i] = new Track (songs [i].path);
		}
	}


	float time = 0;
	int currentSong = -1;
	int currentIndex = 0;
	// Update is called once per frame
	void Update () {
		if (currentSong != -1) {
			while (data[currentSong].data.Length > currentIndex &&
				data[currentSong].data [currentIndex].time < audioSource.time) {
				EmitEvent(data[currentSong].data[currentIndex].index);
				currentIndex++;	
			}
		}
	}

	void EmitEvent(int instrument){
		for (int i = 0; i < subscribers [instrument].Count; i++) {
			subscribers [instrument][i]();
		}
	}

	public void PlaySong(int i){
		currentSong = i;
		audioSource.Stop ();
		audioSource.clip = songs [i].clip;
		audioSource.time = 0;
		currentIndex = 0;
		audioSource.Play ();
	}
		
	public void Subscribe(SubscribeHandler callback, int instrument){
		subscribers[instrument].Add(callback);
	}

	public float GetTimeToNext(int instrument){
		int leftIndex = currentIndex;
		int rightIndex = currentIndex;

		while (data [currentSong].data [leftIndex].index != instrument) {
			leftIndex--;
		}
		while (data [currentSong].data [rightIndex].index != instrument) {
			rightIndex++;
		}

		return data [currentSong].data [rightIndex].time-data [currentSong].data [leftIndex].time;
	}
}
