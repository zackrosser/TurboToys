using UnityEngine;
using System.Collections.Generic;

public class LapCount : MonoBehaviour {

    public int lapCount = 0;
    public GameObject waypoints;
    public List<GameObject> waypoint = new List<GameObject>();
    public string name = "";

    public int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
        waypoints = GameObject.Find("Waypoints");
	    for(int i = 0; i < waypoints.transform.childCount; i++)
        {
            waypoint.Add(waypoints.transform.GetChild(i).gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(Vector3.Distance(transform.position, waypoint[currentWaypoint].transform.position ) < 20)
        {
            currentWaypoint++;
        }
        if(currentWaypoint >= waypoints.transform.childCount)
        {
            currentWaypoint = 0;
            lapCount++;
        }
	}
}
