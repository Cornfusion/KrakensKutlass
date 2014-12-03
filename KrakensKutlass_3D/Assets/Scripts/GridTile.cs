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
		BuildBasicTower ();
	}  

	// Update is called once per frame
	void Update () 
	{
		ShowHideGrid ();
	}

	void BuildBasicTower()
	{
		buildTowers.BuildTower (transform.position + new Vector3 (0, 0.5f, 0), Quaternion.identity);

	}

	void ShowHideGrid()
	{
		if(Input.GetKeyDown (KeyCode.Z))
		{
			showTile = !showTile;
		}
		if(showTile == false)
		{
			mr.enabled = false;
		}
		else
		{
			mr.enabled = true;
		}
	}

	void OnMouseOver()
	{
		mr.enabled = false;
	}
}
