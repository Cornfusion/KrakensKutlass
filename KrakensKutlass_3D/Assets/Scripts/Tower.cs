using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public float attackSpeed;
	public int damage;
	public float range;
	public int cost;

	private int shotsFired;

	private float attackTimer;


	//Player stats
	private Player playerStats;

	//Build towers list
	public BuildTowers builtTowers;

	//Collider for range
	//private SphereCollider rangeCollider;

	// Use this for initialization
	void Awake () 
	{
		builtTowers = GameObject.FindGameObjectWithTag("BuiltTowers").GetComponent<BuildTowers>();
		playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void Start()
	{
		attackTimer = attackSpeed;
		//rangeCollider = gameObject.GetComponent<SphereCollider>();
		//rangeCollider.radius = range;
	}
	
	// Update is called once per frame
	void Update () 
	{
		attackTimer -= Time.deltaTime;
		if(attackTimer < 0)
		{
			ShootSingleEnemy();
		}

	}

	void OnMouseDown()
	{
		SellTower ();		
	}  

	public void SellTower()
	{
		//Remove this object from the list of towers
		builtTowers.towers.Remove(this);

		//Give player sell gold
		playerStats.gold += cost / 2;
				
		//Finally, destroy this object
		Destroy(gameObject);
		builtTowers.SellTower();
	}

	public void RemoveTower()
	{
		//Remove this object from the list of towers
		builtTowers.towers.Remove(this);

		//Reimburse the plater
		playerStats.gold += cost;

		//Finally, destroy this object
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.transform.tag == "Enemy")
		{
			Debug.Log (collider.name + " In range");
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if(collider.transform.tag == "Enemy")
		{
			Debug.Log (collider.name + " Out of range");
		}
	}

	void ShootSingleEnemy()
	{
		//Check for all enemies in range
		Collider[] enemiesInRange = Physics.OverlapSphere (transform.position, range);
		//Debug.Log (enemiesInRange.Length);
		//If there is at least one enemy in range
		if(enemiesInRange.Length > 0)
		{
			foreach (Collider collider in enemiesInRange)
			{
				if(collider.tag == "Enemy")
				{
					//Damage the enemy at slot 1
					collider.gameObject.GetComponent<Enemy>().health -= damage;
					Debug.Log ("Damage dealth x " + shotsFired);
					shotsFired++;
					//Only reset the attack timer if we attacked an enemy
					attackTimer = attackSpeed;
					break;
				}
			}

		}
	}

}
