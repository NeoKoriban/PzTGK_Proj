using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour 
{

    public Vector3 speed;
    public float damage;

    public bool destructionOnImpact;
    public float lifeTime;

    public bool ignorePlayer;
    public bool ignoreEnemies;

    public int defaultSlot;
    public float cooldown;
    public Vector3 creationPosition;
    public int startAmount;

    private float creationTime;

	void Start () 
    {
        creationTime = Time.time;
	}
	
	void Update () 
    {
        transform.position = new Vector3(transform.position.x + speed.x * Time.deltaTime,
                                         transform.position.y + speed.y * Time.deltaTime, 
                                         transform.position.z + speed.z * Time.deltaTime);

        if (Time.time > creationTime + lifeTime)
        {
            Destroy(this.gameObject);

            if(transform.parent != null)
                Destroy(this.transform.parent.gameObject);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        PlayerScript playerScript = col.gameObject.GetComponent<PlayerScript>();
        EnemyScript enemyScript = col.gameObject.GetComponent<EnemyScript>();
        ShieldScript shieldScript = col.gameObject.GetComponent<ShieldScript>();
        BuildingScript buildingScript = col.gameObject.GetComponent<BuildingScript>();

        if( (playerScript != null) && (!ignorePlayer) )
        {
            playerScript.currenHealth -= damage;

            if(destructionOnImpact)
                Destroy(this.gameObject);
        }
        else if ( (enemyScript != null) && (!ignoreEnemies) )
        {
            enemyScript.currentHealth -= damage;

            if (destructionOnImpact)
                Destroy(this.gameObject);
        }
        else if ( (shieldScript != null) && (!ignorePlayer) )
        {
            shieldScript.currentHealth -= damage;

            if (destructionOnImpact)
                Destroy(this.gameObject);
        }
        else if (buildingScript != null)
        {
            buildingScript.currentHealth -= damage;

            if (destructionOnImpact)
                Destroy(this.gameObject);
        }
    }
}
