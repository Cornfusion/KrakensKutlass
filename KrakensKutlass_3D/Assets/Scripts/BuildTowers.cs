using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildTowers : MonoBehaviour {

	//List to store each type of tower
	public List<Tower> towerTypes;

	public Tower towerType1;

	//List to store each tower we have built
	public List<Tower> towers;

	// Use this for initialization
	void Start () 
	{
		towers = new List<Tower> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//public void BuildTower(int type, Vector3 pos, Quaternion rot)
	//{
	//	towers.Add(Instantiate(towerTypes[type], pos, rot));
	//}

	public void BuildTower(Vector3 pos, Quaternion rot)
	{
		towers.Add((Tower)Instantiate(towerType1, pos, rot));
	}

	public void DestroyLastTower()
	{
		//Possible error? may need to reduce count by 1
		if(towers.Count-1 > 0)
		{
			towers [towers.Count-1].RemoveTower ();
		}
	}
}
