using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

    public LapCount lapScript;
    private bool onGround = false;
    public float timer = 0;
    public float currentTime = 0;

	// Use this for initialization
	void Start () 
    {
        lapScript = gameObject.GetComponent<LapCount>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(!onGround)
        {
            currentTime += Time.deltaTime * 1;
        }else
        {
            currentTime = 0;
        }
        if(currentTime >= timer)
        {
            transform.position = lapScript.waypoint[lapScript.currentWaypoint].transform.position + lapScript.waypoint[lapScript.currentWaypoint + 1].transform.up*0.5f;
            transform.rotation = lapScript.waypoint[lapScript.currentWaypoint].transform.rotation;
        }
	}

    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, -transform.up, 5))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }
}
