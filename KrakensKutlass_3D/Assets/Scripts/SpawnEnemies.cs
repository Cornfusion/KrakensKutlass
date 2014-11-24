using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	//Enemy Parameters
	public int numberOfWaves;
	public int enemiesPerWave;


	enum WAVETYPE
	{
		Normal,
		Armoured,
		Air,
		Fast,
		Stealth,
		Boss

	};

	WAVETYPE waveType;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
