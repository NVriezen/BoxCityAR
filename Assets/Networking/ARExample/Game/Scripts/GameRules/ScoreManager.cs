using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class ScoreManager : MonoBehaviour {

	public Text[] playerScores = new Text[4];
	private int[] scores = new int[4];

    //private UnityAction<UnityEvents.I> someListener = AddScore;

	void Start(){
		foreach (Text t in playerScores){
			t.text = "Score: 000";
		}
		//EventManager.StartListening ("SCORE_PLAYER_" + playerNum, AddScore);
	}

	//Change this to event based updating!
	void AddScore(int playerNum){
        Photon.Realtime.Player player = null;
        PhotonNetwork.CurrentRoom.Players.TryGetValue(playerNum, out player);
        scores [ playerNum ] += 1;
        player.AddScore(1);
		//playerScores [ playerNum ].text = "Score: " + scores [ playerNum ].ToString("000");;
	}
}
