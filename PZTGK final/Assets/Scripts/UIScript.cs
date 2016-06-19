using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour 
{
    private Text currentHealth;
    private Text maxHealth;
    private Text points;

    private PlayerScript ps;

	void Start () 
    {
        ps = Object.FindObjectOfType<PlayerScript>();
        currentHealth = GameObject.Find("CurrentHealth").GetComponent<Text>();
        maxHealth = GameObject.Find("MaxHealth").GetComponent<Text>();
        points = GameObject.Find("Points").GetComponent<Text>();


        maxHealth.text = ps.maxHealth.ToString();
	}
	
	void Update () 
    {
        currentHealth.text = ps.currenHealth.ToString();
        points.text = ps.score.ToString();
	}
}
