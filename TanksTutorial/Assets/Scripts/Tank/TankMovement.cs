using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TankMovement : MonoBehaviour
{
    public float m_Speed = 10f;                 //How fast the tank moves forward and back
    public float m_RotationSpeed = 180f;        //How fast the tank turen in degrees per second

    private Rigidbody m_Rigidbody;

    public float m_ForwardInputValue;           //The current value of the movement input
    public float m_TurnInputValue;              //The current value of the turn input

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // When the tank is turned on, make sure it is not kinematic
        m_Rigidbody.isKinematic = false;

        // also reset the input values
        m_ForwardInputValue = 0;
        m_TurnInputValue = 0;
    }

    private void OnDisable()
    {
        //When the tank is turned off, set it to kinematic so it stops moving
        m_Rigidbody.isKinematic = true;
    }

    private void Update()
    {
        m_ForwardInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");
    }

     private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude
        // Based on the input, speed and time between frames
        Vector3 wantedVelocity = transform.forward * m_ForwardInputValue * m_Speed;

        // Apply the wantedVelocity minus the current rigidbody velocity to apply a change
        // in the velocity on the tank.
        // This ignores the mass of the tank
        m_Rigidbody.AddForce(wantedVelocity - m_Rigidbody.velocity, ForceMode.VelocityChange);
    }

    private void Turn()
    {
        // Determing the number of degrees to be turned based on the input.
        // Speed and time between frames
        float turnValue = m_TurnInputValue * m_RotationSpeed * Time.deltaTime;

        // Make this into a rotation around the y-axis
        Quaternion turnRotation = Quaternion.Euler(0f, turnValue, 0f);

        // Apply this rotation to the rigidbody's rotation
        m_Rigidbody.MoveRotation(transform.rotation * turnRotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ammo")
        {
            Destroy(other.gameObject);


        }
    }
}
