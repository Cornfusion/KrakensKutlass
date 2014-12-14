using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemies : MonoBehaviour {

	//Enemy parameters
	public Enemy enemy;
	public List<Enemy> enemies;

	public Vector3 enemySpawn;

	//Spawn position modifier
	public float spawnPosMod;
	private float spawnPosModDefault;

	//Wave parameters
	private int waveNumber = 0;
	public int timeBetweenWaves = 10;
	private float currentWaveTime;
	public int enemiesPerWave = 30;
	public int numberOfWaves;
	private bool spawnWaves;
	private bool waveEnded;

	//Spawn parameters
	public float timeBetweenSpawns = 0.5f;
	private float spawnTimer;
	private int enemiesSpawned = 0;

	// Use this for initialization
	void Start () 
	{
		spawnPosModDefault = spawnPosMod;
		spawnPosMod *= -1;

		spawnTimer = timeBetweenSpawns;
		waveEnded = true;
		currentWaveTime = timeBetweenWaves;

		enemies = new List<Enemy>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			SpawnEnemy();
		}

		//DEBUG
		//Debug.Log ("Current wave time: " + currentWaveTime);
		//Debug.Log ("Enemies per wave: " + enemiesPerWave);
		//Debug.Log ("Enemies left: " + enemies.Count);

		//Count down the wave timer
		if(spawnTimer > 0)
		{
			spawnTimer -= Time.deltaTime;
		}
		if(currentWaveTime > 0)
		{
			currentWaveTime -= Time.deltaTime;
		}

		spawnWave ();

		endWave ();
		nextWave ();


	}


	public void RecalculatePath()
	{
		//For each enemy in the enemies list
		//Run their recalculate path function
		foreach (Enemy enemy in enemies)
		{
			AI_Pathfinder pathfinder = enemy.GetComponent<AI_Pathfinder>();
			pathfinder.RecalculatePath();
		}
	}


	void spawnWave()
	{
		//If spawn wave is true and not all enemies have been spawned
		//Spawn an enemy
		if(spawnWaves && enemiesSpawned < enemiesPerWave && spawnTimer < 0)
		{
			spawnTimer = timeBetweenSpawns;
			SpawnEnemy();
		}
	}

	void endWave()
	{
		//If there are no more enemies and all enemies have been spawned
		//End the wave and reset wave timer
		if(enemies.Count <= 0 && enemiesSpawned >= enemiesPerWave && !waveEnded)
		{
			waveEnded = true;
			currentWaveTime = timeBetweenWaves;
		}
	}

	void nextWave()
	{
		//If the wave timer is finished and the wave has ended
		//increment wave number 
		//spawn waves = true
		//reset enemies spawned
		//set wave ended to false
		if(currentWaveTime < 0 && waveEnded)
		{
			waveNumber++;
			spawnWaves = true;
			enemiesSpawned = 0;
			waveEnded = false;
		}
	}

	void SpawnEnemy()
	{
		
		
		if(spawnPosMod > spawnPosModDefault)
		{
			spawnPosMod = -spawnPosModDefault;
		}

		enemySpawn = transform.position;
		enemySpawn.z += spawnPosMod;
		enemies.Add((Enemy)Instantiate(enemy, enemySpawn, Quaternion.identity));
		enemiesSpawned++;

		
		spawnPosMod++;
	//	Debug.Log(spawnPosMod);
	}
}
