using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarPathfinding : MonoBehaviour {

	public List<Node> openList = new List<Node>();
	public List<Node> closedList = new List<Node>();
	//public List<Node> nodeList = new List<Node>();

	public int gridWidth;
	public int gridHeight;
	public float tileSize;
	public Node node = new Node ();

	// Use this for initialization
	void Start () 
	{
		GenerateGrid ();

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void GenerateGrid()
	{
		for(int x = 0; x < gridWidth; ++x)
		{
			for(int z = 0; z < gridHeight; ++z)
			{
				node.xCoord = x;
				node.zCoord = z;
				openList.Add ((Node)Instantiate(node, new Vector3(tileSize * x, 0, tileSize * z ), Quaternion.identity));
			}
		}
	}
}
