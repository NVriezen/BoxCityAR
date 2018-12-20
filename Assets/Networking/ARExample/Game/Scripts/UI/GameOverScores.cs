using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class GameOverScores : MonoBehaviourPunCallbacks
{

    public GameObject scoreObject;

    override public void OnEnable()
    {
        base.OnEnable();

        EventManager.StartListening("GAME_OVER", ShowScores);

        //foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        //{
        //    GameObject obj = Instantiate(scoreObject, this.transform);
        //    Text[] textList = obj.GetComponentsInChildren<Text>();
        //    if (textList[0].gameObject.name == "PlayerName")
        //    {
        //        textList[0].text = p.NickName;
        //        textList[1].text = p.GetScore().ToString();
        //    } else
        //    {
        //        textList[1].text = p.NickName;
        //        textList[0].text = p.GetScore().ToString();
        //    }
        //}
    }

    override public void OnDisable()
    {
        base.OnEnable();

        EventManager.StopListening("GAME_OVER", ShowScores);
    }

    void ShowScores()
    {
        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
        {
            GameObject obj = Instantiate(scoreObject, this.transform);
            Text[] textList = obj.GetComponentsInChildren<Text>();
            if (textList[0].gameObject.name == "PlayerName")
            {
                textList[0].text = p.NickName;
                textList[1].text = p.GetScore().ToString();
            }
            else
            {
                textList[1].text = p.NickName;
                textList[0].text = p.GetScore().ToString();
            }
        }
    }
}
