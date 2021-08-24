using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_thrust = 100f;
    private bool can_jump = true;
    public BoxCollider feet;
    private bool is_jumping;
    private bool is_firing;
    public GameObject bullet_projectile;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") && can_jump)
        {
            is_jumping = true;
        }
        if (Input.GetButton("Fire1"))
        {
            is_firing = true;
        }
    }
    void FixedUpdate()
    {
        if (is_jumping)
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            m_Rigidbody.AddForce(transform.up * m_thrust);
            can_jump = false;
            is_jumping = false;
        }
        if (is_firing)
        {
            GameObject bullet = Instantiate(bullet_projectile, transform.position, Quaternion.identity) as GameObject;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            can_jump = true;
        }
    }
}
