using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class ARLauncher : MonoBehaviourPunCallbacks
{

    #region Private Serializable Fields

    [Tooltip("If debugging, set this to true. Then the non-AR room will be loaded.")]
    [SerializeField]
    private bool debug = false;

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    //[Tooltip("The Ui Panel to let the user enter name, connect and play")]
    //[SerializeField]
    //private GameObject controlPanel;

    //[Tooltip("The UI Label to inform the user that the connection is in progress")]
    //[SerializeField]
    //private GameObject progressLabel;

    [Tooltip("The UI Button to start the game")]
    [SerializeField]
    private GameObject startButton;

    [Tooltip("The UI Button to start the game")]
    [SerializeField]
    private GameObject waitingLabel;

    [Tooltip("The UI elements spicifcally for the master client")]
    [SerializeField]
    private GameObject masterSub;

    [Tooltip("The UI elements specifically for the players not creating the room(s)")]
    [SerializeField]
    private GameObject clientSub;

    [Tooltip("The UI elements specifically for the players not creating the room(s)")]
    [SerializeField]
    private GameObject waitSub;

    [Tooltip("The UI elements specifically for the players not creating the room(s)")]
    [SerializeField]
    private GameObject augmentedSub;

    [Tooltip("The UI elements specifically for the players not creating the room(s)")]
    [SerializeField]
    private GoogleARCore.Examples.CloudAnchors.CloudAnchorController cloud;

    #endregion

    #region MonoBehaviour CallBacks

    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        //PhotonNetwork.AutomaticallySyncScene = true;

        EventManager.StartListening("ANCHOR_PLACED", OnAnchorPlaced);
        EventManager.StartListening("ANCHOR_RESOLVE_FAILED", ResolveAgain);
        EventManager.StartListening("ANCHOR_HOST_FAILED", HostAgain);
    }

    void Start()
    {
        //progressLabel.SetActive(false);
        //controlPanel.SetActive(false);
        //startButton.SetActive(false);
        //waitingLabel.SetActive(true);

        //if (!PhotonNetwork.IsConnected)
        //{
        //    waitingLabel.SetActive(false);
        //    controlPanel.SetActive(true);
        //    PhotonNetwork.GameVersion = gameVersion;
        //    PhotonNetwork.ConnectUsingSettings();
        //    //PhotonNetwork.JoinLobby();
        //}
        waitSub.SetActive(false);
        augmentedSub.SetActive(true);

        if (PhotonNetwork.IsMasterClient)
        {
            cloud.OnEnterHostingModeClick();
            Debug.Log("Hosting");

            int roomnumber = Random.Range(1, 9999);
            string ipnumber = cloud.UIController.GetDeviceIpAddress();

            PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId).SetRoom(roomnumber); //Random.Range(1, 9999));
            PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId).SetIP(ipnumber); //cloud.UIController.GetDeviceIpAddress());

            //cloud.OnResolveRoomClick(roomnumber, ipnumber, false/*int roomNum, string ipaddress, bool ondevice */);
            Debug.Log("Resolving");
            //masterSub.SetActive(true);
            //clientSub.SetActive(false);
        }
        else
        {
            //Get ip adress and room from master
            int roomnumber = PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId).GetRoom();
            string ipnumber = PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId).GetIP();
            //input in cloudcontroller
            cloud.OnEnterResolvingModeClick();
            cloud.OnResolveRoomClick(roomnumber, ipnumber, false/*int roomNum, string ipaddress, bool ondevice */);
            Debug.Log("Resolving to Join");
            //masterSub.SetActive(false);
            //clientSub.SetActive(true);
        }
    }

    void ResolveAgain()
    {
        //Get ip adress and room from master
        int roomnumber = PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId).GetRoom();
        string ipnumber = PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId).GetIP();
        //input in cloudcontroller
        //cloud.OnEnterResolvingModeClick();
        //cloud.OnResolveRoomClick(roomnumber, ipnumber, false/*int roomNum, string ipaddress, bool ondevice */);
    }

    void HostAgain()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            cloud.OnEnterHostingModeClick();

            int roomnumber = Random.Range(1, 9999);
            string ipnumber = cloud.UIController.GetDeviceIpAddress();

            PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId).SetRoom(roomnumber); //Random.Range(1, 9999));
            PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId).SetIP(ipnumber); //cloud.UIController.GetDeviceIpAddress());

            //cloud.OnResolveRoomClick(roomnumber, ipnumber, false/*int roomNum, string ipaddress, bool ondevice */);
        }
    }

    #endregion


    /// <summary>
    /// Called after disconnecting from the Photon server. It could be a failure or an explicit disconnect call
    /// </summary>
    /// <remarks>The reason for this disconnect is provided as DisconnectCause.</remarks>
    /// <param name="cause">Cause.</param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        //progressLabel.SetActive(false);
        //controlPanel.SetActive(true);
        Debug.LogWarningFormat("PUN Catnip Catastrophe: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    void OnAnchorPlaced()
    {
        augmentedSub.SetActive(false);
        waitSub.SetActive(true);
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
            waitingLabel.SetActive(false);
        } else
        {
            startButton.SetActive(false);
            waitingLabel.SetActive(true);
        }
        PhotonNetwork.CurrentRoom.IsVisible = true;
    }

    //public override void OnJoinRandomFailed(short returncode, string message){
    //	Debug.Log ("");
    //	PhotonNetwork.CreateRoom ( null, new RoomOptions {MaxPlayers = maxPlayersPerRoom} );
    //}

    public void StartGame()
    {
        // #Critical
        // Load the Room Level.
        //     if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        if (!debug)
        {
            //PhotonNetwork.LoadLevel("Roomfor" + PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Roomfor2");
        }
        else
        {
            PhotonNetwork.LoadLevel("Roomfor2NONAR");
        }

    }
}

