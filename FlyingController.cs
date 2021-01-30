using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    GestureController gestureController;
    WingsAudioController wingsAudioController;

    [Header("Environment Settings")]
    [SerializeField] float originalGravity = -9.81f;
    [SerializeField] float inAirGravity = -2f;
    [SerializeField] float airResistance = 0.1f;

    [Header("Player Attributes")]
    [SerializeField] float maxSpeed = 16f;
    [SerializeField] float maxUpwardSpeed = 7f;
    [SerializeField] float takeoffAmount = 700f;
    [SerializeField] float forwardAmount = 600f;
    [SerializeField] float upwardAmount = 400f;
    [SerializeField] float landAmount = 5f;

    Rigidbody rb;
    public static bool isFlying = false;
    bool isLanding = false;
    public static FlyingController _instance;
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        gestureController = GetComponent<GestureController>();
        wingsAudioController = GetComponent<WingsAudioController>();

        /* Physics Setup */
        rb = GetComponent<Rigidbody>();
        rb.drag = airResistance;
        Physics.gravity = new Vector3(0, 1, 0) * originalGravity;
    }


    void Update()
    {
        if (isFlying)
        {
            if (gestureController.Input_land())
            {
                Land();
            }
            else if (gestureController.Input_Fly())
            {
                ManualFly();
            }
        } 
        else if (gestureController.Input_Fly())
        {
            Takeoff();
        }
    }

    void Takeoff()
    {
        Physics.gravity = new Vector3(0, 1, 0) * inAirGravity;
        rb.AddForce(transform.up * takeoffAmount);
        rb.AddForce(Vector3.Normalize(Camera.main.transform.forward) * forwardAmount/3f);
        wingsAudioController.Play();
        isFlying = true;
    }

    void ManualFly()
    {
        if (isLanding)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 1);
            isLanding = false;
        }
        Physics.gravity = new Vector3(0, 1, 0) * inAirGravity;
        rb.AddForce(transform.up * upwardAmount);
        rb.AddForce(Vector3.Normalize(Camera.main.transform.forward) * forwardAmount);
        wingsAudioController.Play();
        print("B: " + rb.velocity);
        if (rb.velocity.y > maxUpwardSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxUpwardSpeed, rb.velocity.z);
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        print("A: " + rb.velocity);
    }
    
    public void Land()
    {
        isLanding = true;
        Physics.gravity = new Vector3(0, 1, 0) * originalGravity;
        rb.velocity = Vector3.down * landAmount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Land")
        {
            isFlying = false;
        }
    }
}
