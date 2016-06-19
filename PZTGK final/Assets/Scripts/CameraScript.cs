using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
    private Vector3 distance;
    private PlayerScript playerScript;

	void Start () 
    {
        playerScript = Object.FindObjectOfType<PlayerScript>();
        distance = transform.position - Object.FindObjectOfType<PlayerScript>().transform.position;
	}
	
	void Update () 
    {
        transform.position = new Vector3(playerScript.screenPosition.x + distance.x, playerScript.screenPosition.y + distance.y, playerScript.screenPosition.z + distance.z);
	}
}
