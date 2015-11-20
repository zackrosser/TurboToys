using UnityEngine;
using System.Collections.Generic;
using System;


public class Controller : MonoBehaviour {

    [Serializable]
    public class PlayerData
    {
        public DriverPicker.KartDrivers driver;
        public KartPicker.KartTypes kart;
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
