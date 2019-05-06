﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	void Start()
	{
		float[] distances = new float[32];
		distances[8] = 30;
		GetComponent<Camera>().layerCullDistances = distances;
	}

	// Update is called once per frame
	void Update()
	{
		float tiltChange = 0f;
		if (Input.GetKey(KeyCode.Alpha1))
			tiltChange = 1f;
		if (Input.GetKey(KeyCode.Alpha2))
			tiltChange = -1f;

		transform.eulerAngles += new Vector3(tiltChange, 0, 0);

		if (Input.GetKey(KeyCode.Q))
			transform.eulerAngles += new Vector3(0, -1f, 0);
		if (Input.GetKey(KeyCode.E))
			transform.eulerAngles += new Vector3(0, 1f, 0);

		Vector3 camFlatForward = transform.forward;
		camFlatForward.y = 0;

		if (Input.GetKey(KeyCode.W))
			transform.position += camFlatForward * 0.2f;
		if (Input.GetKey(KeyCode.A))
			transform.position += transform.right * -0.2f;
		if (Input.GetKey(KeyCode.S))
			transform.position += camFlatForward * -0.2f;
		if (Input.GetKey(KeyCode.D))
			transform.position += transform.right * 0.2f;

		float zoom = Input.GetAxis("Mouse ScrollWheel") * 15f;
		float newZoom = Mathf.Max(1f, Mathf.Min(30f, transform.position.y - zoom));

		transform.position = new Vector3(transform.position.x, newZoom, transform.position.z);
	}
}