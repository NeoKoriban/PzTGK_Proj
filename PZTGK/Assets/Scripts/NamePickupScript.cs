using UnityEngine;
using System.Collections;

public enum parameterName { health, maxHealth, cooldownReduction, moveSpeed};

public class NamePickupScript : MonoBehaviour 
{
    public parameterName param;
    public float amount;

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

        if (playerScript != null)
        {
            if (param == parameterName.health)
                playerScript.addHealth(amount);
            else if (param == parameterName.maxHealth)
                playerScript.maxHealth += amount;
            else if (param == parameterName.cooldownReduction)
                playerScript.addCooldownReduction(amount);
            else if (param == parameterName.moveSpeed)
                playerScript.addMoveSpeed(amount);


            playerScript.addScore(points);
            Destroy(this.gameObject);
        }
    }
}
