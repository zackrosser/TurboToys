using UnityEngine;
using System.Collections;

public class KartActive : MonoBehaviour {

    public bool kartOn = false;
    public bool playerKart = false;
    public float startTime = 3;
    private float timer = 0;
	// Use this for initialization
    private bool first = true;
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (first)
        {
            if (timer >= startTime)
            {
                kartOn = true;
                first = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        if (playerKart)
        {
            if (kartOn)
            {
                transform.GetComponentInChildren<KartControls>().enabled = true;
            }
            else
            {
                transform.GetComponentInChildren<KartControls>().enabled = false;
                transform.GetComponentInChildren<Respawn>().enabled = false;
            }
        }else
        {
            if (kartOn)
            {
                transform.GetComponent<AIKart>().enabled = true;
            }
            else
            {
                transform.GetComponent<AIKart>().enabled = false;
                transform.GetComponent<Respawn>().enabled = false;
            }
        }
	}
}
