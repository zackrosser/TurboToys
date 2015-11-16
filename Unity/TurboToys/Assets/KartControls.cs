using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class KartControls : MonoBehaviour {

    //public Text scoreText;

    public int playerID;

	public Transform modelTransform;

	public float fwd_accel = 100f;
	public float fwd_max_speed = 200f;
	public float brake_speed = 200f;
	public float turn_speed = 50f;
    public float m_hoverForce = 9.0f;
    public GameObject[] m_hoverPoints;

    public float hover_height = 3f;     //Distance to keep from the ground
    public float height_smooth = 10f;   //How fast the ship will readjust to "hover_height"
    public float pitch_smooth = 5f;   

    public float desiredHeight = 1.0f;
    public float spring = 100.0f;
    public float damp = 10.0f;
	
	public float m_forwardAcl = 100.0f;
	public float m_backwardAcl = 25.0f;
	
	public float m_turnStrength = 10f;

    public float yaw;
    public float gravitySpeed;
    public float jumpSpeed;
    private float smooth_y;
    private float current_speed;

    private float backGravSpeed;
    public float steeringAngle = 10.0f;
    public float driftingAngle = 20.0f;

    public float driftingSpeed = 100.0f;
    private bool drifting = false;
    private int driftingDirection = 0;
    private float driftYaw = 0;

    private InputDevice inputDevice;

    Rigidbody rb;
	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody>();
        backGravSpeed = gravitySpeed;
		modelTransform = this.gameObject.transform.GetChild (0).transform;
        inputDevice = InputManager.Devices[playerID];
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        
        //inputDevice = InputManager.Devices[playerID];
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints [i];
			if (Physics.Raycast(hoverPoint.transform.position, -hoverPoint.transform.up, out hit, hover_height*2))
            {
                
                Debug.DrawLine(hoverPoint.transform.position,hit.point);
            }
        }

        if (inputDevice.Action1.WasPressed)
        {
            if (Physics.Raycast(transform.position, -transform.up, out hit, hover_height * 1.2f))
            {
                rb.AddForce(transform.up * jumpSpeed);
                if (inputDevice.LeftStickX.Value >= 0.1) { drifting = true; driftingDirection = 1; }
                else if (inputDevice.LeftStickX.Value <= -0.1) { drifting = true; driftingDirection = -1; }
            }
        }else if(inputDevice.Action1.WasReleased)
        {
            gravitySpeed = backGravSpeed;
            drifting = false;
            driftingDirection = 0;
            driftYaw = 0;
        }
		if (inputDevice.RightTrigger.Value >= 0.1)
		{
			if (Physics.Raycast(transform.position, -transform.up, out hit, hover_height * 1.2f))
			{
                current_speed += (current_speed >= fwd_max_speed) ? 0f : fwd_accel * inputDevice.RightTrigger.Value * Time.deltaTime;
			}
        }else if (inputDevice.LeftTrigger.Value >= 0.1)
        {
            current_speed -= (current_speed <= -(fwd_max_speed/3)) ? 0f : fwd_accel*2 * inputDevice.LeftTrigger.Value * Time.deltaTime;
        }
		else
		{
			if (current_speed > 100)
			{
				current_speed -= brake_speed * Time.deltaTime;
			}
            else if (current_speed < -100)
            {
                current_speed += brake_speed * Time.deltaTime;
            }
			else
			{
				current_speed = 0f;
			}
		}
        if (inputDevice.LeftStickX.Value <= -0.1)
        {
            if (drifting)
            {
                if (driftingDirection == -1)
                {
                    driftYaw += 0.05f * -inputDevice.LeftStickX.Value;
                }
                else 
                {
                    if (driftYaw < 0)
                    {
                        driftYaw += 0.05f * -inputDevice.LeftStickX.Value;
                    }
                }
            }
            else
            {
                yaw += -0.05f * -inputDevice.LeftStickX.Value;
            }
        }
        else if (inputDevice.LeftStickX.Value >= 0.1)
        {
            if (drifting)
            {
                if (driftingDirection == 1)
                {
                    driftYaw -= 0.05f * inputDevice.LeftStickX.Value;
                }
                else
                {
                    if (driftYaw > 0)
                    {
                        driftYaw -= 0.05f * inputDevice.LeftStickX.Value;
                    }
                }
            }
            else
            {
                yaw += 0.05f * inputDevice.LeftStickX.Value;
            }
        }
        else 
        {
            if (yaw > 0.1) 
            {
                yaw -= 0.1f;
            }
            else if (yaw < -0.1)
            {
                yaw += 0.1f;
            }
            else
            {
                yaw = 0;
            }
            if (driftYaw > 0)
            {
                driftYaw += 0.005f;
            }
            if (driftYaw < 0)
            {
                driftYaw -= 0.005f;
            }
        }
        if(driftYaw >= 1.2f)
        {
            driftYaw = 1.2f;
        }else if (driftYaw <= -1.2f)
        {
            driftYaw = -1.2f;
        }
        if(yaw >= 1.3f)
        {
            yaw = 1.3f;
        }
        else if (yaw <= -1.3f)
        {
            yaw = -1.3f;
        }
        
	}  

    void FixedUpdate()
    {
        RaycastHit hit;
		Vector3 normal = new Vector3(0,0,0);
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints[i];
			if (Physics.Raycast(hoverPoint.transform.position, -hoverPoint.transform.up, out hit, hover_height*2f))
            {
				normal += hit.normal;
                Vector3 wheelVel = rb.GetRelativePointVelocity(hoverPoint.transform.localPosition);
                float dot = Vector3.Dot(hit.normal,wheelVel);
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
        if(Physics.Raycast(transform.position, -transform.up, hover_height*2))
        {
            Vector3 newForward = Vector3.Cross(Vector3.left,normal);
            Vector3 newUp = Vector3.Cross(Vector3.left,newForward);
            Quaternion.LookRotation(newForward,newUp);
            transform.Rotate(Vector3.up, 0, Space.Self);
            if (!drifting)
            {
                //scoreText.text = "Not Drifting";
                rb.AddTorque(transform.up * yaw * steeringAngle, ForceMode.Acceleration);
            }else if(drifting)
            {
                //scoreText.text = "Drifting";
                if(driftingDirection == 1)
                {
                    // Drifting right, add some velocity from the left 
                    rb.velocity += -transform.right * driftingSpeed;
                    rb.AddTorque(transform.up * -driftYaw *  driftingAngle, ForceMode.Acceleration);

                }
                else if (driftingDirection == -1)
                {
                    // Drifting left, add some velocity from the right
                    rb.velocity += transform.right * driftingSpeed;
                    rb.AddTorque(transform.up * -driftYaw *  driftingAngle, ForceMode.Acceleration);
                }
            }
            
        }
        else{
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
		if (Physics.Raycast (transform.position, -transform.up, hover_height * 2)) {
			if (drifting) {
				rb.AddForce (transform.forward * (current_speed * 1.1f * Time.deltaTime));
			} else {
				rb.AddForce (transform.forward * (current_speed * Time.deltaTime));
			}
		}
    }
}