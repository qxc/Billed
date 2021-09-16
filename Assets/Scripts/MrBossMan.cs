using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBossMan : MonoBehaviour
{
    public GameObject player1;
    private float last_dive_time;
    public float dive_cooldown = 4f;
    private bool is_diving = false;
    private Rigidbody boss_rigidbody;
    public float dive_force = 30f;

    // Start is called before the first frame update
    void Start()
    {
        last_dive_time = Time.time; 
        boss_rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(transform.position);
        //Debug.Log(player1.transform.position);
        transform.LookAt(player1.transform);
        if (last_dive_time + dive_cooldown < Time.time) {
            Debug.Log("DIVE!!");
            is_diving = true;
            last_dive_time = Time.time;
        }
    }

    private void FixedUpdate() {
        if ( is_diving ) {
            boss_rigidbody.AddForce(transform.forward * dive_force, ForceMode.Impulse);
            is_diving = false;
        } 
    }
}
