using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody m_rigidbody;
    public float jump_thrust = 70f;
    public int max_jumps = 1;
    public float top_speed = 10f;
    private int jumps_remaining = 0;
    private float jump_cooldown = .4f;
    private float last_jump_time;
    private bool is_grounded = false;
    public float ground_acceleration_modifier = 50f;
    public float air_acceleration_modifier = 40f;
    public GameObject bullet_spawn;
    public bool is_facing_left;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.sleepThreshold = 0f;
        jumps_remaining = max_jumps;
        last_jump_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") && jumps_remaining > 0 && last_jump_time + jump_cooldown < (Time.time ))
        {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            m_rigidbody.AddForce(transform.up * jump_thrust, ForceMode.Impulse);
            last_jump_time = Time.time;
            jumps_remaining--;
        }


        float acceleration_modifier = is_grounded ? ground_acceleration_modifier : air_acceleration_modifier;

        if (Input.GetAxis("Horizontal") < 0f && m_rigidbody.velocity.x > -top_speed)
        {
            //bullet_spawn.transform.position = new Vector3(transform.position.x - 1, bullet_spawn.transform.position.y, bullet_spawn.transform.position.z);
            //bullet_spawn.transform.eulerAngles = new Vector3(bullet_spawn.transform.eulerAngles.x, 180, bullet_spawn.transform.eulerAngles.z);
            m_rigidbody.AddForce(transform.right * Input.GetAxis("Horizontal") * acceleration_modifier, ForceMode.Force);
            is_facing_left = true;
        }

        if (Input.GetAxis("Horizontal") > 0f && m_rigidbody.velocity.x < top_speed)
        {
            //bullet_spawn.transform.position = new Vector3(transform.position.x + 1, bullet_spawn.transform.position.y, bullet_spawn.transform.position.z);
            //bullet_spawn.transform.eulerAngles = new Vector3(bullet_spawn.transform.eulerAngles.x, 0, bullet_spawn.transform.eulerAngles.z);
            m_rigidbody.AddForce(transform.right * Input.GetAxis("Horizontal") * acceleration_modifier, ForceMode.Force);
            is_facing_left = false;
        }
    }

    void FixedUpdate()
    {
    }
    void OnTriggerStay(Collider collider)
    {
        is_grounded = true;
        jumps_remaining = max_jumps;
    }
    private void OnTriggerExit(Collider other)
    {
        is_grounded = false;
    }
}