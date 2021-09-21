using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBossMan : MonoBehaviour
{
    public GameObject player1;
    private float last_dive_time;
    private float base_dive_cooldown = 1f;
    private float dive_cooldown_permanent_mod;
    private float permanent_range = 2f;
    private float dive_cooldown_temporary_mod;
    private float temporary_range = 2f;
    private float dive_cooldown;

    private bool is_diving = false;
    private Rigidbody boss_rigidbody;
    private float base_dive_force = 15f;
    private float dive_force_permanent_mod;
    private float dive_force_temporary_mod;
    private float dive_force_permanent_range = 5f;
    private float dive_force_temporary_range = 5f;
    private float dive_force;

    // Start is called before the first frame update
    void Start()
    {
        ObjectReferences objectReferences = GameObject.FindWithTag("ObjectReferences").GetComponent<ObjectReferences>();
        player1 = objectReferences.Player1;
        last_dive_time = Time.time; 
        boss_rigidbody = GetComponent<Rigidbody>();
        dive_cooldown_permanent_mod = Random.Range(0f, permanent_range);
        dive_cooldown_temporary_mod = Random.Range(0f, temporary_range);
        dive_cooldown = base_dive_cooldown + dive_cooldown_permanent_mod + dive_cooldown_temporary_mod;
        
        dive_force_permanent_mod = Random.Range(0f, dive_force_permanent_range);
        dive_force_temporary_mod = Random.Range(0f, dive_force_temporary_range);
        dive_force = base_dive_force + dive_force_permanent_mod + dive_force_temporary_mod;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(transform.position);
        //Debug.Log(player1.transform.position);
        transform.LookAt(player1.transform);
        if (last_dive_time + dive_cooldown < Time.time) {
            is_diving = true;
            last_dive_time = Time.time;
            dive_cooldown_temporary_mod = Random.Range(0f, temporary_range);
            dive_cooldown = base_dive_cooldown + dive_cooldown_permanent_mod + dive_cooldown_temporary_mod;
        }
    }

    private void FixedUpdate() {
        if ( is_diving ) {
            dive_force_temporary_mod = Random.Range(0f, dive_force_temporary_range);
            dive_force = base_dive_force + dive_force_permanent_mod + dive_force_temporary_mod;
            boss_rigidbody.AddForce(transform.forward * dive_force, ForceMode.Impulse);
            is_diving = false;
        } 
    }
}
