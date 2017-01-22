using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MusicRecorder : MonoBehaviour {

	public List<TrackData> recording = new List<TrackData>();

	public AudioClip[] tracks;
	public RectTransform Content;
	public GameObject InstrumentPrefab;

	public Text playButton;
	public Text recordButton;
	public Dropdown dropdown;
	public Slider slider;
	public InputField path;

	public List<Instrument> instruments;
	public bool isRecording = false;

	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		ChangeTrack (dropdown.value);
	}

	bool wasPlaying = false;
	bool seeking = false;

	int currentIndex = 0;
	// Update is called once per frame
	void Update () {
		
		if (audioSource.isPlaying && !wasPlaying) {
			playButton.text = "\u23f8";
			wasPlaying = true;
		} else if(!audioSource.isPlaying && wasPlaying) {
			playButton.text = "\u25b6";
			wasPlaying = false;
		}
		if (!seeking) {
			slider.value = audioSource.time / audioSource.clip.length;
		}

		for (int i = 0; i < instruments.Count; i++) {
			if (instruments [i].key == "")
				continue;
			if (Input.GetKeyDown (instruments [i].key)) {
				Debug.Log ("Recording data: " + audioSource.time + ", " + i);
				recording.Add (new TrackData(audioSource.time, i));
				instruments [i].LightUp();
			}
		}

		while (recording.Count > currentIndex && recording [currentIndex].time < audioSource.time) {
			Debug.Log ("recording["+currentIndex+"].index = "+recording [currentIndex].index);
			instruments [recording [currentIndex].index].LightUp ();
			currentIndex++;	
		}
	}

	public void Play(){
		if (audioSource.isPlaying) {
			audioSource.Pause ();
		} else {
			audioSource.Play ();
		}
	}
	public void Rec(){
		if (!isRecording) {
			isRecording = true;
			recordButton.color = Color.red;
			if (!audioSource.isPlaying) {
				audioSource.Play ();
			}
		} else {
			recordButton.color = Color.black;
			audioSource.Pause ();
			isRecording = false;
		}
	}
	public void Stop(){
		audioSource.time = 0;
		audioSource.Stop ();

		currentIndex = 0;
	}
	public void AddInstrument(){
		GameObject instrument = Instantiate (InstrumentPrefab, Content) as GameObject;
		instrument.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,instruments.Count * -30);
		instruments.Add (instrument.GetComponent<Instrument>());
	}

	int currentTrack = -1;
	public void ChangeTrack(int i){
		Debug.Log ("Changing track to: " + i);
		if (currentTrack != -1) {
			Save ();
		}
		currentTrack = i;
		audioSource.clip = tracks [currentTrack];
	}

	public void Seek(float value){
		float timeToSet = value * audioSource.clip.length;
		if (audioSource.time > timeToSet) {
			while (recording.Count > currentIndex && recording [currentIndex].time > audioSource.time) {
				instruments [recording [currentIndex].index].LightUp ();
				currentIndex--;	
			}
		}
		audioSource.time = timeToSet;
	}
	public void SeekEnd(){
		seeking = false;
	}
	public void SeekStart(){
		seeking = true;
	}

	public void Load(){
		string data = File.ReadAllText(path.text);
		TrackData[] tmp = JsonUtility.FromJson<TrackDataWrapper> (data).data;
		if (tmp != null) {
			recording = new List<TrackData> (tmp);
		}
		int instrumentNum = -1;
		for (int i = 0; i < recording.Count; i++) {
			if (recording [i].index > instrumentNum) {
				instrumentNum = recording [i].index;
			}
		}
		for (int i = 0; i < instrumentNum+1; i++) {
			AddInstrument ();
		}
		currentIndex = 0;
	}
	public void Save(){
		string data = JsonUtility.ToJson (new TrackDataWrapper(recording.ToArray()));
		Debug.Log ("Saving to " + path.text);
		File.WriteAllText(path.text, data);
	}
}
