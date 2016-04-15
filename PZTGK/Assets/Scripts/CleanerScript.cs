using UnityEngine;
using System.Collections;

public class CleanerScript : MonoBehaviour 
{
    private PlayerScript ps;

	void Start () 
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        for(int i = 0; i < allObjects.Length; ++i)
        {
            ps = allObjects[i].GetComponent<PlayerScript>();
            if (ps != null)
                break;
        }
	}

    void Update() 
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            CameraScript cs = obj.GetComponent<CameraScript>();
            Light l = obj.GetComponent<Light>();

            if ( (cs != null) || (l != null) || (obj.gameObject == this.gameObject))
                continue;

            if (obj.transform.position.z < ps.screenPosition.z + ps.minPosition.z - 10)
                Destroy(obj.gameObject);
        }
	}
}
