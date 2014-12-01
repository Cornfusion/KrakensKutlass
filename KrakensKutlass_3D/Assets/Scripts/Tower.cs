using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public float attackSpeed;
	public int damage;
	public int range;
	public int cost;

	//Player stats
	private Player playerStats;

	//Build towers list
	public BuildTowers builtTowers;

	// Use this for initialization
	void Awake () 
	{
		builtTowers = GameObject.FindGameObjectWithTag("BuiltTowers").GetComponent<BuildTowers>();
		playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
	{

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

}
