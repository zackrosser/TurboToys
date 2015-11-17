using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class KartControls : MonoBehaviour
{

    public GameObject itemShell;
    public GameObject[] wheels;

    //public Text scoreText;

    public int playerID;

    public float fwd_accel = 100f;
    public float fwd_max_speed = 200f;
    public float brake_speed = 200f;
    public float turn_speed = 50f;
    public float m_hoverForce = 9.0f;
    public GameObject[] m_hoverPoints;

    public float hover_height = 3f;

    public float desiredHeight = 1.0f;
    public float spring = 100.0f;
    public float damp = 10.0f;

    public float m_forwardAcl = 100.0f;
    public float m_backwardAcl = 25.0f;

    public float m_turnStrength = 10f;

    public float yaw;
    public float gravitySpeed;
    public float jumpSpeed;
    private float current_speed;

    public float steeringAngle = 10.0f;
    public float driftingAngle = 20.0f;

    public float driftingSpeed = 100.0f;
    private bool drifting = false;
    private int driftingDirection = 0;
    private float driftYaw = 0;

    private InputDevice inputDevice;
    Vector3 normal = new Vector3(0, 0, 0);
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputDevice = InputManager.Devices[playerID];
    }

    // Update is called once per frame
    void Update()
    {

        //Turn/Spin wheels
        Vector3 newForward = Vector3.Cross(Vector3.left, normal);
        //wheels[0].transform.Rotate(wheels[0].transform.right, rb.velocity.magnitude, Space.World);
       // wheels[1].transform.Rotate(wheels[1].transform.right, rb.velocity.magnitude, Space.World);
        //wheels[2].transform.Rotate(wheels[2].transform.right, rb.velocity.magnitude, Space.World);
        //wheels[3].transform.Rotate(wheels[3].transform.right, rb.velocity.magnitude, Space.World);

        //wheels[0].transform.rot


        RaycastHit hit;

        //inputDevice = InputManager.Devices[playerID];
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, -hoverPoint.transform.up, out hit, hover_height * 2))
            {
                Debug.DrawLine(hoverPoint.transform.position, hit.point);
            }
        }

        if (inputDevice.Action2.WasPressed)
        {
            Instantiate(itemShell, transform.position + transform.forward, transform.rotation);
        }

        if (inputDevice.Action1.WasPressed)
        {
            if (Physics.Raycast(transform.position, -transform.up, out hit, hover_height * 1.2f))
            {
                rb.AddForce(transform.up * jumpSpeed);
                if (inputDevice.LeftStickX.Value >= 0.1) { drifting = true; driftingDirection = 1; }
                else if (inputDevice.LeftStickX.Value <= -0.1) { drifting = true; driftingDirection = -1; }
            }
        }
        else if (inputDevice.Action1.WasReleased)
        {
            drifting = false;
            driftingDirection = 0;
            driftYaw = 0;
        }
        if (inputDevice.RightTrigger.Value >= 0.1)
        {
            //if (Physics.Raycast(transform.position, -transform.up, out hit, hover_height * 5f))
            {
                //if(yaw > 0.1)
                //{
                //    rb.AddForce(transform.forward * (yaw));
                //}else if (yaw < -0.1)
                //{

                //    float newLeftAngle = 360 - inputDevice.LeftStick.Angle;
                //    if (newLeftAngle == 360)
                //    {
                //        newLeftAngle = 0;
                //    }


                //    //This is the force and direction of velocity // REFERENCE VALUE 0, TRUE NORTH
                //    // InControl.InputManager.ActiveDevice.LeftStick.Angle == Rotation from True North, 
                //    // if I rotate The force vector by the LeftStick.Angle, this will give me a SideB vector, I use this to Cross TrueNorth and SideB 
                //    Vector3 newForward = Vector3.Cross(Vector3.left,normal);
                //    Vector3 newUp = Vector3.Cross(Vector3.left,newForward);
                //    Vector3 nb_vA = transform.forward * (fwd_accel * inputDevice.RightTrigger.Value * Time.deltaTime);
                //    GameObject nbVecA = new GameObject();


                //    Quaternion rotation = Quaternion.Euler(0, newLeftAngle, 0);
                //    Vector3 myVector = Vector3.one;
                //    Vector3 rotateVector = rotation * myVector;


                //    nbVecA.transform.position = new Vector3(nb_vA.x, nb_vA.y, nb_vA.z);
                //    Debug.Log("VECATRANSPRE ROT: " + nbVecA.transform.rotation.x + "nb_VBy: " + nbVecA.transform.position.y + "nb_VBz: " + nbVecA.transform.position.z);
                //    Debug.Log("VECATRANSPRE ROT: " + nbVecA.transform.position.x + "nb_VBy: " + nbVecA.transform.position.y + "nb_VBz: " + nbVecA.transform.position.z);
                //    nbVecA.transform.rotation = Quaternion.AngleAxis(-30, Vector3.up);// = Quaternion.AngleAxis(newUp, newLeftAngle);  //nbVecA.transform.Rotate(newUp, newLeftAngle,Space.Self); nbVecA.transform.Rotate(newUp, newLeftAngle,Space.World);





                //    Debug.Log("VECATRANSPOSTROT: " + nbVecA.transform.position.x + "nb_VBy: " + nbVecA.transform.position.y + "nb_VBz: " + nbVecA.transform.position.z + "FTEROTATIONFTEROTATIONFTEROTATIONFTEROTATIONAFTEROTATION");
                //    Debug.Log("VECATRANSPOSTROT: " + nbVecA.transform.rotation.x + "nb_VBy: " + nbVecA.transform.position.y + "nb_VBz: " + nbVecA.transform.position.z + "FTEROTATIONFTEROTATIONFTEROTATIONFTEROTATIONAFTEROTATION");
                //    Debug.Log("newUp: " + newUp);
                //    Debug.Log("LS Angle: " + newLeftAngle);
                //    Vector3 nb_vB = new Vector3(nbVecA.transform.position.x, nbVecA.transform.position.y, nbVecA.transform.position.z);//Vector3 nb_vB = new Vector3(nbVecA.transform.position.x,nbVecA.transform.position.y,nbVecA.transform.position.z);

                //    Debug.Log("nb_VBx: " + nb_vB.x + "nb_VBy: " + nb_vB.y + "nb_VBz: " + nb_vB.z);
                //    Vector3 nb_Centripital = Vector3.Cross(nb_vA, nb_vB);
                //    nb_Centripital.Normalize();
                //    Debug.Log("nbVECATrans: "+ nbVecA.transform.position);
                //    Debug.Log("nb_VA: " + nb_vA);
                //    Debug.Log("nb_VB: " + nb_vB);
                //    Debug.Log("nb_cent: " + nb_Centripital);
                //    Debug.Log(nb_Centripital * (fwd_accel * inputDevice.RightTrigger.Value * Time.deltaTime));
                //    rb.AddForce(nb_Centripital * (fwd_accel * inputDevice.RightTrigger.Value * Time.deltaTime));
                //    //Find perpendicular centripital force,which is  90 degrees to  the direction of nb_VA
                //    //var nb_XB = Quaternion.ro
                //        //InControl.InputManager.ActiveDevice.LeftStick.Angle
                //   // Vector3 norman = transform.forward * (fwd_accel * inputDevice.RightTrigger.Value * Time.deltaTime);
                //    //Vector3.Cross(norman,transform.forward);
                //}

                rb.AddForce(transform.forward * (fwd_accel * inputDevice.RightTrigger.Value * Time.deltaTime));
                //current_speed += (current_speed >= fwd_max_speed) ? 0f : fwd_accel * inputDevice.RightTrigger.Value * Time.deltaTime;
                //current_speed = fwd_accel * inputDevice.RightTrigger.Value * Time.deltaTime;
            }
        }
        else if (inputDevice.LeftTrigger.Value >= 0.1)
        {
            rb.AddForce(transform.forward * (-fwd_accel / 2 * 1f * Time.deltaTime));
            //current_speed -= (current_speed <= -(fwd_max_speed/3)) ? 0f : fwd_accel*2 * inputDevice.LeftTrigger.Value * Time.deltaTime;
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
                yaw += -0.04f * -inputDevice.LeftStickX.Value;
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
                yaw += 0.04f * inputDevice.LeftStickX.Value;
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
        if (driftYaw >= 1.2f)
        {
            driftYaw = 1.2f;
        }
        else if (driftYaw <= -1.2f)
        {
            driftYaw = -1.2f;
        }
        if (yaw >= 1.3f)
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
        Vector3 velocity = rb.velocity;
        Vector3 forwardsVelocity = Vector3.Dot(transform.forward, velocity) * transform.forward;
        Vector3 sidewaysVelocity = Vector3.Dot(transform.right, velocity) * transform.right;
        sidewaysVelocity *= 0.95f;
        velocity = sidewaysVelocity + forwardsVelocity;
        rb.velocity = velocity;

        RaycastHit hit;
        normal = new Vector3(0, 0, 0);
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
                rb.AddTorque(transform.up * yaw * steeringAngle, ForceMode.Acceleration);
            }
            else if (drifting)
            {
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
            drifting = false;
        }
        rb.drag = (1 / (1 + (rb.velocity.magnitude * rb.velocity.magnitude) / fwd_accel));

        //Debug.Log(rb.velocity.x);
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
                //rb.AddForce (transform.forward * (current_speed * 1.1f * Time.deltaTime));
            }
            else
            {
                //rb.AddForce (transform.forward * (current_speed * Time.deltaTime));
            }
        }
    }
}