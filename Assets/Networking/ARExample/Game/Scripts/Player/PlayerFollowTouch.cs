using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFollowTouch : MonoBehaviourPunCallbacks
{

	public Camera cam;
	public float force;
	public float turnSpeed;
	private Rigidbody rigid;

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            this.enabled = false;
        }
    }

    void Start(){
		rigid = GetComponent<Rigidbody> ();
        cam = Camera.main;
	}

	void Update(){
		#if UNITY_EDITOR
		if ( Input.GetMouseButton(0) ) {
			Vector3 targetPos = Input.mousePosition;
		#else
		if ( Input.touchCount > 0 /*&& (Input.GetTouch ( 0 ).phase == TouchPhase.Began )*/ ) {
			Vector3 targetPos = Input.GetTouch ( 0 ).position;//cam.ScreenToWorldPoint ( Input.GetTouch ( 0 ).position );
		#endif
			var direction = new Vector3 (0,0,0);
			var heading = targetPos - cam.WorldToScreenPoint(this.transform.position);
			var distance = heading.magnitude;
			direction = heading / distance; // This is now the normalized direction.
			direction = new Vector3(direction.x, 0, direction.y);
			rigid.AddForce(this.transform.position + (direction * force));
			transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, turnSpeed * Time.deltaTime, 0.0f));
		}
	}
}
