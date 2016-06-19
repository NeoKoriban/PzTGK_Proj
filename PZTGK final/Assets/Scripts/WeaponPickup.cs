using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour 
{
    public GameObject[] weapon;
    public int additionalAmmo;

	void Start () 
    {
	
	}

	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider col)
    {
        PlayerScript playerScript = col.gameObject.GetComponent<PlayerScript>();

        if ( (playerScript != null) && (weapon.Length > 0) )
        {
            int index = -1;
            for(int i = 0; i < weapon.Length; ++i)
            {
                if (playerScript.ammoType.Contains(weapon[i]))
                {
                    index = i;
                    break;
                }
            }
            
            if(index == -1)
            {
                
                BulletScript bulletScript = weapon[0].GetComponent<BulletScript>();
                for(int i = 0; i < playerScript.ammoType.Count; ++i)
                {
                    BulletScript bulScr = playerScript.ammoType[i].GetComponent<BulletScript>();
                    if (bulletScript.defaultSlot < bulScr.defaultSlot)
                    {
                        playerScript.addWeapon(weapon[0], i, additionalAmmo);
                        break;
                    }
                    
                    if(i + 1 == playerScript.ammoType.Count)
                    {
                        playerScript.addWeapon(weapon[0], i + 1, additionalAmmo);
                        break;
                    }
                }
                 
            }
            else if(index + 1 < weapon.Length)
            {
                int place = playerScript.ammoType.IndexOf(weapon[index]);
                playerScript.ammoType[place] = weapon[index + 1];
            }

            Destroy(this.gameObject);
        }
    }
}
