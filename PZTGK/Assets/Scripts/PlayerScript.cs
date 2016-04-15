using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour 
{
    public Vector3 screenPosition;
    public Vector3 gameSpeed;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public Vector3 moveSpeed;
    public Vector3 maxMoveSpeed;

    public float effectiveCooldown;
    public float minEffectiveCooldown;

    public float maxHealth;
    public float currenHealth;

    public int selectedAmmo;
    public List<GameObject> ammoType;

    private float score;

    private GameObject shield = null;
    private List<Vector3> bulletCreationPosition = new List<Vector3>();
    private List<float> shootCooldown = new List<float>();
    private List<int> ammoAmount = new List<int>();
    private List<float> shootTimer = new List<float>();


	void Start () 
    {
        screenPosition = transform.position;
        currenHealth = maxHealth;

        for (int i = 0; i < ammoType.Count; ++i )
        {
            BulletScript bulScr = ammoType[i].GetComponent<BulletScript>();
            bulletCreationPosition.Add(bulScr.creationPosition);
            shootCooldown.Add(bulScr.cooldown);
            ammoAmount.Add(bulScr.startAmount);
            shootTimer.Add(Time.time - shootCooldown[i]);
        }

        if (selectedAmmo > ammoType.Count)
            selectedAmmo = 0;
        if (ammoType.Count == 0)
            selectedAmmo = -1;
	}
	
	void Update () 
    {
        movement();

        if(shield != null)
            shield.transform.position = transform.position;

        ammoSelect();
        shoot();

        if (currenHealth <= 0)
            Destroy(this.gameObject);
	}

    private void movement()
    {
        screenPosition = new Vector3(screenPosition.x + gameSpeed.x * Time.deltaTime,
                             screenPosition.y + gameSpeed.y * Time.deltaTime,
                             screenPosition.z + gameSpeed.z * Time.deltaTime);

        transform.position = new Vector3(transform.position.x + gameSpeed.x * Time.deltaTime,
                                         transform.position.y + gameSpeed.y * Time.deltaTime,
                                         transform.position.z + gameSpeed.z * Time.deltaTime);

        float input = Input.GetAxis("Vertical");
        transform.position = new Vector3(transform.position.x,
                                         transform.position.y,
                                         transform.position.z + moveSpeed.z * input);

        input = Input.GetAxis("Horizontal");
        transform.position = new Vector3(transform.position.x + moveSpeed.x * input,
                                         transform.position.y,
                                         transform.position.z);

        Vector3 positionCorrection = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (transform.position.x - screenPosition.x < minPosition.x)
            positionCorrection.x = minPosition.x + screenPosition.x;
        else if (transform.position.x - screenPosition.x > maxPosition.x)
            positionCorrection.x = maxPosition.x + screenPosition.x;
        if (transform.position.z - screenPosition.z < minPosition.z)
            positionCorrection.z = minPosition.z + screenPosition.z;
        else if (transform.position.z - screenPosition.z > maxPosition.z)
            positionCorrection.z = maxPosition.z + screenPosition.z;
        transform.position = positionCorrection;
    }

    private void ammoSelect()
    {
        if (selectedAmmo == -1)
            return;

        if ( Input.GetButtonDown("PrevAmmo") )
        {
            if (--selectedAmmo < 0)
                selectedAmmo = ammoType.Count - 1;
        }
        else if (Input.GetButtonDown("NextAmmo"))
        {
            if (++selectedAmmo >= ammoType.Count)
                selectedAmmo = 0;
        }
    }

    private void shoot()
    {
        if (selectedAmmo == -1)
            return;

        GameObject bullet = ammoType[selectedAmmo];

        if ( (Input.GetButton("Fire1")) && (shootTimer[selectedAmmo] + shootCooldown[selectedAmmo]*effectiveCooldown < Time.time) )
        {
            if (ammoAmount[selectedAmmo] > 0) 
            {
                bullet.transform.position = new Vector3(transform.position.x + bulletCreationPosition[selectedAmmo].x,
                                                        transform.position.y + bulletCreationPosition[selectedAmmo].y, 
                                                        transform.position.z + bulletCreationPosition[selectedAmmo].z);

                shootTimer[selectedAmmo] = Time.time;
                Instantiate(bullet);
                --ammoAmount[selectedAmmo];;
            }
            else if (ammoAmount[selectedAmmo] == -1)
            {
                bullet.transform.position = new Vector3(transform.position.x + bulletCreationPosition[selectedAmmo].x,
                                                        transform.position.y + bulletCreationPosition[selectedAmmo].y,
                                                        transform.position.z + bulletCreationPosition[selectedAmmo].z);

                shootTimer[selectedAmmo] = Time.time;
                Instantiate(bullet);
            }
        }
    }

    public void addAmmo(GameObject type, int amount)
    {
        for (int i = 0; i < ammoType.Count; ++i)
        {
            if (ammoType[i] == type)
            {
                ammoAmount[i] += amount;
                break;
            }
        }
    }

    public void addScore(float amount)
    {
        score += amount;
    }

    public void addHealth(float amount)
    {
        currenHealth += amount;
        if (currenHealth > maxHealth)
            currenHealth = maxHealth;
    }

    public void addWeapon(GameObject weapon, int index, int ammo)
    {
        BulletScript bulletScript = weapon.GetComponent<BulletScript>();

        ammoType.Insert(index, weapon);
        bulletCreationPosition.Insert(index, bulletScript.creationPosition);
        ammoAmount.Insert(index, ammo);
        shootCooldown.Insert(index, bulletScript.cooldown);
        shootTimer.Add(Time.time - bulletScript.cooldown);
    }

    public void addShield(GameObject shi)
    {
        if(shield == null)
        {
            shield = shi;
            shield.transform.position = transform.position;
            shield = Instantiate(shield);
        }
        else
        {
            Destroy(shield.gameObject);
            shield = shi;
            shield.transform.position = transform.position;
            shield = Instantiate(shield);
        }
    }

    public void addCooldownReduction(float amount)
    {
        effectiveCooldown -= amount;
        if (effectiveCooldown < minEffectiveCooldown)
            effectiveCooldown = minEffectiveCooldown;
    }

    public void addMoveSpeed(float amount)
    {
        moveSpeed.x += amount;
        moveSpeed.z += amount;

        if (moveSpeed.x > maxMoveSpeed.x)
            moveSpeed.x = maxMoveSpeed.x;

        if (moveSpeed.z > maxMoveSpeed.z)
            moveSpeed.z = maxMoveSpeed.z;
    }
}
