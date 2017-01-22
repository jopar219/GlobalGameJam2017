using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour {

	public static MusicManager instance;
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

	void Awake(){
		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
		}

		instance = this;
		DontDestroyOnLoad( this.gameObject );
	}
	void Start(){
		audioSource = GetComponent<AudioSource> ();
		data = new Track[songs.Length];

		for (int i = 0; i < data.Length; i++) {
			data [i] = new Track (songs [i].path);
		}
		PlaySong (0);
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
		List<SubscribeHandler> subscriber;
		if (subscribers.TryGetValue (instrument, out subscriber)) {
			for (int i = 0; i < subscriber.Count; i++) {
				subscriber [i] ();
			}
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
		List<SubscribeHandler> subscriber;
		if (!subscribers.TryGetValue (instrument, out subscriber)) {
			subscribers [instrument] = new List<SubscribeHandler> ();
			subscriber = subscribers [instrument];
		}

		subscriber.Add(callback);
	}

	public float GetTimeToNext(int instrument){
		int leftIndex = currentIndex-1;
		int rightIndex = currentIndex;

		while (leftIndex >= 0 && data [currentSong].data [leftIndex].index != instrument) {
			leftIndex--;
		}
		while (rightIndex < data [currentSong].data.Length && data [currentSong].data [rightIndex].index != instrument) {
			rightIndex++;
		}
		if (leftIndex == -1)
			return data [currentSong].data [rightIndex].time;
		if (rightIndex == data [currentSong].data.Length)
			return 0;
		return data [currentSong].data [rightIndex].time-data [currentSong].data [leftIndex].time;
	}
}
