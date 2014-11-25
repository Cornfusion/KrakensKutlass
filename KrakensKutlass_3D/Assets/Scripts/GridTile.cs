using UnityEngine;
using System.Collections;

public class GridTile : MonoBehaviour {

	public GameObject basicTower;

	MeshRenderer mr;
	bool showTile = true;



	// Use this for initialization
	void Start () 
	{
		mr = gameObject.GetComponent<MeshRenderer>();
	}

	void OnMouseDown()
	{
		if(showTile == true)
		{
			showTile = !showTile;
			// this object was clicked - do something
			mr.enabled = false;
		}
		else
		{
			showTile = !showTile;
			// this object was clicked - do something
			mr.enabled = true;
		}

		Instantiate (basicTower, transform.position + new Vector3(0, basicTower.transform.localScale.y*1.5f, 0), Quaternion.identity);
		GameParameters.Instance.bRecalculatePath = true;

	}  

	// Update is called once per frame
	void Update () 
	{
	
	}
}
