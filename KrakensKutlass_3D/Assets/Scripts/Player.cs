using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int lives;
	public int gold;
	public int premiumCurrency;



	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.R))
		{
			GameParameters.Instance.bRecalculatePath = true;
		}
	}
}
