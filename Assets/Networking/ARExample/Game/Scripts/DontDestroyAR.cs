﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyAR : MonoBehaviour {

    public GameObject[] disableObjects;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "RoomFor2")
        {
            foreach (GameObject game in disableObjects)
            {
                game.SetActive(false);
            }
        }
    }
}
