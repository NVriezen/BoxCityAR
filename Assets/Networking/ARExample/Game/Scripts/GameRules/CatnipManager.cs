using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hku.hydra.boxcity
{

    public class CatnipManager : MonoBehaviour
    {

        public float secondsBetween;
        public GameObject catnipPrefab; //catnip prefab

        [Tooltip("The radius in which the next catnip may not spawn in relation to the previous spawned catnip")]
        public float catRadius;
        public GameObject playingField;
        public Grid2D grid;
        public float fieldSize;

        private void Start()
        {
            grid = GameObject.Find("A*").GetComponent<Grid2D>();
            playingField = GameObject.Find("PlayingField");
            EventManager.StartListening("SPAWN_CATNIP", Spawner);
            //StartCoroutine(SpawnCatnip());
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
            Node nod = null;
            do {
                Vector3 spawnPos = playingField.GetComponent<Transform>().position + new Vector3(Random.Range(-fieldSize, fieldSize), 1, Random.Range(-fieldSize, fieldSize));
                //check if position is inside radius of previous object
                //place object on Node
                nod = grid.NodeFromWorldPoint(spawnPos);
            } while (!nod.walkable);
            Instantiate(catnipPrefab, nod.worldPosition, this.transform.rotation);
        }
    }
}


