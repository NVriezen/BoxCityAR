using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

namespace hku.hydra.boxcity 
{
	public class Launcher : MonoBehaviourPunCallbacks
	{

		#region Private Serializable Fields

		[Tooltip("If debugging, set this to true. Then the non-AR room will be loaded.")]
		[SerializeField]
		private bool debug = false;

		[Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
		[SerializeField]
		private byte maxPlayersPerRoom = 4;

		[Tooltip("The Ui Panel to let the user enter name, connect and play")]
		[SerializeField]
		private GameObject controlPanel;

		[Tooltip("The UI Label to inform the user that the connection is in progress")]
		[SerializeField]
		private GameObject progressLabel;

        [Tooltip("The UI Button to start the game")]
        [SerializeField]
        private GameObject startButton;

        [Tooltip("The UI Button to start the game")]
        [SerializeField]
        private GameObject waitingLabel;

        #endregion

        #region Private Fields

        /// <summary>
        /// This client's version number. Users are seperated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";

		/// <summary>
		/// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon,
		/// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
		/// Typically this is used for the OnConnectedToMaster() callback.
		/// </summary>
		bool isConnecting;

		#endregion

		#region MonoBehaviour CallBacks

		void Awake() {
			// #Critical
			// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
			PhotonNetwork.AutomaticallySyncScene = true;
		}

		void Start() {
			progressLabel.SetActive(false);
			controlPanel.SetActive(false);
            startButton.SetActive(false);
            waitingLabel.SetActive(true);

            if (!PhotonNetwork.IsConnected)
            {
                waitingLabel.SetActive(false);
                controlPanel.SetActive(true);
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
                //PhotonNetwork.JoinLobby();
            }
        }

		#endregion

		#region Public Methods

		/// <summary>
		/// 
		/// </summary>
		public void Connect() {
			progressLabel.SetActive(true);
			controlPanel.SetActive(false);
            startButton.SetActive(false);

            // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            isConnecting = true;

			if ( PhotonNetwork.IsConnected ) {
                PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
            } else {
				PhotonNetwork.GameVersion = gameVersion;
				PhotonNetwork.ConnectUsingSettings ();
			}
		}

        /// <summary>
		/// 
		/// </summary>
		public void CreateNewRoom()
        {

            // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            isConnecting = true;

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.CreateRoom(GameRoomNameInput.GetRoomName(), new RoomOptions { MaxPlayers = maxPlayersPerRoom });
                progressLabel.SetActive(true);
                controlPanel.SetActive(false);
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        /// <summary>
		/// 
		/// </summary>
		public void JoinExisitngRoom(string roomName)
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            isConnecting = true;

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRoom(roomName);
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        /// <summary>
        /// Called when the client is connected to the Master Server and ready for matchmaking and other tasks.
        /// </summary>
        /// <remarks>The list of available rooms won't become available unless you join a lobby via LoadBalancingClient.OpJoinLobby.
        /// You can join rooms and create them even without being in a lobby. The default lobby is used in that case.</remarks>
        public override void OnConnectedToMaster(){
			Debug.Log ("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
            //startButton.SetActive(true);
            //PhotonNetwork.JoinLobby();


            // we don't want to do anything if we are not attempting to join a room.
            // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
            // we don't want to do anything.
            if (isConnecting)
			{
                // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
                PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
            }
		}

        /// <summary>
        /// Called after disconnecting from the Photon server. It could be a failure or an explicit disconnect call
        /// </summary>
        /// <remarks>The reason for this disconnect is provided as DisconnectCause.</remarks>
        /// <param name="cause">Cause.</param>
        public override void OnDisconnected(DisconnectCause cause){
			progressLabel.SetActive(false);
			controlPanel.SetActive(true);
			Debug.LogWarningFormat ("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
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
            //if (!debug)
            //{
            //    //PhotonNetwork.LoadLevel("Roomfor" + PhotonNetwork.CurrentRoom.PlayerCount);
            //    PhotonNetwork.LoadLevel("Roomfor2");
            //} else
            //{
            //    PhotonNetwork.LoadLevel("Roomfor2NONAR");
            //}
            PhotonNetwork.LoadLevel("WaitForPlayers");

        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            //Debug.Log("Joined a lobby, yeahh" + PhotonNetwork.CurrentLobby.Name);
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            //Debug.Log("Created our own room");
        }


        public override void OnJoinedRoom(){
            // #Critical: We only load if we are the first player, else we rely on `PhotonNetwork.AutomaticallySyncScene` to sync our instance scene.
            progressLabel.SetActive(false);
            //Debug.Log("Joined a Room");
            if (/*PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom && */PhotonNetwork.IsMasterClient)
			{
                PhotonNetwork.CurrentRoom.IsVisible = true;
                startButton.SetActive(true);
                //StartCoroutine(OnJoin());

                //Debug.Log("We load the 'Roomfor[maxPlayers]' ");


                //// #Critical
                //// Load the Room Level.
                //if ( !debug ) {
                //	PhotonNetwork.LoadLevel ( "Roomfor" + maxPlayersPerRoom );

                //} else {
                //	PhotonNetwork.LoadLevel ( "NonConnectionRoom" );
                //}
            }
		}

        IEnumerator OnJoin()
        {
            while (PhotonNetwork.CurrentRoom.PlayerCount != maxPlayersPerRoom)
            {
                yield return null;
            }
            //Debug.Log("We load the 'Roomfor[maxPlayers]' ");

            // #Critical
            // Load the Room Level.
            if (!debug)
            {
                PhotonNetwork.LoadLevel("Roomfor" + maxPlayersPerRoom);
                //PhotonNetwork.LoadLevel("Roomfor2");
            }
            else
            {
                PhotonNetwork.LoadLevel("NonConnectionRoom");
            }
        }

		#endregion
	}
}
