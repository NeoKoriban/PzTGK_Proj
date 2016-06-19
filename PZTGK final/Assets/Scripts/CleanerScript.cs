using UnityEngine;
using UnityEngine.UI;
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
            UIScript u = obj.GetComponent<UIScript>();
            Text t = obj.GetComponent<Text>();

             
            if ( (cs != null) || (l != null) || (u != null) || (t != null) || (obj.gameObject == this.gameObject))
                continue;

            if (obj.transform.position.z < ps.screenPosition.z + ps.minPosition.z - 120)
                Destroy(obj.gameObject);
        }
	}
}
