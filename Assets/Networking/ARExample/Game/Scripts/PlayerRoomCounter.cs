using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerRoomCounter : MonoBehaviour {

	public Text playertext;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            playertext.text = "Connected: " + PhotonNetwork.CurrentRoom.PlayerCount;//.room.playerCount;
        }
    }
}
