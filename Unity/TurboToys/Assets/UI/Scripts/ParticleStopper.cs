using UnityEngine;
using System.Collections;

public class ParticleStopper : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().Stop();
	}
	    
}
