﻿using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player;

	private Vector3 distance;
	private PlayerScript playerScript;

	void Start ()
	{
		playerScript = player.GetComponent<PlayerScript>();
		distance = transform.position - player.transform.position;
	}

	void Update ()
	{
		transform.position = new Vector3(playerScript.screenPosition.x + distance.x, playerScript.screenPosition.y + distance.y, playerScript.screenPosition.z + distance.z);
	}
}
