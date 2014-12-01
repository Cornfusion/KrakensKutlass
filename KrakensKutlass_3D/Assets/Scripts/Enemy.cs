using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//Enemy parameters
	public int health;
	public float speed;
	public int bounty;

	//Variable to store the spawner object
	public SpawnEnemies spawner;

	//Player
	private GameObject player;
	private Player playerStats;


	
	void Start() 
	{
		//Find the spawner object and store it
		spawner = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnEnemies>();

		//Find the player object and store it
		player = GameObject.FindGameObjectWithTag ("Player");
		playerStats = player.GetComponent<Player>();
	}

	void Update()
	{
		if(health < 0)
		{
			OnDeath ();
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.transform.tag == "Goal")
		{
			OnLeak ();
		}
	}

	void OnDeath()
	{
		//add gold to the player
		playerStats.gold += bounty;
		//Remove this object from the enemies list
		spawner.enemies.Remove(this);
		//Destroy this object
		Destroy(gameObject);
	}

	void OnLeak()
	{
		//Reduce player lives by 1
		playerStats.lives--;
		//Remove this object from the enemies list
		spawner.enemies.Remove(this);
		//Destroy this object
		Destroy(gameObject);
	}

}
