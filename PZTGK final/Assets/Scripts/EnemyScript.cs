using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
    public float points;

    public float maxHealth;
    public float currentHealth;

    public GameObject[] ammoType;

    public GameObject[] lootType;
    public float[] lootPossibility;

    public Vector3 speed;

    public float shootingPossibility;

    private float shootingCount;

    private bool isShuttingDown = false;

    private float spawnCooldown;

    void Start()
    {
        currentHealth = maxHealth;

        if (lootType.Length != lootPossibility.Length)
            lootPossibility = new float[lootPossibility.Length];

    }
	
	void Update () 
    {
        transform.position = new Vector3(transform.position.x + speed.x*Time.deltaTime,
                                 transform.position.y + speed.y * Time.deltaTime,
                                 transform.position.z + speed.z * Time.deltaTime);

        if (currentHealth <= 0.0f)
            Destroy(this.gameObject);

        spawnCooldown += Time.deltaTime;
        if (spawnCooldown > 1.0f)
        {
            spawnCooldown -= 1.0f;
            if (Random.Range(0.0f, 1.0f) < shootingPossibility)
            {
                shoot(0);
            }
                
        }
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
        GameObject bullet = ammoType[number];
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
        GameObject bulRef = Instantiate(bullet);
        BulletScript bs = bulRef.GetComponent<BulletScript>();

        bulRef.transform.Rotate(0, 180, 0);

        if (bs != null)
            bs.speed =  -bs.speed + speed;
        else
        {
            BulletScript[] bss;
            bss = bulRef.GetComponentsInChildren<BulletScript>();

            foreach (BulletScript buletScript in bss)
            {
                buletScript.speed = -buletScript.speed + speed;
            }
        }
        
        print("bbbbb");
    }

    
}
