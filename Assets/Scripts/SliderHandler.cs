using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public MusicRecorder recorder;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBeginDrag(PointerEventData eventData){
		recorder.SeekStart();
	}
	public void OnDrag(PointerEventData eventData){
		recorder.Seek(GetComponent<Slider>().value);
	}
	public void OnEndDrag(PointerEventData eventData){
		recorder.SeekEnd ();
	}
}
