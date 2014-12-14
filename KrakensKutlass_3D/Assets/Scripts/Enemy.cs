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


	public struct sMovement
	{
		public Vector3 Direction;
		public float Speed; // aka acceleration
		public Vector3 Velocity; // the velocity of this object Direction * Speed
		public Vector3 NEXTnode; // holds the next node its travling to
		public Vector3 LASTnode; // holds the last node its traveling to

	}
	public sMovement Movement = new sMovement();

	
	void Start() 
	{
		//Find the spawner object and store it
		spawner = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnEnemies>();

		//Find the player object and store it
		player = GameObject.FindGameObjectWithTag ("Player");
		playerStats = player.GetComponent<Player>();

		//initialise the base paramaters
		//these can be overrided
		InitMovementValues();
	}

	void Update()
	{
		if(health < 0)
		{
			OnDeath ();
		}
		//move the enemy this can be overrided
		UpdateMovement();
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

	public void SetMovementNode( Vector3 MOVEMENT_NODE )
	{

		Movement.LASTnode = Movement.NEXTnode;
		Movement.NEXTnode = MOVEMENT_NODE;
	}

	virtual public void InitMovementValues() // set as virtual to be overrided by inhertitide classes
	{
		Movement.Direction = new Vector3(0,1,0);
		Movement.Speed = 1.0f;
		Movement.Velocity = new Vector3();
		Movement.NEXTnode = new Vector3();
		Movement.LASTnode = new Vector3();
	}

	virtual public void UpdateMovement()  // set as virtual to be overrided by inhertitide classes
	{

		Movement.Direction = (Movement.NEXTnode - this.transform.position).normalized;
		Debug.Log(Movement.Direction );
	//Movement.Direction.y = 0.00f;
		Vector3 LookatPoint = new Vector3(Movement.NEXTnode.x,0.00f,Movement.NEXTnode.z);
		//this.transform.LookAt(LookatPoint);
		this.transform.forward = LookatPoint;
		Movement.Velocity = Movement.Direction * Movement.Speed;

		this.transform.position += Movement.Velocity * Time.deltaTime;




	}

}
