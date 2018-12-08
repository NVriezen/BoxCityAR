using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSender : MonoBehaviour {

	public int playerNum = 1;

	void OnTriggerEnter(Collider collider){
		EventManager.TriggerEvent ("SCORE_PLAYER_" + playerNum);
		//BroadcastMessage("AddScore", playerNum);
		Destroy ( collider.gameObject );
	}
}
