using UnityEngine;
using System.Collections;

using Pathfinding;

public class AI_Pathfinder : MonoBehaviour {

	public Enemy enemyParams;

	public GameObject goal;
	public Transform goalPosition;

	Seeker seeker;
	Path path;
	int currentWaypoint;

	CharacterController characterController;

	private float speed;
	float closeToWaypoint = 2f;

	void Start()
	{
		goal = GameObject.FindGameObjectWithTag ("Goal");
		goalPosition = goal.transform;

		GameParameters.Instance.bRecalculatePath = false;
		seeker = GetComponent<Seeker>();
		seeker.StartPath (transform.position, goalPosition.position, OnPathComplete);

		characterController = GetComponent<CharacterController> ();
		enemyParams = GetComponent<Enemy>();
		speed = enemyParams.speed;
	}

	void Update()
	{
		if(GameParameters.Instance.bRecalculatePath == true)
		{
			RecalculatePath();
		}
	}

	public void RecalculatePath()
	{
		seeker = GetComponent<Seeker>();
		seeker.StartPath (transform.position, goalPosition.position, OnPathComplete);
		GameParameters.Instance.bRecalculatePath = false;
	}

	public void OnPathComplete(Path p)
	{
		if(!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
		else
		{
			Debug.Log (p.error);
		}
	}

	void FixedUpdate()
	{
		if(path == null)
		{
			return;
		}

		if(currentWaypoint >= path.vectorPath.Count)
		{
			return;
		}

		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized * speed;
		characterController.SimpleMove (dir);

		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) <= closeToWaypoint)
		{
			++currentWaypoint;
		}
	}
}
