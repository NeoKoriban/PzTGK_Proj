using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public float startOfWave;
	public int numberOfEnemiesInWave;
	public float enemySpawnInterval;
	public List<BaseAI> enemyAI = new List<BaseAI> (0);
	public GameObject enemyPrefab;
	public bool destroyAfterLastAction;
	private bool spawning = false;
	private float currentTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawning && currentTimer >= startOfWave) {
			spawning = true;
			currentTimer = 0.0f;
		}
		if (spawning && numberOfEnemiesInWave > 0) {
			if (currentTimer >= enemySpawnInterval) {
				currentTimer = 0.0f;
				GameObject enemy = (GameObject)Instantiate(enemyPrefab, this.transform.position, Quaternion.Euler(0,0,0));
				enemy.name = this.name + " " + numberOfEnemiesInWave--.ToString();
				EnemyScript enemyScript = (EnemyScript)enemy.transform.gameObject.GetComponent(typeof(EnemyScript));
				enemyScript.setAIQueue(enemyAI);
				enemyScript.setDestroyAfteLastAction(destroyAfterLastAction);
			}
		}
		if (numberOfEnemiesInWave > 0) {
			currentTimer += Time.deltaTime;
		}
	}
}
