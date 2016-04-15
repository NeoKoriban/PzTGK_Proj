using UnityEngine;
using System.Collections;

public class BlackHoleScript : MonoBehaviour 
{
    public float force;
    public float attractionDistance;
    public float killDistance;

    private GameObject player;

	void Start () 
    {
        player = null;
	}
	
	void Update () 
    {
		if(player != null)
        {
            float xDifference = transform.position.x - player.transform.position.x;
            float zDifference = transform.position.z - player.transform.position.z;

            float distance = Mathf.Sqrt(Mathf.Pow((xDifference), 2) + Mathf.Pow((zDifference), 2));

            if (distance < attractionDistance)
            {
                player.transform.position = new Vector3(player.transform.position.x + xDifference*force*Time.deltaTime,
                                                        player.transform.position.y,
                                                        player.transform.position.z + zDifference*force*Time.deltaTime);
            }

            if(distance < killDistance)
            {
                PlayerScript ps = player.GetComponent<PlayerScript>();
                ps.currenHealth = -1000;
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        PlayerScript playerScript = col.gameObject.GetComponent<PlayerScript>();

        if (playerScript != null)
        {
            player = col.gameObject;
        }
    }
}
