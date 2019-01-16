using System;
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

        [Tooltip("Text")]
        public UnityEngine.UI.Text text;

        public static bool gameOver = false;

        [Tooltip("Game Over Canvas")]
        public GameObject gameOverCanvas;

        private GameObject playerActiveObject;

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
		}

        public void RestartGame()
        {
            PhotonNetwork.Destroy(playerActiveObject);
            PhotonNetwork.LoadLevel("RoomFor2");
        }

		 
		IEnumerator waitingStart(){
            text.text = "waiting for field";
            while (!GameObject.FindWithTag("Finish"))
            {
                yield return null;
            }
            //Debug.Log("start Spawning");
            text.text = "spawning";
            if (playerPrefab == null)
			{
				Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
			}
			else
			{
				//if (playerActiveObject == null)
				//{
					//Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
 
                    Vector3 spawnPos = GameObject.Find("SpawnPointP" + PhotonNetwork.LocalPlayer.ActorNumber).transform.position;

                    //playerActiveObject = PhotonNetwork.Instantiate("CatP" + PhotonNetwork.LocalPlayer.ActorNumber, spawnPos, Quaternion.identity, 0);
                    playerActiveObject = PhotonNetwork.Instantiate("CatP" + PhotonNetwork.LocalPlayer.ActorNumber, spawnPos, Quaternion.identity, 0);
                    //GameObject tempPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                    playerActiveObject.GetComponent<EventSender>().playerNum = PhotonNetwork.LocalPlayer.ActorNumber;
                    playerActiveObject.transform.SetParent(GameObject.FindWithTag("Finish").transform);//GameObject.FindObjectOfType<GoogleARCore.CrossPlatform.XPAnchor>().transform);
                    //text.text = "carrr";
                //}
				//else
				//{
				//	Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
				//}
			}
        }

		#endregion

		#region Private Methods

		void LoadArena()
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
			}
			//Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
			PhotonNetwork.LoadLevel("Roomfor" + PhotonNetwork.CurrentRoom.PlayerCount);
		}

		#endregion
	}
}
