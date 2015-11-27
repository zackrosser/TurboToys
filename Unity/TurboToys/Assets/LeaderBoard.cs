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
    private GameObject winPoints;

    int count = 0;

	// Use this for initialization
	void Start () {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoints").GetComponent<SpawnPoints>();
        winPoints = GameObject.FindGameObjectWithTag("WinSpawnPoints");
        karts = spawnPoint.kartsArray;        
        spawnPoint.playerCount = players;

        for (int i = 0; i < 8; i++)
        {
            KartData kart = new KartData();
            kart.name = karts[i].transform.GetComponentInChildren<LapCount>().name;
            kart.kart = karts[i].gameObject;
            kart.place = 0;       
            leaderBoard.Add(kart);
        }
	}
	
    void Update()
    {
        count++;
        if(count >= 60)
        {
            count = 0;
            UpdateKart();
        }
    }

	// Update is called once per frame
	void UpdateKart () {

        for (int i = 0; i < 8; i++)
        {
            GameObject kart = leaderBoard[i].kart;
            leaderBoard[i].wayPoint = (kart.GetComponentInChildren<LapCount>().lapCount + 1) * kart.GetComponentInChildren<LapCount>().currentWaypoint + 1;
        }

        leaderBoard.Sort((a, b) => b.wayPoint.CompareTo(a.wayPoint));

        for (int i = 0; i < 8; i++)
        {
            GameObject kart = leaderBoard[i].kart;
            if (leaderBoard[i].finished == false)
            {
                leaderBoard[i].place = i + 1;
                if (leaderBoard[i].wayPoint == 3 * 106)
                {
                    kart.transform.GetComponent<KartActive>().kartOn = false;
                    if (kart.transform.GetComponent<KartActive>().playerKart == true)
                    {
                        kart.transform.GetChild(0).position = winPoints.transform.GetChild(leaderBoard[i].place - 1).position;
                        kart.transform.GetChild(0).rotation = winPoints.transform.GetChild(leaderBoard[i].place - 1).rotation;
                        kart.transform.GetChild(0).GetComponent<KartControls>().rb.velocity = new Vector3(0, 0, 0); ;
                        kart.transform.GetChild(0).GetComponent<KartControls>().playerCam.enabled = false;
                    }
                    else
                    {
                        kart.transform.position = winPoints.transform.GetChild(leaderBoard[i].place - 1).position;
                        kart.transform.rotation = winPoints.transform.GetChild(leaderBoard[i].place - 1).rotation;
                        kart.transform.GetComponent<AIKart>().rb.velocity = new Vector3(0,0,0);
                    }
                    leaderBoard[i].finished = true;
                    
                }
            }
        }

       

	}
}
