using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour 
{
    public Vector3 spawnArea;
    public float spawnFreq;

    public GameObject[] objects;
    public float[] yPosition;
    public float[] possibility;

    private PlayerScript ps;
    private float spawnCooldown;

	void Start () 
    {
        ps = Object.FindObjectOfType<PlayerScript>();
	}
	

	void Update () 
    {
        transform.position = new Vector3(0, 0, ps.screenPosition.z + 30);


        spawnCooldown += Time.deltaTime;
	    if(spawnCooldown > spawnFreq)
        {
            spawnCooldown = 0.0f;

            for(int i = 0; i < objects.Length; ++i)
            {
                if( Random.Range(0.0f, 1.0f) < possibility[i] )
                {
                    GameObject obj = objects[i];
                    obj.transform.position = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), yPosition[i], transform.position.z);
                    Instantiate(obj);
                }
            }
        }
	}
}
