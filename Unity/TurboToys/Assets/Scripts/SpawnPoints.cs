﻿using UnityEngine;
using System.Collections.Generic;

public class SpawnPoints : MonoBehaviour {

    private List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject player;
    public GameObject enemy;
    public List<string> karts = new List<string>();

    private List<GameObject> kartsArray = new List<GameObject>();

    private int playerCount = 0;

    private bool first = true;

    public GameObject controller;
    public Controller controlScript;

	// Use this for initialization
	void Start () {

        controller = GameObject.Find("Character Controller");

        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints.Add(transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < 8; i++)
        {
            karts.Add("AI");
        }

        //How many players are there
        //playerCount = controller.gameObject.GetComponent<Controller>().playerCount;
        controlScript = controller.GetComponent<Controller>();
        playerCount = controlScript.players.Count;

        switch (playerCount)
        {
            case 1:
                karts[4] = "Player1";
                break;
            case 2:
                karts[4] = "Player1";
                karts[5] = "Player2";
                break;
            case 3:
                karts[4] = "Player1";
                karts[5] = "Player2";
                karts[6] = "Player3";
                break;
            case 4:
                karts[4] = "Player1";
                karts[5] = "Player2";
                karts[6] = "Player3";
                karts[7] = "Player4";
                break;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (first == true)
        {
            first = false;
            for (int i = 0; i < 8; i++)
            {
                if(karts[i] == "AI")
                {
                    //Spawn AI
                    kartsArray.Add(Instantiate(enemy, spawnPoints[i].transform.position - new Vector3(0, -0.5f, 0), spawnPoints[i].transform.rotation) as GameObject);
                    kartsArray[i].SetActive(true);
                }else if(karts[i] == "Player1")
                {
                    kartsArray.Add(Instantiate(controlScript.players[0].kart, spawnPoints[i].transform.position - new Vector3(-2, 0, -1), spawnPoints[i].transform.rotation) as GameObject);
                    kartsArray[i].GetComponentInChildren<KartControls>().playerID = 0;
                    kartsArray[i].GetComponentInChildren<KartControls>().enabled = true;
                    Destroy(controlScript.players[0].kart.gameObject);

                    if (playerCount == 4)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                    }else if (playerCount == 3)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0, 0.5f, 1f, 0.5f);
                    }
                    else if (playerCount == 2)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0, 0.5f, 1f, 0.5f);
                    }
                    else if (playerCount == 1)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0, 0f, 1f, 1f);
                    }
                }
                else if (karts[i] == "Player2")
                {
                    kartsArray.Add(Instantiate(controlScript.players[1].kart, spawnPoints[i].transform.position - new Vector3(-2, 0, -1), spawnPoints[i].transform.rotation) as GameObject);
                    kartsArray[i].GetComponentInChildren<KartControls>().playerID = 0;
                    kartsArray[i].GetComponentInChildren<KartControls>().enabled = true;
                    Destroy(controlScript.players[1].kart.gameObject);
                    if (playerCount == 4)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    }
                    else if (playerCount == 3)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0, 0, 0.5f, 0.5f);
                    }
                    else if (playerCount == 2)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0, 0, 1f, 0.5f);
                    }
                    //kartsArray[i].GetComponent<KartControls>().enabled = true;
                }
                else if (karts[i] == "Player3")
                {
                    kartsArray.Add(Instantiate(controlScript.players[2].kart, spawnPoints[i].transform.position - new Vector3(-2, 0, -1), spawnPoints[i].transform.rotation) as GameObject);
                    kartsArray[i].GetComponentInChildren<KartControls>().playerID = 0;
                    kartsArray[i].GetComponentInChildren<KartControls>().enabled = true;
                    Destroy(controlScript.players[2].kart.gameObject);
                    if (playerCount == 4)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0, 0, 0.5f, 0.5f);
                    }
                    else if (playerCount == 3)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                    }
                }
                else if (karts[i] == "Player4")
                {
                    kartsArray.Add(Instantiate(controlScript.players[3].kart, spawnPoints[i].transform.position - new Vector3(-2, 0, -1), spawnPoints[i].transform.rotation) as GameObject);
                    kartsArray[i].GetComponentInChildren<KartControls>().playerID = 0;
                    kartsArray[i].GetComponentInChildren<KartControls>().enabled = true;
                    Destroy(controlScript.players[3].kart.gameObject);
                    if (playerCount == 4)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                    }
                }
            }
        }
    }
}