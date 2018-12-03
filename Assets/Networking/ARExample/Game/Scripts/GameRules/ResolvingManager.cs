using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using GoogleARCore;

public class ResolvingManager : MonoBehaviour {

    public GoogleARCore.Examples.CloudAnchors.CloudAnchorController cloudController;

    private void Update()
    {
        if (cloudController.GetLastPlacedAnchor() == false && cloudController.GetLastResolvedAnchor() == false) {
            if (PhotonNetwork.IsMasterClient)
            {
                //try to host
                cloudController.OnEnterHostingModeClick();
            } else
            {
                //wait for master to host, then resolve
                cloudController.OnEnterResolvingModeClick();
            }
        }
    }
}
