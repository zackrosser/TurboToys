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
    public List<KartData> UnSortedLeaderBoard;
    private int players = 0;

	// Use this for initialization
	void Start () {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoints").GetComponent<SpawnPoints>();
        karts = spawnPoint.kartsArray;        
        spawnPoint.playerCount = players;

        for (int i = 0; i < 8; i++)
        {
            KartData kart = new KartData();
            kart.name = "Test";
            kart.kart = karts[i].gameObject;
            kart.place = 0;       
            leaderBoard.Add(kart);
        }
	}
	
	// Update is called once per frame
	void LateUpdate () {

        for (int i = 0; i < 8; i++)
        {
            GameObject kart = leaderBoard[i].kart;
            leaderBoard[i].wayPoint = (kart.GetComponentInChildren<LapCount>().lapCount + 1) * kart.GetComponentInChildren<LapCount>().currentWaypoint + 1;
        }

        leaderBoard.Sort((a, b) => b.wayPoint.CompareTo(a.wayPoint));

        for (int i = 0; i < 8; i++)
        {
            GameObject kart = leaderBoard[i].kart;
            leaderBoard[i].place = i + 1;
            if(leaderBoard[i].wayPoint == 3 * 106)
            {
                leaderBoard[i].finished = true;
            }
        }

       

	}
}
