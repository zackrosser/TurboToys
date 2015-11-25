using UnityEngine;
using System.Collections.Generic;
using System;

public class LeaderBoard : MonoBehaviour {

    [Serializable]
    public class KartData
    {
        public string name;
        public GameObject kart;
        public int place;
        public bool finished = false;
        public int wayPoint;
    }

    public SpawnPoints spawnPoint;
    public List<GameObject> karts;

    public List<KartData> leaderBoard;
    private int players = 0;

	// Use this for initialization
	void Start () {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoints").GetComponent<SpawnPoints>();
        karts = spawnPoint.kartsArray;
        spawnPoint.playerCount = players;
        for(int i = 0; i < 8; i++)
        {
            KartData kart = new KartData();
            kart.name = "Test";
            kart.kart = karts[i].gameObject;
            kart.place = 0;
            kart.wayPoint = 0;
            leaderBoard.Add(kart);
        }
	}
	
	// Update is called once per frame
	void LateUpdate () {
        for (int i = 0; i < 8; i++ )
        {
            //leaderBoard[i].wayPoint = (karts[i].gameObject.GetComponent<LapCount>().lapCount+1) * karts[i].gameObject.GetComponent<LapCount>().currentWaypoint;
        }
	}
}
