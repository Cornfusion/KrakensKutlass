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
	void Start () 
	{
		builtTowers = GameObject.FindGameObjectWithTag("BuiltTowers").GetComponent<BuildTowers>();
		playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SellTower()
	{
		//Remove this object from the list of towers
		builtTowers.towers.Remove(this);

		//Give player sell gold
		playerStats.gold += cost / 2;

		//Set recalculate paths to true
		//Set grid to recalculate

		//Finally, destroy this object
		Destroy(gameObject);
	}

	public void RemoveTower()
	{
		//Remove this object from the list of towers
		builtTowers.towers.Remove(this);

		//Give player sell gold
		playerStats.gold += cost;
		Debug.Log ("GOLD: " + playerStats.gold);

		//Set recalculate paths to true
		//Set grid to recalculate
		
		//Finally, destroy this object
		Destroy(gameObject);
	}

}
