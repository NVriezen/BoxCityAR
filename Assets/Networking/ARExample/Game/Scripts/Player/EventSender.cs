using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EventSender : MonoBehaviourPunCallbacks {

	public int playerNum = 1;
    private bool holdingCatnip;

    void OnTriggerEnter(Collider collider){
        if (collider.tag == "Catnip" && !holdingCatnip) {
            holdingCatnip = true;
            PhotonNetwork.Destroy(collider.gameObject);
        } else if (collider.tag == "ScoreShop" && holdingCatnip)
        {
            holdingCatnip = false;
            Debug.Log("Score +1");
            EventManager.TriggerEvent("SCORE_PLAYER_" + playerNum);
            EventManager.TriggerEvent("SPAWN_CATNIP");
        } else if (collider.tag == "Player")
        {
            //if () //check if doing a dash
            {
                //Only if not holding catnip change something on this player and collided player
                if (!holdingCatnip)
                {
                    if (collider.gameObject.GetComponent<EventSender>().holdingCatnip)
                    {
                        collider.gameObject.GetComponent<EventSender>().holdingCatnip = false;
                        holdingCatnip = true;
                    }
                }
            }
        }
	}
}
