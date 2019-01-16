using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace hku.hydra.boxcity
{

    public class PickupSpawner : MonoBehaviour
    {

        public float secondsBetween;
        public GameObject pickupPrefab;
        public LayerMask unwalkableMask;

        [Tooltip("The radius in which the next catnip may not spawn in relation to the previous spawned catnip")]
        public float catRadius;
        public GameObject playingField;
        public float fieldSize;

        public int currentPickups = 0;
        public int maxPickups;

        private GameObject pickupActiveObject;

        private void Start()
        {
            playingField = GameObject.FindWithTag("Finish");
            StartCoroutine(SpawnPickup());
        }

        private void OnDisable()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(pickupActiveObject);
            }
        }

        IEnumerator SpawnPickup()
        {
            while (!DebugManager.gameOver && currentPickups < maxPickups)
            {
                //wait for seconds
                yield return new WaitForSeconds(secondsBetween);
                //spawn
                //Debug.Log("Starting Field Check");
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
                spawnPos = playingField.GetComponent<Transform>().position + new Vector3(Random.Range(-fieldSize, fieldSize), 0f, Random.Range(-fieldSize, fieldSize));
                walkable = !Physics.CheckSphere(spawnPos, catRadius, unwalkableMask);
                //GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //ball.transform.position = spawnPos;
                //ball.transform.localScale = new Vector3(catRadius - 0.1f, catRadius - 0.1f, catRadius - 0.1f);
                //Debug.Log("checking walkable field");
            } while (walkable == false);
            //Debug.Log("Walkable Field Found!");
            //spawnPos = new Vector3(spawnPos.x, 0.01f, spawnPos.z);
            spawnPos = new Vector3(spawnPos.x, playingField.GetComponent<Transform>().position.y + 0.012f, spawnPos.z);
            //Transform anchorParent = GameObject.FindObjectOfType<GoogleARCore.CrossPlatform.XPAnchor>().transform;
            pickupActiveObject = PhotonNetwork.Instantiate("Pickup", spawnPos, this.transform.rotation);
            //pickupActiveObject.transform.SetParent(anchorParent);
            pickupActiveObject.transform.SetParent(playingField.transform);
            currentPickups += 1;
        }
    }
}
