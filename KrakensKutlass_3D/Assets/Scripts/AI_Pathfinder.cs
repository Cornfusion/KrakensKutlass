using UnityEngine;
using System.Collections;

using Pathfinding;

public class AI_Pathfinder : MonoBehaviour {

	//Store the objects parameters so the pathfinding can access things like move Speed etc.
	public Enemy enemyParams;	

	//Local variable to store speed (We get the speed at start up from enemyParams)
	private float speed;

	//The goals position
	public Vector3 goalPosition;

	//Seeker for path finding
	//Path to store the path found
	Seeker seeker;
	Path path;

	//The current waypoint we want the object to move towards
	int currentWaypoint;

	//Stores our character controller
	CharacterController characterController;


	public float closeToWaypoint = 2f;

	void Start()
	{
		goalPosition = GameObject.FindGameObjectWithTag ("Goal").transform.position;

		seeker = GetComponent<Seeker>();
		seeker.StartPath (transform.position, goalPosition, OnPathComplete);

		characterController = GetComponent<CharacterController> ();

		enemyParams = GetComponent<Enemy>();
		speed = enemyParams.speed;

	}

	void Update()
	{

	}

	//Recalculates the path based on the current grid
	public void RecalculatePath()
	{
		seeker.StartPath (transform.position, goalPosition, OnPathComplete);
	}

	public void OnPathComplete(Path p)
	{
		if(!p.error)
		{
			//If the path (p) has no errors
			//Store p in our path variable
			path = p;
			//Reset our current waypoint to 0
			currentWaypoint = 0;

		}
		//If there is an error
		//Print it.
		else
		{
			Debug.Log (p.error);
		}
	}

	void FixedUpdate()
	{
		//If we don't have a path
		//Return
		if(path == null)
		{
			return;
		}

		//If we are at the last waypoint
		//Return
		if(currentWaypoint >= path.vectorPath.Count)
		{
			return;
		}

		//Store the direction towards the next waypoint
		//Then simple move in that direction
		//Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized * speed;
		//characterController.SimpleMove (dir);

		enemyParams.SetMovementNode( path.vectorPath [currentWaypoint] );


		//Vector3 node = new Vector3(path.vectorPath [currentWaypoint].x, 0.00f, path.vectorPath [currentWaypoint].y);
		//Vector3 RealDir = (node - transform.position).normalized;
		//this.transform.LookAt( RealDir  );

		//When we are close enough to the next waypoint
		//Get the next waypoint
		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) <= closeToWaypoint)
		{
			++currentWaypoint;
		}
	}
}
