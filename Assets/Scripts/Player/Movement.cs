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
    bool is_dashing = false;
    float last_dash_time;
    float dash_cooldown = 1f;
    float dash_force = 150f;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.sleepThreshold = 0f;
        jumps_remaining = max_jumps;
        last_jump_time = Time.time;
        last_dash_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("FireTriggerLeft") > 0 && last_dash_time + dash_cooldown < Time.time && Input.GetAxis("Horizontal") != 0) {
            is_dashing = true;
        }
        if (Input.GetButton("Fire4") && jumps_remaining > 0 && last_jump_time + jump_cooldown < (Time.time )) {
            //Apply a force to this Rigidbody in direction of this GameObjects up axis
            m_rigidbody.AddForce(transform.up * jump_thrust, ForceMode.Impulse);
            last_jump_time = Time.time;
            jumps_remaining--;
        }

        float acceleration_modifier = is_grounded ? ground_acceleration_modifier : air_acceleration_modifier;

        if (Input.GetAxis("Horizontal") < 0f && m_rigidbody.velocity.x > -top_speed) {
            m_rigidbody.AddForce(transform.right * Input.GetAxis("Horizontal") * acceleration_modifier, ForceMode.Force);
        }

        if (Input.GetAxis("Horizontal") > 0f && m_rigidbody.velocity.x < top_speed) {
            m_rigidbody.AddForce(transform.right * Input.GetAxis("Horizontal") * acceleration_modifier, ForceMode.Force);
        }

        if (Input.GetAxis("HorizontalLook") < 0f ) {
            is_facing_left = true;
        }

        if (Input.GetAxis("HorizontalLook") > 0f ) {
            is_facing_left = false;
        }
    }

    void FixedUpdate() {
        if (is_dashing) {
            is_dashing = false;
            float h = Input.GetAxis("Horizontal");            
            if (h > 0) {
                m_rigidbody.AddRelativeForce(gameObject.transform.right * dash_force, ForceMode.Impulse);
            } else {
                m_rigidbody.AddRelativeForce(-gameObject.transform.right * dash_force, ForceMode.Impulse);
            }
            last_dash_time = Time.time;
        }
    }
    void OnTriggerStay(Collider collider) {
        is_grounded = true;
        jumps_remaining = max_jumps;
    }
    private void OnTriggerExit(Collider other) {
        is_grounded = false;
    }
}