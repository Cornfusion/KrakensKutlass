using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildTowers : MonoBehaviour {

	//Pathing
	public PathTester pathTester;

	//List to store each type of tower
	public List<Tower> towerTypes;

	public Tower towerType1;

	//List to store each tower we have built
	public List<Tower> towers;

	// Use this for initialization
	void Start () 
	{
		pathTester = GameObject.FindGameObjectWithTag ("PathTester").GetComponent<PathTester>();
		towers = new List<Tower> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If I press D - manually destroy the last tower
		if(Input.GetKeyDown(KeyCode.D))
		{			
			DestroyLastTower();
		}

		//if(GameParameters.Instance.pathBlocked)
		//{
		//	DestroyLastTower();
		//}
	}

	public void BuildTower(Vector3 pos, Quaternion rot)
	{
		towers.Add((Tower)Instantiate(towerType1, pos, rot));
		pathTester.RecalculatePath ();
	}

	public void DestroyLastTower()
	{
		if(towers.Count-1 >= 0)
		{
			towers [towers.Count-1].RemoveTower ();
		}
		pathTester.RecalculatePath ();
	}

	public void SellTower()
	{

		pathTester.RecalculatePath ();
	}

}
