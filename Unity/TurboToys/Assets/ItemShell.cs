using UnityEngine;
using System.Collections;

public class ItemShell : MonoBehaviour {

    public float m_hoverForce = 9.0f;
    public float hover_height = 3f;
    public float damp = 0.01f;
    public float gravitySpeed = 1000;

    public GameObject[] m_hoverPoints;

    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * (2000 * Time.deltaTime));
    }

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT");
		Vector3 myCollisionNormal = collision.contacts[0].normal;
		Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
		rb.AddForce(myCollisionNormal*(5),ForceMode.VelocityChange);
    }


    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 normal = new Vector3(0, 0, 0);
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, -hoverPoint.transform.up, out hit, hover_height * 4f))
            {
                normal += hit.normal;
                Vector3 wheelVel = rb.GetRelativePointVelocity(hoverPoint.transform.localPosition);
                float dot = Vector3.Dot(hit.normal, wheelVel);
                rb.AddForceAtPosition(transform.up * (m_hoverForce * (1.0f - (hit.distance / hover_height)) - dot * damp), hoverPoint.transform.position);
                rb.AddForceAtPosition(-transform.up * (Time.deltaTime * 90.8f), hoverPoint.transform.position);
            }
            else
            {
                    normal += -transform.up;
                    rb.AddForceAtPosition(Vector3.down * (Time.deltaTime * gravitySpeed), hoverPoint.transform.position);
            }
        }
        normal.Normalize();
        if (Physics.Raycast(transform.position, -transform.up, hover_height * 4))
        {
            Vector3 newForward = Vector3.Cross(Vector3.left, normal);
            Vector3 newUp = Vector3.Cross(Vector3.left, newForward);
            Quaternion.LookRotation(newForward, newUp);
            //transform.Rotate(Vector3.up, 0, Space.Self);
            //rb.AddTorque(transform.up * yaw * steeringAngle, ForceMode.Acceleration);
        }
        else
        {
            //Quaternion tilt = Quaternion.FromToRotation(transform.up, Vector3.up);
            //drifting = false;
            /*Now we apply it to the ship with the quaternion product property*/
            //transform.rotation = tilt * transform.rotation;
        }
        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
		//Vector3 newDir = Vector3.RotateTowards(transform.forward, new Vector3(localVel.x, transform.rotation.y,transform.rotation.z),0.1f,0.0f);
		//Debug.DrawRay(transform.position, newDir, Color.red);
		//transform.rotation = Quaternion.LookRotation(newDir);
        
		//Quaternion.RotateTowards(transform.); new Vector3(localVel.x, transform.rotation.y,transform.rotation.z);
        rb.AddForce(transform.forward * (150 * Time.deltaTime));
        transform.Rotate(Vector3.up, localVel.x * 2, Space.Self);
    }
}
