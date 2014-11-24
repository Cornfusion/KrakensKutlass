using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//Enemy parameters
	public int health;
	public float speed;
	public int bounty;



	//Player
	private GameObject player;
	private Player playerStats;


	
	void Start() 
	{
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
			//Reduce player lives by 1
			--playerStats.lives;
			
			//Destroy this object
			Destroy(gameObject);
		}
	}



	void OnDeath()
	{
		playerStats.gold += bounty;
		Destroy (gameObject);
	}

}
