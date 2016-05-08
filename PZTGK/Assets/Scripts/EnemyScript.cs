using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour 
{
    public float points;

    public float maxHealth;
    public float currentHealth;

    public GameObject[] ammoType;

    public GameObject[] lootType;
    public float[] lootPossibility;
	public List<BaseAI> AIQueue;

    private bool isShuttingDown = false;

	void Start () 
    {
        currentHealth = maxHealth;

        if (lootType.Length != lootPossibility.Length)
            lootPossibility = new float[lootPossibility.Length];
	}
	
	void Update () 
    {
        if (currentHealth <= 0.0f)
            Destroy(this.gameObject);
	}

    void OnDestroy()
    {
        if(!isShuttingDown)
            dropLoot();
    }

    void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    private void dropLoot()
    {
        for (int i = 0; i < lootType.Length; ++i)
        {
            if (Random.Range(0.0f, 1.0f) <= lootPossibility[i])
            {
                GameObject loot = lootType[i];
                loot.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Instantiate(loot);
                break;
            }
        }
    }

    private void shoot(int number)
    {
        if (ammoType.Length < number)
            return;

        GameObject bullet = ammoType[number];
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(bullet);
    }

    
}
