using UnityEngine;
using System.Collections.Generic;

public class SpawnPoints : MonoBehaviour {

    private GameObject PlayerKart;   //Ed test

    private List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject player;
    public GameObject enemy;
    public List<string> karts = new List<string>();

    public List<GameObject> kartsArray = new List<GameObject>();

    public int playerCount = 0;

    private bool first = true;

    public Controller controlScript;

	// Use this for initialization
	void Start () {

        PlayerKart = Resources.Load<GameObject>("PlayerKart");

        controlScript = GameObject.FindGameObjectWithTag("CharacterLoader").GetComponent<Controller>();

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

        SpawnKarts();
    }

    void SpawnKarts()
    {
        for (int i = 0; i < 8; i++)
        {
            if (karts[i] == "AI")
            {
                //Spawn AI
                kartsArray.Add(Instantiate(enemy, spawnPoints[i].transform.position - new Vector3(0, -0.3f, 0), Quaternion.identity) as GameObject);
                kartsArray[i].SetActive(true);
                kartsArray[i].transform.rotation = spawnPoints[i].transform.rotation;
                kartsArray[i].GetComponent<AIKart>().number = 0;
                kartsArray[i].GetComponent<LapCount>().name = "CPU";

            }
            else if (karts[i] == "Player1")
            {
                GameObject kartObj = Instantiate<GameObject>(PlayerKart);
                PlayerKart pKart = kartObj.GetComponent<PlayerKart>();
                pKart.transform.position = spawnPoints[i].transform.position - new Vector3(0, -0.05f, 0);
                pKart.transform.rotation = spawnPoints[i].transform.rotation;
                pKart.SetDriver(controlScript.players[0].driver);
                pKart.SetKart(controlScript.players[0].kart);
                pKart.GetComponentInChildren<LapCount>().enabled = true;
                pKart.transform.GetChild(0).GetComponent<Respawn>().enabled = true;
                Debug.Log("Player 1 Respawn Enabled");
                pKart.GetComponentInChildren<Finished>().enabled = true;
                pKart.SetControlID(controlScript.players[0].controllerIndex);
                pKart.transform.GetChild(0).GetComponent<LapCount>().name = "P1";
                kartsArray.Add(pKart.gameObject);



                if (playerCount == 4)
                {
                    kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                }
                else if (playerCount == 3)
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
                GameObject kartObj = Instantiate<GameObject>(PlayerKart);
                PlayerKart pKart = kartObj.GetComponent<PlayerKart>();
                pKart.transform.position = spawnPoints[i].transform.position - new Vector3(0, -0.05f, 0);
                pKart.transform.rotation = spawnPoints[i].transform.rotation;
                pKart.SetDriver(controlScript.players[1].driver);
                pKart.SetKart(controlScript.players[1].kart);
                pKart.GetComponentInChildren<LapCount>().enabled = true;
                pKart.transform.GetChild(0).GetComponent<Respawn>().enabled = true;
                pKart.GetComponentInChildren<Finished>().enabled = true;
                pKart.SetControlID(controlScript.players[1].controllerIndex);
                pKart.transform.GetChild(0).GetComponent<LapCount>().name = "P2";
                kartsArray.Add(pKart.gameObject);

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
                GameObject kartObj = Instantiate<GameObject>(PlayerKart);
                PlayerKart pKart = kartObj.GetComponent<PlayerKart>();
                pKart.transform.position = spawnPoints[i].transform.position - new Vector3(0, -0.05f, 0);
                pKart.transform.rotation = spawnPoints[i].transform.rotation;
                pKart.SetDriver(controlScript.players[2].driver);
                pKart.SetKart(controlScript.players[2].kart);
                pKart.GetComponentInChildren<LapCount>().enabled = true;
                pKart.GetComponentInChildren<Respawn>().enabled = true;
                pKart.GetComponentInChildren<Finished>().enabled = true;
                pKart.SetControlID(controlScript.players[2].controllerIndex);
                pKart.transform.GetChild(0).GetComponent<LapCount>().name = "P3";
                kartsArray.Add(pKart.gameObject);

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
                GameObject kartObj = Instantiate<GameObject>(PlayerKart);
                PlayerKart pKart = kartObj.GetComponent<PlayerKart>();
                pKart.transform.position = spawnPoints[i].transform.position - new Vector3(0, -0.05f, 0);
                pKart.transform.rotation = spawnPoints[i].transform.rotation;
                pKart.SetDriver(controlScript.players[3].driver);
                pKart.SetKart(controlScript.players[3].kart);
                pKart.GetComponentInChildren<LapCount>().enabled = true;
                pKart.GetComponentInChildren<Respawn>().enabled = true;
                pKart.GetComponentInChildren<Finished>().enabled = true;
                pKart.SetControlID(controlScript.players[3].controllerIndex);
                pKart.transform.GetChild(0).GetComponent<LapCount>().name = "P4";
                kartsArray.Add(pKart.gameObject);

                if (playerCount == 4)
                {
                    kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (first == true)
        {
            first = false;
        }
    }
}
