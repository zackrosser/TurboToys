using UnityEngine;
using System.Collections;

public class AIKart : MonoBehaviour {

    public GameObject waypointControl;
    public GameObject[] waypoints;
    public GameObject[] m_hoverPoints;
    public float m_hoverForce = 9.0f;
    public float hover_height = 3f;
    public float driftingSpeed = 100.0f;
    private bool drifting = false;
    private int driftingDirection = 0;
    private float driftYaw = 0;
    public float yaw;
    public float steeringAngle = 10.0f;
    public float driftingAngle = 20.0f;
    private float current_speed;
    public float gravitySpeed;
    public float jumpSpeed;
    public float damp = 10.0f;
    public float angle = 0;

    public int point = 0;
    private GameObject targetWaypoint;

    Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        waypoints = waypointControl.GetComponent<Waypoints>().waypoints;
        targetWaypoint = waypoints[0];
        current_speed = 100;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        //inputDevice = InputManager.Devices[playerID];
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, -hoverPoint.transform.up, out hit, hover_height * 1))
            {

                Debug.DrawLine(hoverPoint.transform.position, hit.point);
            }
        }
        Debug.DrawLine(transform.position, targetWaypoint.transform.position,Color.red);
    }

    void FixedUpdate()
    {
        Vector3 referenceRight = Vector3.Cross(Vector3.up, (transform.forward).normalized);
        float angle2 = Vector3.Angle(targetWaypoint.transform.position, (transform.forward).normalized);
        float sign = Mathf.Sign(Vector3.Dot(targetWaypoint.transform.position, referenceRight));
        float finalAngle = sign * angle2;
        angle = finalAngle;// Vector3.Angle(targetWaypoint.transform.position, transform.forward);
        if (angle >= 180) { angle -= 360; }
        float rightAngle = Vector3.Angle(transform.right + new Vector3(1,0,0), targetWaypoint.transform.position);
        float leftAngle = Vector3.Angle(-transform.right + new Vector3(-1, 0, 0), targetWaypoint.transform.position);
        if (angle>=5)
        {
            yaw = 1f;
        }else if(angle<=-5)
        {
            yaw = -1f;
        }
        else
        {
            yaw = 0;
        }
        if(Vector3.Distance(transform.position, targetWaypoint.transform.position) <= 10)
        {
            point++;
            if(point > waypoints.Length)
            {
                point = 0;
            }
            targetWaypoint = waypoints[point];
        }
        RaycastHit hit;
        Vector3 normal = new Vector3(0, 0, 0);
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, -hoverPoint.transform.up, out hit, hover_height * 2f))
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
        if (Physics.Raycast(transform.position, -transform.up, hover_height * 2))
        {
            Vector3 newForward = Vector3.Cross(Vector3.left, normal);
            Vector3 newUp = Vector3.Cross(Vector3.left, newForward);
            Quaternion.LookRotation(newForward, newUp);
            transform.Rotate(Vector3.up, 0, Space.Self);
            if (!drifting)
            {
                //scoreText.text = "Not Drifting";
                rb.AddTorque(transform.up * yaw * steeringAngle, ForceMode.Acceleration);
            }
            else if (drifting)
            {
                //scoreText.text = "Drifting";
                if (driftingDirection == 1)
                {
                    // Drifting right, add some velocity from the left 
                    rb.velocity += -transform.right * driftingSpeed;
                    rb.AddTorque(transform.up * -driftYaw * driftingAngle, ForceMode.Acceleration);

                }
                else if (driftingDirection == -1)
                {
                    // Drifting left, add some velocity from the right
                    rb.velocity += transform.right * driftingSpeed;
                    rb.AddTorque(transform.up * -driftYaw * driftingAngle, ForceMode.Acceleration);
                }
            }

        }
        else
        {
            //Quaternion tilt = Quaternion.FromToRotation(transform.up, Vector3.up);
            drifting = false;
            /*Now we apply it to the ship with the quaternion product property*/
            //transform.rotation = tilt * transform.rotation;
        }
        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
        Vector3 worldVel = transform.TransformDirection(localVel);
        Debug.DrawLine(transform.position, worldVel + transform.position, Color.red);
        Debug.DrawLine(transform.position, transform.right + transform.position, Color.green);
        //float drift =  Vector3.Dot(worldVel , transform.right);
        //int d = (int)drift;
        //scoreText.text = d.ToString();
        //scoreText.text = current_speed.ToString();
        if (Physics.Raycast(transform.position, -transform.up, hover_height * 2))
        {
            if (drifting)
            {
                rb.AddForce(transform.forward * (current_speed * 1.1f * Time.deltaTime));
            }
            else
            {
                rb.AddForce(transform.forward * (current_speed * Time.deltaTime));
            }
        }
    }
}
