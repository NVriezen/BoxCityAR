﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hku.hydra.boxcity
{

    public class CatnipManager : MonoBehaviour
    {

        public float secondsBetween;
        public GameObject catnipPrefab; //catnip prefab
        public LayerMask unwalkableMask;

        [Tooltip("The radius in which the next catnip may not spawn in relation to the previous spawned catnip")]
        public float catRadius;
        public GameObject playingField;
        //public Grid2D grid;
        public float fieldSize;

        private void Start()
        {
            //grid = GameObject.Find("A*").GetComponent<Grid2D>();
            playingField = GameObject.FindWithTag("Finish");
            EventManager.StartListening("SPAWN_CATNIP", Spawner);
            EventManager.TriggerEvent("SPAWN_CATNIP");
            //StartCoroutine(SpawnCatnip()); //Only for debugging
        }

        private void OnDisable()
        {
            EventManager.StopListening("SPAWN_CATNIP", Spawner);
        }

        IEnumerator SpawnCatnip()
        {
            while (!DebugManager.gameOver)
            {
                //wait for seconds
                yield return new WaitForSeconds(secondsBetween);
                //spawn
                Spawner();
                //iets wat het sneller maakt?
            }
        }

        public void Spawner()
        {
            //Node nod = null;
            //do {
            //    Vector3 spawnPos = playingField.GetComponent<Transform>().position + new Vector3(Random.Range(-fieldSize, fieldSize), 1, Random.Range(-fieldSize, fieldSize));
            //    //check if position is inside radius of previous object
            //    //place object on Node
            //    nod = grid.NodeFromWorldPoint(spawnPos);
            //} while (!nod.walkable);
            Vector3 spawnPos;
            bool walkable;
            do
            {
                spawnPos = playingField.GetComponent<Transform>().position + new Vector3(Random.Range(-fieldSize, fieldSize), 1, Random.Range(-fieldSize, fieldSize));
                walkable = !Physics.CheckSphere(spawnPos, catRadius - 0.1f, unwalkableMask); //Check for multiple masks and set multiple bools for walkable, interactable, grass, etc.
            } while (walkable == false);
            spawnPos = new Vector3(spawnPos.x, 0.01f, spawnPos.z);
            Instantiate(catnipPrefab, spawnPos, this.transform.rotation);
        }
    }
}


