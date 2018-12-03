using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Photon.Pun;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviourPun
    {
        private CarController m_Car; // the car controller we want to use

		[Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
		public static GameObject LocalPlayerInstance;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();

			// #Important
			// used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
			if (photonView.IsMine)
			{
				CarUserControl.LocalPlayerInstance = this.gameObject;
			}
			// #Critical
			// we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
			DontDestroyOnLoad(this.gameObject);
        }


        private void FixedUpdate()
        {
			//if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
			//{
			//	return;
			//}

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			// pass the input to the car!
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");

			if ( Physics.Raycast ( ray, out hit ) ) {
				Transform objectHit = hit.transform;
				//Vector3 distance;
				if ( objectHit.name == "Plane" ) {
					//distance = 
					var heading = hit.point - this.transform.position;
					var distance = heading.magnitude;
					var direction = heading / distance; // This is now the normalized direction.
					v = 1;
					h = direction.x + direction.z;
				}
			}

			//DEBUG
			if (Input.touchCount > 0){
				v = 1;
			}

#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
