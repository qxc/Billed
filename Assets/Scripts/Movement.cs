using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Thrust = 40f;
    public int max_jumps = 2;
    private int jumps_remaining = 0;
    private float jump_cooldown = .4f;
    private float last_jump_time;
    public BoxCollider feet;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
        jumps_remaining = max_jumps;
        last_jump_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") && jumps_remaining > 0 && last_jump_time + jump_cooldown < (Time.time ))
        {
            Debug.Log(Time.time);
            Debug.Log(last_jump_time);
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            m_Rigidbody.AddForce(transform.up * m_Thrust, ForceMode.Impulse);
            last_jump_time = Time.time;
            jumps_remaining--;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.thisCollider == feet)
            {
                jumps_remaining = max_jumps;
            }

        }
    }
}