using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerList : MonoBehaviour {

    public GameObject playerLabel;

    private void Update()
    {
        //update a certain amount of frames
        if (Time.frameCount % 3 == 0)
        {
            if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && (PhotonNetwork.CurrentRoom.Players.Count > this.transform.childCount))
            {
                GameObject newLabel = Instantiate(playerLabel, this.transform);

                //Photon.Realtime.Player player = null;
                //if (PhotonNetwork.PlayerList[], out player))
                //{
                    newLabel.GetComponentInChildren<UnityEngine.UI.Text>().text = PhotonNetwork.PlayerList[PhotonNetwork.CurrentRoom.Players.Count-1].NickName;
                //} else
                //{
                //   
                //Debug.LogError("No such player in current room");
                //}
            }
        }
        //check the list of players in the room once connected
        //add player label for every player in the list.

    }
}
