using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Photon.Pun.UtilityScripts
{
    /// <summary>
    /// Scoring system for PhotonPlayer
    /// </summary>
    public class PunARInfo : MonoBehaviour
    {
        public const string MasterRoomProp = "room";
        public const string MasterIPProp = "ipaddress";
    }

    public static class RoomExtensions
    {
        public static void SetRoom(this Player player, int roomNum)
        {
            Hashtable room = new Hashtable();  // using PUN's implementation of Hashtable
            room[PunARInfo.MasterRoomProp] = roomNum;

            player.SetCustomProperties(room);  // this locally sets the score and will sync it in-game asap.
        }

        public static void SetIP(this Player player, string ip)
        {
            Hashtable ipAddress = new Hashtable();  // using PUN's implementation of Hashtable
            ipAddress[PunARInfo.MasterIPProp] = ip;

            player.SetCustomProperties(ipAddress);  // this locally sets the score and will sync it in-game asap.
        }

        //public static void AddScore(this Player player, int scoreToAddToCurrent)
        //{
        //    int current = player.GetScore();
        //    current = current + scoreToAddToCurrent;

        //    Hashtable score = new Hashtable();  // using PUN's implementation of Hashtable
        //    score[PunPlayerScores.PlayerScoreProp] = current;

        //    player.SetCustomProperties(score);  // this locally sets the score and will sync it in-game asap.
        //}

        public static int GetRoom(this Player player)
        {
            object room;
            if (player.CustomProperties.TryGetValue(PunARInfo.MasterRoomProp, out room))
            {
                return (int)room;
            }

            return 0;
        }

        public static string GetIP(this Player player)
        {
            object ipAddress;
            if (player.CustomProperties.TryGetValue(PunARInfo.MasterRoomProp, out ipAddress))
            {
                return ipAddress.ToString();
            }

            return null;
        }
    }
}
