using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public float startOfWave;
	public int numberOfEnemiesInWave;
	public float enemySpawnInterval;
	public List<BaseAI> enemyAI = new List<BaseAI> (0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
