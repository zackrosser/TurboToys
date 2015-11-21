using UnityEngine;
using System.Collections.Generic;

public class SpawnPoints : MonoBehaviour {

    private GameObject PlayerKart;   //Ed test

    private List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject player;
    public GameObject enemy;
    public List<string> karts = new List<string>();

    private List<GameObject> kartsArray = new List<GameObject>();

    private int playerCount = 0;

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
                    kartsArray.Add(Instantiate(enemy, spawnPoints[i].transform.position - new Vector3(0, -0.5f, 0), Quaternion.identity) as GameObject);
                    kartsArray[i].SetActive(true);

                }else if(karts[i] == "Player1")
                {
                    GameObject kartObj = Instantiate<GameObject>(PlayerKart);
                    PlayerKart pKart = kartObj.GetComponent<PlayerKart>();
                    pKart.transform.position = spawnPoints[i].transform.position - new Vector3(-2, 0, -1);
                    pKart.SetDriver(controlScript.players[0].driver);
                    pKart.SetKart(controlScript.players[0].kart);
                    pKart.SetControlID(controlScript.players[0].controllerIndex);
                    kartsArray.Add(pKart.gameObject);
                    


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
                    GameObject kartObj = Instantiate<GameObject>(PlayerKart);
                    PlayerKart pKart = kartObj.GetComponent<PlayerKart>();
                    pKart.transform.position = spawnPoints[i].transform.position - new Vector3(-2, 0, -1);
                    pKart.SetDriver(controlScript.players[1].driver);
                    pKart.SetKart(controlScript.players[1].kart);
                    pKart.SetControlID(controlScript.players[1].controllerIndex);
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
                    pKart.transform.position = spawnPoints[i].transform.position - new Vector3(-2, 0, -1);
                    pKart.SetDriver(controlScript.players[2].driver);
                    pKart.SetKart(controlScript.players[2].kart);
                    pKart.SetControlID(controlScript.players[2].controllerIndex);
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
                    pKart.transform.position = spawnPoints[i].transform.position - new Vector3(-2, 0, -1);
                    pKart.SetDriver(controlScript.players[3].driver);
                    pKart.SetKart(controlScript.players[3].kart);
                    pKart.SetControlID(controlScript.players[3].controllerIndex);
                    kartsArray.Add(pKart.gameObject);

                    if (playerCount == 4)
                    {
                        kartsArray[i].gameObject.transform.GetChild(0).GetComponent<KartControls>().playerCam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                    }
                }
            }
        }
    }
}
