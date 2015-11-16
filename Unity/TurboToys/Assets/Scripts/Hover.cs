using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {

	// Use this for initialization
    private Rigidbody rigidbody;
    private float current_speed;

    public float fwd_accel = 100f;
    public float fwd_max_speed = 200f;
    public float brake_speed = 200f;
    public float turn_speed = 50f;
    public float gravitySpeed = 1;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.3f, -transform.up, out hit, 5))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.deltaTime * 5.0f);
        }
        rigidbody.AddForce(-transform.up * Time.deltaTime * gravitySpeed);
	}
}
