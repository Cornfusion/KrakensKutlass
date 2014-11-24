using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A place to store miscellaneous global parameters.
/// </summary>
public class GameParameters : ScriptableObject {

    // Convenience method for accessing GameParameters asset
    public static GameParameters Instance
    {
        get
        {
            if (_instance == null)
                _instance = (GameParameters)Resources.Load("GameParameters", typeof(GameParameters));
            if (_instance == null)
                Debug.LogError("GameParameters asset hasn't be created yet! Go to 'Assets/GameParameters'");
            return _instance;
        }
    }
    static GameParameters _instance = null;



	//Pathfinding
	public bool bRecalculatePath;





}
