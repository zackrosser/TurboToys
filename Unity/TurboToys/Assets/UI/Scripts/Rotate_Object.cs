using UnityEngine;
using System.Collections;

public class Rotate_Object : MonoBehaviour {

    public Vector3 rotateAxis;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotateAxis * Time.deltaTime);
	}
}
