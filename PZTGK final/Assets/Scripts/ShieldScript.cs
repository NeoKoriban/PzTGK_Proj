using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour 
{
    public float duration;
    public bool indestructible;
    public float maxHealth;
    public float currentHealth;

    private float startTimer;

	void Start () 
    {
        currentHealth = maxHealth;
        startTimer = Time.time;
	}
	
	void Update () 
    {
        if (startTimer + duration < Time.time)
            Destroy(this.gameObject);

        if ( (currentHealth < 0) && (!indestructible) )
            Destroy(this.gameObject);
	}
}
