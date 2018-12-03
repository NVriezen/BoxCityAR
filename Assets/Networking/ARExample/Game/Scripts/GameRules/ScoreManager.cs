using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text[] playerScores = new Text[4];
	private int[] scores = new int[4];

	void Start(){
		foreach (Text t in playerScores){
			t.text = "Score: 000";
		}
		//EventManager.StartListening ("Score", AddScore);
	}

	//Change this to event based updating!
	void AddScore(int playerNum){
		scores [ playerNum ] += 1;
		playerScores [ playerNum ].text = "Score: " + scores [ playerNum ].ToString("000");;
	}
}
