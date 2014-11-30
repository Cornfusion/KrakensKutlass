using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemies : MonoBehaviour {

	//Pathing
	public GameObject pathTester;

	//Enemy parameters
	public Enemy enemy;
	public List<Enemy> enemies;

	//Game parameters
	public GameObject goal;
	public Transform goalPosition;
	public Vector3 enemySpawn;

	//Wave parameters
	private int waveNumber = 0;
	public int timeBetweenWaves = 10;
	private float currentWaveTime;
	public float timeBetweenSpawns = 0.5f;
	private float spawnTimer;
	public int enemiesPerWave = 30;
	private int enemiesSpawned = 0;
	public int numberOfWaves;

	private bool spawnWaves;
	private bool waveEnded;

	public BuildTowers buildTowers;

	//enum WAVETYPE
	//{
	//	Normal,
	//	Armoured,
	//	Air,
	//	Fast,
	//	Stealth,
	//	Boss
	//
	//};

	//WAVETYPE waveType;

	// Use this for initialization
	void Start () 
	{
		pathTester = GameObject.FindGameObjectWithTag ("PathTester");
		spawnTimer = timeBetweenSpawns;
		waveEnded = true;
		currentWaveTime = timeBetweenWaves;
		goal = GameObject.FindGameObjectWithTag ("Goal");
		goalPosition = goal.transform;
		enemies = new List<Enemy>();

		buildTowers = GameObject.FindGameObjectWithTag("BuiltTowers").GetComponent<BuildTowers>();

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

		//If the global parameter wants us to recalculate the path for each object
		//(Usually after a tower is built or destoryed)
		if(GameParameters.Instance.bRecalculatePath)
		{
			//Run the calculate path function
			//Then turn off recalculate path
			RecalculatePath ();
			GameParameters.Instance.bRecalculatePath = false;
		}
	}

	//Any object that needs to recalculate its path should be put into this function
	//Only after each object has had their path recalculated - we set recalculate to false 
	//This is why each recalculate should be done here.
	//The path tester should recalculate first to find any problems
	//If there are problems - break, fix them, then recalculate again.
	void RecalculatePath()
	{
		//Run the recalculate path function in our path tester
		//This will tell us if the path is blocked OR not
		pathTester.GetComponent<PathTester>().RecalculatePath();

		//If the path is blocked, we want to remove the last built tower
		//Break out of function
		//Recalculate path
		//Recalculate path should be set to TRUE again once the last tower has been removed
		if(GameParameters.Instance.pathBlocked)
		{
			buildTowers.DestroyLastTower();


			return;
		}
		//If the path is not blocked
		//calculate the path for each object
		//Set recalculate path to false
		else
		{
			foreach (Enemy enemy in enemies)
			{
				AI_Pathfinder pathfinder = enemy.GetComponent<AI_Pathfinder>();
				pathfinder.RecalculatePath();
				//MeshRenderer mr = enemy.GetComponent<MeshRenderer>();
				//mr.material.color = Color.red;
				
				//Debug.Log (enemies.Count);
			}
			//Once each objects path has been recalculated
			//Set recalculate path to false
			GameParameters.Instance.bRecalculatePath = false;
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
		enemySpawn = transform.position;
		enemySpawn.z += Random.Range(-2, 2);
		enemies.Add((Enemy)Instantiate(enemy, enemySpawn, Quaternion.identity));
		enemiesSpawned++;
	}
}
