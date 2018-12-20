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
    private bool inputActive = true;

    private void Awake()
    {
        EventManager.StartListening("GAME_OVER", DisableInput);
        if (!photonView.IsMine)
        {
            EventManager.StopListening("GAME_OVER", DisableInput);
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        EventManager.StopListening("GAME_OVER", DisableInput);
    }

    void Start(){
		rigid = GetComponent<Rigidbody> ();
        cam = Camera.main;
	}

	void Update(){
        if (inputActive) {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0)) {
                Vector3 targetPos = Input.mousePosition;
#else
		if ( Input.touchCount > 0 /*&& (Input.GetTouch ( 0 ).phase == TouchPhase.Began )*/ ) {
			Vector3 targetPos = Input.GetTouch ( 0 ).position;//cam.ScreenToWorldPoint ( Input.GetTouch ( 0 ).position );
#endif
                var direction = new Vector3(0, 0, 0);
                var heading = targetPos - cam.WorldToScreenPoint(this.transform.position);
                var distance = heading.magnitude;
                direction = heading / distance; // This is now the normalized direction.
                direction = new Vector3(direction.x, 0, direction.y);
                rigid.AddForce(this.transform.position + (direction * force));
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, turnSpeed * Time.deltaTime, 0.0f));
            }
        }
	}

    void DisableInput()
    {
        inputActive = false;
    }
}
