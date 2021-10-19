using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject followTarget;

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	private float cameraZ = 0;

	private Camera mainCamera;

	void Start () {
		cameraZ = transform.position.z;
		mainCamera = GetComponent<Camera>();
	}
	
	void FixedUpdate () {
		if (followTarget) {
			Vector3 delta = followTarget.transform.position - mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cameraZ));
			Vector3 destination = transform.position + delta;
			destination.z = cameraZ;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}
