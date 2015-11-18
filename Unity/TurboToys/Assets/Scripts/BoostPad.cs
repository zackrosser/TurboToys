using UnityEngine;
using System.Collections;

public class BoostPad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(transform.forward*5, ForceMode.VelocityChange);
    }
}
