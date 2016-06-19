using UnityEngine;
using System.Collections;

public class BuildingScript : MonoBehaviour 
{
    public float points;

    public float maxHealth;
    public float currentHealth;

    public GameObject[] lootType;
    public float[] lootPossibility;

    private bool isShuttingDown = false;

	void Start () 
    {
        currentHealth = maxHealth;

        if (lootType.Length != lootPossibility.Length)
            lootPossibility = new float[lootPossibility.Length];
	}
	
	void Update () 
    {
        if (currentHealth <= 0)
            Destroy(this.gameObject);
	}

    void OnDestroy()
    {
        if (!isShuttingDown)
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
                loot.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                Instantiate(loot);
                break;
            }
        }
    }
}
