using UnityEngine;
using System.Collections;

using Pathfinding;

public class PathTester : MonoBehaviour {


	//Stores the goal position
	public Vector3 goalPosition;

	//Used to path find
	Seeker seeker;
	Path path;

	//Variable to access the towers
	public BuildTowers buildTowers;

	//Variable to access the enemies list
	public SpawnEnemies enemies;

	public bool pathIsBlocked = false;

	void Start()
	{

		//Initialise the goal position
		goalPosition = GameObject.FindGameObjectWithTag ("Goal").transform.position;

		//Inistialise the seeker
		//Give the seeker position to path to
		seeker = GetComponent<Seeker>();
		seeker.StartPath (transform.position, goalPosition, OnPathComplete);

		//Initialise build towers to the build towers class
		buildTowers = GameObject.FindGameObjectWithTag("BuiltTowers").GetComponent<BuildTowers>();

		//Initialise enemies
		enemies = GameObject.FindGameObjectWithTag ("Spawn").GetComponent<SpawnEnemies>();
	}

	//Recalculates the objects path based on the current grid
	public void RecalculatePath()
	{
		//First, we rescan the graph to check for changes
		AstarPath.active.Scan();

		//Then we give the seeker a new position to path to
		seeker.StartPath (transform.position, goalPosition, OnPathComplete);
	}

	public void OnPathComplete(Path p)
	{
		//If the path has no error
		if(!p.error)
		{
			//Store the path (p) in our path variable
			path = p;

			//Some lines to help with debugging
			//Yellow is the last node in the path
			//Red is the goals actualy position
			//Blue is the goals position - a bit
			Debug.DrawRay(new Vector3(p.vectorPath[p.vectorPath.Count-1].x, p.vectorPath[p.vectorPath.Count-1].y, p.vectorPath[p.vectorPath.Count-1].z), Vector3.up * 10, Color.yellow, 1);
			Debug.DrawRay(new Vector3(goalPosition.x, goalPosition.y, goalPosition.z), Vector3.up * 10, Color.red, 1);
			Debug.DrawRay(new Vector3(goalPosition.x - 1, goalPosition.y, goalPosition.z), Vector3.up * 10, Color.blue, 1);

			//If the last node in the path is not the goals position
			//The path is blocked - destroy the last tower
			if(p.vectorPath[p.vectorPath.Count-1].x < goalPosition.x - 1)
			{
				buildTowers.DestroyLastTower();
			}
			//If the path is not blocked
			//recalculate each enemys path
			else
			{
				enemies.RecalculatePath();
			}
		}
		else
		{
			//Debug.Log (p.error);
		}
	}
}
