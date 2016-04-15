using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour 
{
    public float range;

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
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                EnemyScript enemyScript = null;
                enemyScript = obj.GetComponent<EnemyScript>();

                if( (enemyScript != null) &&  (Vector3.Distance(transform.position, obj.transform.position) < range) )
                {
                    playerScript.addScore(enemyScript.points);
                    Destroy(obj);
                }
            }

            Destroy(this.gameObject);
        }
    }
}
