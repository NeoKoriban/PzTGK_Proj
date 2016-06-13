using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour 
{
	public float points;

	public float maxHealth;
	public float currentHealth;

	public GameObject[] ammoType;

	public GameObject[] lootType;
	public float[] lootPossibility;
	private List<BaseAI> AIQueue;
	protected bool destroyAfterLastAction;
	private int currentAction;
	private float currentActionTime;
	private float timeSinceLastShot;
	private bool killed = false;
	private Vector2 differenceBetweenPoints;

	private bool isShuttingDown = false;

	public void setAIQueue(List<BaseAI> list) {
		this.AIQueue = list;
	}

	public void setDestroyAfteLastAction(bool destroy) {
		this.destroyAfterLastAction = destroy;
	}

	public List<BaseAI> getAIQueue() {
		return AIQueue;
	}

	public bool getDestroyAfterLastAction() {
		return destroyAfterLastAction;
	}

	protected void Start ()
	{
		currentHealth = maxHealth;
		currentActionTime = 0.0f;
		timeSinceLastShot = 0.0f;
		currentAction = 0;

		if (lootType.Length != lootPossibility.Length)
			lootPossibility = new float[lootPossibility.Length];
	}

	protected void Update ()
	{
		currentActionTime += Time.deltaTime;
		Debug.Log (currentAction + " - " + currentActionTime);
		timeSinceLastShot += Time.deltaTime;
		if (currentHealth <= 0.0f) {
			Destroy (this.gameObject);
			killed = true;
		}

		if (AIQueue == null || AIQueue.Count == 0 || currentAction > AIQueue.Count - 1) { return; }

		switch (AIQueue [currentAction].typeOfMove) {
			case 0: //idle
			break;
			case 1: //linear
			Vector3 position = this.transform.position;
			position.x += differenceBetweenPoints.x * Time.deltaTime / AIQueue [currentAction].lengthOfAction;
			position.z += differenceBetweenPoints.y * Time.deltaTime / AIQueue [currentAction].lengthOfAction;
			this.transform.position = position;
			break;
		}
		if (AIQueue[currentAction].shootInterval > 0.0f && timeSinceLastShot >= AIQueue [currentAction].shootInterval) {
			shoot (0); //TODO: zmieńcie to sobie na jakiś typ, bo się na tym nie znam
			timeSinceLastShot = 0.0f;
		}
		if (currentActionTime >= AIQueue [currentAction].lengthOfAction) {
			if (++currentAction == AIQueue.Count) {
				if (destroyAfterLastAction) {
					Destroy (this.gameObject);
				} else {
					currentAction = 0;
					currentActionTime = 0.0f;
					UpdateDifference ();
				}
			} else {
				currentActionTime = 0.0f;
				UpdateDifference ();
			}
		}
	}

	protected void UpdateDifference()
	{
		if (AIQueue == null || AIQueue.Count == 0 || currentAction > AIQueue.Count - 1) { return; }

		Vector3 pos = this.transform.position;
		differenceBetweenPoints = AIQueue[currentAction].finalPointCoords - new Vector2(pos.x, pos.z);
	}

	void OnDestroy()
	{
		if(!isShuttingDown && killed)
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
		if (ammoType.Length < number)
			return;

		GameObject bullet = ammoType[number];
		bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		Instantiate(bullet);
	}
}
