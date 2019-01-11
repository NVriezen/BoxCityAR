﻿using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;

using System.Linq;


namespace hku.hydra.boxcity
{
	public class DebugManager : MonoBehaviourPunCallbacks
	{


		#region Photon Callbacks


		/// <summary>
		/// Called when the local player left the room. We need to load the launcher scene.
		/// </summary>
		public override void OnLeftRoom()
		{
			SceneManager.LoadScene("TitleScreen");
		}


		#endregion

		#region Public Fields

		public static DebugManager Instance;

		[Tooltip("The prefab to use for representing the player")]
		public GameObject playerPrefab;

		//[Tooltip("Cloud Anchor manager")]
		//public GoogleARCore.Examples.CloudAnchors.CloudAnchorController cloudAnchor;

        [Tooltip("Text")]
        public UnityEngine.UI.Text text;

        public static bool gameOver = false;

        [Tooltip("Game Over Canvas")]
        public GameObject gameOverCanvas;

        #endregion


        #region Public Methods

        private void Awake()
        {
            gameOverCanvas.SetActive(false);
            EventManager.StartListening("GAME_OVER", OnGameOver);
        }

        void OnGameOver()
        {
            gameOverCanvas.SetActive(true);
            gameOver = true;
        }

        override public void OnDisable()
        {
            base.OnDisable();
            EventManager.StopListening("GAME_OVER", OnGameOver);
        }


        public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
		}

		void Start() {
			Instance = this;
            text.text = "start";
            StartCoroutine (waitingStart());
            //PrefabInstan();
			
			//if (playerPrefab == null)
			//{
			//	Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
			//}
			//else
			//{
			//	if (UnityStandardAssets.Vehicles.Car.CarUserControl.LocalPlayerInstance == null)
			//	{
			//		Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
			//		// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
			//		PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
			//	}
			//	else
			//	{
			//		Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
			//	}
			//}

            //if (PhotonNetwork.IsMasterClient)
            //{
            //    cloudAnchor.OnEnterHostingModeClick();
            //} else
            //{
            //    cloudAnchor.OnEnterResolvingModeClick();
            //}
		}

		//public void PrefabInstan(){
		//	if (playerPrefab == null)
		//	{
		//		Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
		//	}
		//	else
		//	{
		//		if (UnityStandardAssets.Vehicles.Car.CarUserControl.LocalPlayerInstance == null)
		//		{
		//			Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
		//			// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
		//			PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0.1f, 0f), Quaternion.identity, 0);
		//			//PhotonNetwork.Instantiate (this.playerPrefab.name, GameObject.Find("Player1Spawn").GetComponent<Transform>().position, Quaternion.identity, 0);
		//		}
		//		else
		//		{
		//			Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
		//		}
		//	}
		//}

        public void RestartGame()
        {
            PhotonNetwork.LoadLevel("RoomFor2");
        }

		 
		IEnumerator waitingStart(){
            //         Debug.Log("In coroutine");
            //         if ( PhotonNetwork.IsMasterClient ) {
            //	//cloudAnchor.OnEnterHostingModeClick ();
            //	while ( cloudAnchor.GetLastPlacedAnchor () == false ) {
            //                 Debug.Log("Waiting");
            //		yield return null;
            //	}
            //} else {
            //             //cloudAnchor.OnEnterHostingModeClick();
            //             while ( cloudAnchor.GetLastResolvedAnchor () == false ) {
            //                 Debug.Log("resolving");
            //                 yield return null;
            //             }
            //         }
            text.text = "waiting for field";
            while (!GameObject.FindWithTag("Finish"))
            {
                yield return null;
            }
            Debug.Log("start Spawning");
            text.text = "spawning";
            if (playerPrefab == null)
			{
				Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
			}
			else
			{
				if (UnityStandardAssets.Vehicles.Car.CarUserControl.LocalPlayerInstance == null)
				{
					Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
 
                    Vector3 spawnPos = GameObject.Find("SpawnPointP" + PhotonNetwork.LocalPlayer.ActorNumber).transform.position;

                    GameObject tempPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPos, Quaternion.identity, 0);
                    //GameObject tempPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                    tempPlayer.GetComponent<EventSender>().playerNum = PhotonNetwork.LocalPlayer.ActorNumber;
                    tempPlayer.transform.SetParent(GameObject.FindObjectOfType<GoogleARCore.CrossPlatform.XPAnchor>().transform);
                    text.text = "carrr";
                }
				else
				{
					Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
				}
			}
            Debug.Log("out coroutine");
        }
		


		#endregion

		#region Private Methods


		void LoadArena()
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
			}
			Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
			PhotonNetwork.LoadLevel("Roomfor" + PhotonNetwork.CurrentRoom.PlayerCount);
		}


		#endregion

		#region Photon Callbacks


		//public override void OnPlayerEnteredRoom(Player other)
		//{
		//	Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
		//	string userId = other.NickName;
		//	PhotonNetwork.AuthValues = new AuthenticationValues(userId);


		//	if (PhotonNetwork.IsMasterClient)
		//	{
		//		Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


		//		LoadArena();
		//	}
		//}


		//public override void OnPlayerLeftRoom(Player other)
		//{
		//	Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


		//	if (PhotonNetwork.IsMasterClient)
		//	{
		//		Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


		//		LoadArena();
		//	}
		//}


		#endregion
	}
}
