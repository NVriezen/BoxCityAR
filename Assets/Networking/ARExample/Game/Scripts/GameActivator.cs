using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActivator : MonoBehaviour {

	[Tooltip("Cloud Anchor manager")]
	public GoogleARCore.Examples.CloudAnchors.CloudAnchorController cloudAnchor;

	public GameObject gameManager;

	void Awake(){
		gameManager.SetActive ( false );
	}

	void Update(){
		if ( /*cloudAnchor.GetLastPlacedAnchor() || */ cloudAnchor.GetLastResolvedAnchor()) {
			gameManager.SetActive ( true );
		} else {
			gameManager.SetActive ( false );
		}
	}
}
