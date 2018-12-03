using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	public Text timerText;
	[Tooltip("Total amount of time in seconds for one game round.")]
	public float totalTime;
	private float startTime;

	void Start(){
		startTime = Time.time;
	}

	void Update(){
		float remainingTime = totalTime - (Time.time - startTime);
		float minutes = Mathf.Floor(remainingTime / 60);
		float seconds = (remainingTime - minutes);
		timerText.text = minutes.ToString("00") + ":" + seconds.ToString ("00");
	}
}
