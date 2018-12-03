using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


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

		[Tooltip("Cloud Anchor manager")]
		public GoogleARCore.Examples.CloudAnchors.CloudAnchorController cloudAnchor;

		#endregion


		#region Public Methods


		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
		}

		void Start() {
			Instance = this;
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

		public void PrefabInstan(){
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
					PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0.1f, 0f), Quaternion.identity, 0);
					//PhotonNetwork.Instantiate (this.playerPrefab.name, GameObject.Find("Player1Spawn").GetComponent<Transform>().position, Quaternion.identity, 0);
				}
				else
				{
					Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
				}
			}
		}

		 
		IEnumerator waitingStart(){
            Debug.Log("In coroutine");
            if ( PhotonNetwork.IsMasterClient ) {
				cloudAnchor.OnEnterHostingModeClick ();
				while ( cloudAnchor.GetLastPlacedAnchor () == false ) {
                    Debug.Log("Waiting");
					yield return null;
				}
			} else {
                cloudAnchor.OnEnterHostingModeClick();
                while ( cloudAnchor.GetLastResolvedAnchor () == false ) {
                    Debug.Log("resolving");
                    yield return null;
                }
            }
			
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
					PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
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
