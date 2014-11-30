using UnityEngine;
using System.Collections;

using Pathfinding;

public class PathTester : MonoBehaviour {

	//Variable to store the shortest path possible
	//If the path length is shorter than this veriable, we know the path is blocked
	private int quickestPath = 0;
	
	private bool runOnce = false;

	//Stores the goal position
	public Vector3 goalPosition;

	//Used to path find
	Seeker seeker;
	Path path;

	void Start()
	{
		goalPosition = GameObject.FindGameObjectWithTag ("Goal").transform.position;

		seeker = GetComponent<Seeker>();
		seeker.StartPath (transform.position, goalPosition, OnPathComplete);
	}

	//Recalculates the objects path based on the current grid
	public void RecalculatePath()
	{
		seeker.StartPath (transform.position, goalPosition, OnPathComplete);
	}

	public void OnPathComplete(Path p)
	{
		if(!p.error)
		{
			path = p;

			//The first time a path is calculated (Before towers are placed)
			//We will store the length in our quickestPath variable
			//We only want this to run once, otherwise it will overwrite with its current path
			if(!runOnce)
			{
				quickestPath = path.vectorPath.Count;
				runOnce = true;
			}

			//If our current path length is less than the quickest path
			//We know the path is blocked (It won't reach the goal so the path is shortened to where it is blocked -
			//therefore the path length will be shorter)
			//Set the global path blocked bool to true
			if(p.vectorPath.Count < quickestPath)
			{
				GameParameters.Instance.pathBlocked = true;
			}
			//If the path is longer or equal to the quickest path
			//The path blocked bool is set to false
			else
			{
				GameParameters.Instance.pathBlocked = false;
			}

		}
		else
		{
			Debug.Log (p.error);
		}
	}
}
