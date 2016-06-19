using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour 
{
    public GameObject plane;
    public Texture[] textures;

    private PlayerScript p;
    private GameObject tp;
    private GameObject bp;

	void Start () 
    {
        p = Object.FindObjectOfType<PlayerScript>();

        tp = (GameObject)Instantiate(plane);
        bp = (GameObject)Instantiate(plane);


        tp.transform.position = new Vector3(0, -10, p.screenPosition.z + 50.0f);
        bp.transform.position = new Vector3(0, -10, p.screenPosition.z - 50.0f);
	}
	
	void Update () 
    {
        if(bp.transform.position.z + 100 < p.screenPosition.z)
        {
            bp.transform.position = new Vector3(tp.transform.position.x, tp.transform.position.y ,tp.transform.position.z);
            tp.transform.position = new Vector3(tp.transform.position.x, tp.transform.position.y, tp.transform.position.z + 100);
        }
	}
}
