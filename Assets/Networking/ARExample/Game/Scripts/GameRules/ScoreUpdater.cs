using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class ScoreUpdater : MonoBehaviour
{

    public Text scoreText;

    [SerializeField]
    private int playerNum = 1;
    //private int[] scores = new int[4];

    //private UnityAction<UnityEvents.I> someListener = AddScore;

    void Awake()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= playerNum)
        {
            EventManager.StartListening("SCORE_PLAYER_" + playerNum, AddScore);
            scoreText = GetComponent<Text>();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        EventManager.StopListening("SCORE_PLAYER_" + playerNum, AddScore);
    }

    //Change this to event based updating!
    void AddScore()
    {
        Photon.Realtime.Player player = null;
        PhotonNetwork.CurrentRoom.Players.TryGetValue(playerNum, out player);
        //scores[playerNum] += 1;
        player.AddScore(1);
        scoreText.text = "P" + playerNum + ": " + player.GetScore().ToString("000");
        //playerScores [ playerNum ].text = "Score: " + scores [ playerNum ].ToString("000");;
    }
}
