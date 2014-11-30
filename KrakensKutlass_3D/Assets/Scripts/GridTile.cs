using UnityEngine;
using System.Collections;


public class GridTile : MonoBehaviour {


	public BuildTowers buildTowers;

	//public GameObject lastBuiltTower;


	//Variable to store the mesh renderer (We will want to hide this when a tower is built on this tile)
	//show tile bool
	MeshRenderer mr;
	bool showTile = true;


	// Use this for initialization
	void Start () 
	{
		buildTowers = GameObject.FindGameObjectWithTag("BuiltTowers").GetComponent<BuildTowers>();

		mr = gameObject.GetComponent<MeshRenderer>();
	}

	void OnMouseDown()
	{
		if(showTile == true)
		{
			showTile = false;
			// this object was clicked - do something
			mr.enabled = false;
		}
		else
		{
			showTile = true;
			// this object was clicked - do something
			mr.enabled = true;
		}


		BuildBasicTower ();
		GameParameters.Instance.bRecalculatePath = true;

	}  

	// Update is called once per frame
	void Update () 
	{
		if(GameParameters.Instance.pathBlocked)
		{
			//AstarPath.active.UpdateGraphs(this.collider.bounds);
			//GameParameters.Instance.pathBlocked = false;

			//GameParameters.Instance.bRecalculatePath = true;

			//AstarPath.active.UpdateGraphs(buildTowers.towers[buildTowers.towers.Count].collider.bounds);

			//buildTowers.DestroyLastTower();

			//Show grid square again
			//showTile = true;
			// this object was clicked - do something
			//mr.enabled = true;
		}

		//If I press D - manually destroy the last tower
		if(Input.GetKeyDown(KeyCode.D))
		{
			buildTowers.DestroyLastTower();
			//AstarPath.active.UpdateGraphs(gameObject.collider.bounds);
			AstarPath.active.Scan();
			GameParameters.Instance.bRecalculatePath = true;
		}
	}

	void BuildBasicTower()
	{
		buildTowers.BuildTower (transform.position + new Vector3 (0, 0.5f, 0), Quaternion.identity);

	}
}
