using UnityEngine;
using System.Collections;

public class Rotate_Object : MonoBehaviour {

    public Vector3 rotateAxis;
    public bool stop;


	// Update is called once per frame
	void Update () {
        if (!stop)
        {
            transform.Rotate(rotateAxis * Time.deltaTime);
        }
	}
}
