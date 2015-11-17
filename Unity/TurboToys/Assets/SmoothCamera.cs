using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour {
    public GameObject kart;
    public float damp=2.0f;
    private Transform target;

    void Start()
    {
        target = kart.GetComponent<Transform>();
    }
    void Update () {
        var currentPos=transform.rotation * Vector3.forward * 10;
   
        var targetPos=Vector3.zero;
        if(target) targetPos=target.position;
        var rotation=transform.rotation;
        transform.LookAt(targetPos);
   
        var lookPos=transform.rotation * Vector3.forward * 10;
   
        transform.rotation=Quaternion.Lerp(rotation, transform.rotation, damp * Time.deltaTime);
   
        var actualPos=transform.rotation * Vector3.forward * 10;
   
        Debug.DrawLine (transform.position, transform.position + currentPos, Color.red);
        Debug.DrawLine (transform.position, transform.position + actualPos, Color.yellow);
        Debug.DrawLine (transform.position, transform.position + lookPos, Color.green);
   
        // currentPos is the initial rotation
        // actualPos is the rotation lerped
        // lookPos is the rotation based on where it wants to be
    }
}
