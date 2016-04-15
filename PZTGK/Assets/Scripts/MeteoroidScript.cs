using UnityEngine;
using System.Collections;

public class MeteoroidScript : MonoBehaviour 
{
    public Vector3 moveSpeed;
    public float damage;

	void Start () 
    {

	}

	void Update () 
    {
        transform.position = new Vector3(transform.position.x + moveSpeed.x*Time.deltaTime,
                                         transform.position.y + moveSpeed.y*Time.deltaTime,
                                         transform.position.z + moveSpeed.z*Time.deltaTime);
	}

    void OnTriggerEnter(Collider col)
    {
        PlayerScript playerScript = col.gameObject.GetComponent<PlayerScript>();
        ShieldScript shieldScript = col.gameObject.GetComponent<ShieldScript>();

        if (playerScript != null)
            playerScript.currenHealth -= damage;
        else if (shieldScript != null)
            shieldScript.currentHealth -= damage;
    }

}
