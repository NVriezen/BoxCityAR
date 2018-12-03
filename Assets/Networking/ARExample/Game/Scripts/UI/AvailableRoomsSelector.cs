using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AvailableRoomsSelector : MonoBehaviourPunCallbacks
{

    public GameObject roomButton;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //Debug.LogError("Room List Updated");
        //update a certain amount of frames
        //if (Time.frameCount % 4 == 0)
        //{
        //Instantiate(roomButton, this.transform).GetComponentInChildren<UnityEngine.UI.Text>().text = roomList.Count.ToString();
        //if (PhotonNetwork.IsConnected && (roomList.Count > this.transform.childCount))
        // {
        if (roomList.Count > 0)
        {
            GameObject newButton = Instantiate(roomButton, this.transform);

            //Photon.Realtime.Player player = null;
            //if (PhotonNetwork.PlayerList[], out player))
            //{
            newButton.GetComponentInChildren<UnityEngine.UI.Text>().text = roomList[roomList.Count-1].Name;
            newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SelectRoom());
        }
                //} else
                //{
                //   Debug.LogError("No such player in current room");
                //}
           // }
        //}
        //check the list of players in the room once connected
        //add player label for every player in the list.

    }

    public void SelectRoom()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        button.gameObject.GetComponent<Image>().color = new Color(0,0,1);
        hku.hydra.boxcity.JoinLauncher.SetRoom(button.GetComponentInChildren<UnityEngine.UI.Text>().text);
    }
}
