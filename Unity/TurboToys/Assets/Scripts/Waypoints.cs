using UnityEngine;
using System.Collections;

public class Waypoints : MonoBehaviour {

    public GameObject[] waypoints;
    public int zero = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            if(i == waypoints.Length - 1)
            {
                Debug.DrawLine(waypoints[i].transform.position, waypoints[0].transform.position);
            }
            else 
            {
                Debug.DrawLine(waypoints[i].transform.position, waypoints[i + 1].transform.position);
            }
        }
	}

    void FixxedUpdate()
    {

    }
}
