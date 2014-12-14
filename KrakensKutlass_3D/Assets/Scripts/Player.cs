using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float lives;
	private float defaultLives;
	public int gold;
	public int premiumCurrency;

	//Gold pile
	public Transform goldPile;
	public Vector3 goldPileOriginalSize;
	private float sizePercentage = 1.0f;

	// Use this for initialization
	void Start () 
	{
		goldPile = GameObject.FindGameObjectWithTag ("GoldPile").GetComponentInChildren<Transform>();
		goldPileOriginalSize = goldPile.localScale;
		defaultLives = lives;

	}
	
	// Update is called once per frame
	void Update () 
	{
		GoldPileSize ();
	}

	void GoldPileSize()
	{
		//Debug.Log (lives);
		sizePercentage = (float)(lives / defaultLives);
		//Debug.Log (sizePercentage);
		goldPile.localScale = goldPileOriginalSize * sizePercentage;
	}
}
