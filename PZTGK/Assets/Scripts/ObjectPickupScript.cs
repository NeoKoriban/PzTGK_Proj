using UnityEngine;
using System.Collections;

public class ObjectPickupScript : MonoBehaviour 
{
    public GameObject content;
    public int amount;

    public float points;

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider col)
    {
        PlayerScript playerScript = col.gameObject.GetComponent<PlayerScript>();

        if(playerScript != null)
        {
            if(content != null)
            {
                BulletScript bulletScript = content.GetComponent<BulletScript>();
                if (bulletScript != null)
                    playerScript.addAmmo(content, amount);

                ShieldScript shieldScript = content.GetComponent<ShieldScript>();
                if (shieldScript != null)
                    playerScript.addShield(content);
            }

            playerScript.addScore(points);
            Destroy(this.gameObject);
        }
    }
}
