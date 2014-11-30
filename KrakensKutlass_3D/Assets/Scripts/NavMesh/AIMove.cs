using UnityEngine;
using System.Collections;

public class AIMove : MonoBehaviour {

	private GameObject goal;
	private NavMeshAgent agent;
	private NavMeshPath path;

	// Use this for initialization
	void Start () 
	{
		goal = GameObject.FindGameObjectWithTag ("Goal");
		agent = GetComponent<NavMeshAgent>();

		path = new NavMeshPath();


	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.C))
		{
			//Debug.Log ("1 " + path.status);
			CalculatePath();
			//Debug.Log ("2 " + path.status);
		}

		
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.transform.tag == "Goal")
		{
			Debug.Log ("ENEMY LEAKED");
			Vector3 resetPos = new Vector3(-30, 0, 0);
			transform.position = resetPos;
		}
	}

	void CalculatePath()
	{
		//Debug.Log ("3 " + path.status);
		Debug.Log ("1 " + agent.destination);
		agent.SetDestination (goal.transform.position);
		Debug.Log ("2 " + agent.destination);

		//agent.CalculatePath (goal.transform.position, path);
		//if(path.status == NavMeshPathStatus.PathInvalid)
		//{
		//	Debug.Log ("No path to goal");
		//}
		//else
		//{
		//	Debug.Log ("Path to goal found!");
		//}
		//Debug.Log ("4 " + path.status);
	}
}
