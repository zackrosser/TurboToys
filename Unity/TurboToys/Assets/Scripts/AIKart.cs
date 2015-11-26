using UnityEngine;
using System.Collections;

public class AIKart : MonoBehaviour
{
    public GameObject waypointControl;
    public GameObject[] waypoints = new GameObject[105];
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
    public float breakSpeed = 10f;
    public float fwd_accel = 5f;
    public float angle = 0;
    public float maxSpeed = 800f;

    public int number;

    private float minDistance = 10f;
    Vector3 normal = new Vector3(0, 0, 0);
    public GameObject[] wheels;

    public int point = 0;
    private GameObject targetWaypoint;
    private bool first = true;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        current_speed = 0;
        waypointControl = GameObject.FindGameObjectWithTag("Waypoints");
        //Select Random Driver
        int rand = Random.Range(0, 3);
        switch(rand)
        {
            case 0:
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                break;
            case 1:
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                break;
            case 2:
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                break;
            case 3:
                //transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newForward = Vector3.Cross(Vector3.left, normal);
        wheels[0].transform.Rotate(wheels[0].transform.right, rb.velocity.magnitude, Space.World);
        wheels[1].transform.Rotate(wheels[1].transform.right, rb.velocity.magnitude, Space.World);
        wheels[2].transform.Rotate(wheels[2].transform.right, rb.velocity.magnitude, Space.World);
        wheels[3].transform.Rotate(wheels[3].transform.right, rb.velocity.magnitude, Space.World);
        if (first)
        {
            waypoints = waypointControl.GetComponent<Waypoints>().waypoints;
            targetWaypoint = waypoints[0];
            first = false;
        }
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
        Debug.DrawLine(transform.position, targetWaypoint.transform.position, Color.red);
    }

    void FixedUpdate()
    {
       

        Vector3 velocity = rb.velocity;
        Vector3 forwardsVelocity = Vector3.Dot(transform.forward, velocity) * transform.forward;
        Vector3 sidewaysVelocity = Vector3.Dot(transform.right, velocity) * transform.right;
        sidewaysVelocity *= 0.95f;
        velocity = sidewaysVelocity + forwardsVelocity;
        rb.velocity = velocity;

        RaycastHit hit2;
        if (Physics.Raycast(transform.position, transform.forward, out hit2, 5))
        {
            if (hit2.collider.gameObject.tag != "Boost")
            {
                current_speed = breakSpeed;
            }
        }
        else
        {
            current_speed = fwd_accel;
        }


        if (Vector3.Distance(transform.position, targetWaypoint.transform.position) <= minDistance)
        {
            minDistance = Random.RandomRange(12f, 16f);
            point++;
            if (point > waypoints.Length)
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
        float Rand = Random.RandomRange(0.5f, 1.5f);

        Vector3 targetDirection = (targetWaypoint.transform.position - transform.position).normalized; //Vector3.ProjectOnPlane(, normal);        
        Vector3 forward = transform.forward;
        Plane plane = new Plane(Vector3.zero, transform.right, forward);


        float angle2 = Vector3.Angle(targetDirection, forward);
        Vector3 up = Vector3.Cross(targetDirection, forward);

        float planeDistance = plane.GetDistanceToPoint(up);

        float newAngle = Mathf.Sign(-planeDistance) * angle2;
        angle = angle2;
        if (newAngle >= Random.RandomRange(4f, 20f))
        {
            yaw = -Random.RandomRange(4f, 6f);
        }
        else if (newAngle <= -Random.RandomRange(4f, 20f))
        {
            yaw = Random.RandomRange(4f, 6f);
        }
        else
        {
            yaw = 0;
        }
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
        rb.drag = (1 / (1 + (rb.velocity.magnitude * rb.velocity.magnitude) / fwd_accel));
        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
        Vector3 worldVel = transform.TransformDirection(localVel);
        Debug.DrawLine(transform.position, worldVel + transform.position, Color.red);
        Debug.DrawLine(transform.position, transform.right + transform.position, Color.green);

        //Limit top speed
        //if (rb.velocity.magnitude < 30)
        {
            if (Physics.Raycast(transform.position, -transform.up, hover_height * 2))
            {
                if (drifting)
                {
                    rb.AddForce(transform.forward * (current_speed * 1.1f * Time.deltaTime));
                }
                else
                {
                    rb.AddForce(transform.forward * (current_speed * Rand * Time.deltaTime));
                }
            }
        }
    }
}
