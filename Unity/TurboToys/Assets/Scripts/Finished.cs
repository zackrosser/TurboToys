using UnityEngine;
using System.Collections;

public class Finished : MonoBehaviour {

    public LapCount lapScript;
    public KartControls kartControls;
    public AIKart aiKart;

	// Use this for initialization
	void Start () {
        lapScript = gameObject.GetComponent<LapCount>();
        kartControls = gameObject.GetComponent<KartControls>();
        aiKart = gameObject.GetComponent<AIKart>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(lapScript.lapCount >= 3)
        {
            kartControls.enabled = false;
            aiKart.enabled = true;
        }
	}
}
