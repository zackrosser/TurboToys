using UnityEngine;
using System.Collections.Generic;
using System;


public class Controller : MonoBehaviour {

    [Serializable]
    public class PlayerData
    {
        public string character;
        public string kart;
        public int controllerIndex; // InputDevice?
    }

    public List<PlayerData> players;


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
