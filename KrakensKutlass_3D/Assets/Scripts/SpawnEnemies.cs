using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemies : MonoBehaviour {

	//Enemy Parameters
	public int numberOfWaves;
	public int enemiesPerWave;
	public Enemy enemy;

	public GameObject goal;
	public Transform goalPosition;

	public List<Enemy> enemies;

	enum WAVETYPE
	{
		Normal,
		Armoured,
		Air,
		Fast,
		Stealth,
		Boss

	};

	WAVETYPE waveType;

	// Use this for initialization
	void Start () 
	{
		goal = GameObject.FindGameObjectWithTag ("Goal");
		goalPosition = goal.transform;
		enemies = new List<Enemy>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Vector3 spawnPos = transform.position;
			spawnPos.z += Random.Range(-2, 2);
			enemies.Add((Enemy)Instantiate(enemy, spawnPos, Quaternion.identity));
		}
		RecalculatePath ();
	}

	void RecalculatePath()
	{
		if(GameParameters.Instance.bRecalculatePath == true)
		{
			foreach (Enemy enemy in enemies)
			{
				AI_Pathfinder pathfinder = enemy.GetComponent<AI_Pathfinder>();
				pathfinder.RecalculatePath();
			}
			GameParameters.Instance.bRecalculatePath = false;
		}

	}
}
