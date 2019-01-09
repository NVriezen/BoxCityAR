using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAR : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    { 
        //Photon.Pun.PhotonView viewer = this.gameObject.AddComponent<Photon.Pun.PhotonView>();
        
        //Photon.Pun.PhotonTransformView transformView = this.gameObject.AddComponent<Photon.Pun.PhotonTransformView>();
        //viewer.ObservedComponents.Add(transformView);
        //viewer.ObservedComponents.Add(transformView);
        DontDestroyOnLoad(this.gameObject);
    }
}
