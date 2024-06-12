using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    // Prefab of the Shell
    public Rigidbody m_Shell;
    // A child of the tank where the shells are spawned
    public Transform m_FireTransform;
    // The force given to the shell when firing
    public float m_LaunchForce = 30f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Later on, we'll check with the 'Game Manager' to make
        // sure the game isn't over

        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
    }
    private void Fire()
    {

        // Create an instance of the shell and store a reference to its rigidbody
        Rigidbody shellInstance = Instantiate(m_Shell,
            m_FireTransform.position, m_FireTransform.rotation);

        // Set the shell's velocity to the launch force in the fire
        // position's forward direction
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
    }
}
