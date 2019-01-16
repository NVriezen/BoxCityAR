using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickupObject : MonoBehaviourPunCallbacks {

    public override void OnEnable()
    {
        StartCoroutine(WaitToDestroy());
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(16);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
