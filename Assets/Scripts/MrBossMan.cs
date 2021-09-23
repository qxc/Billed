using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBossMan : MonoBehaviour
{
    public GameObject player1;
    private float last_dive_time;
    private float base_dive_cooldown = 1.5f;
    private float dive_cooldown_permanent_mod;
    private float permanent_range = 2f;
    private float dive_cooldown_temporary_mod;
    private float temporary_range = 2f;
    private float dive_cooldown;

    private int dive_state = 0; // 0/1/2/3 == not diving / dive startup / dive active / dive recovery
    private Rigidbody boss_rigidbody;
    private float base_dive_force = 12f;
    private float dive_force_permanent_mod;
    private float dive_force_temporary_mod;
    private float dive_force_permanent_range = 5f;
    private float dive_force_temporary_range = 5f;
    private float dive_force;
    public float dive_startup_duration = 1.5f;
    public float dive_active_duration = 1f;
    public float dive_recovery_duration = 1f;
    private ParticleSystem dive_particles;

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
        dive_particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(transform.position);
        //Debug.Log(player1.transform.position);
        transform.LookAt(player1.transform);
        if (last_dive_time + dive_cooldown < Time.time && dive_state == 0) {
            Debug.Log("particles!");
            dive_particles.Play();
            Debug.Log(last_dive_time + dive_cooldown);
            dive_state = 1;
        }
        if (last_dive_time + dive_cooldown + dive_startup_duration < Time.time && dive_state == 1) {
            dive();
        }
        if (last_dive_time + dive_cooldown + dive_startup_duration + dive_active_duration < Time.time && dive_state == 2) {
            Debug.Log("active frames ended");
            dive_state = 3;
            dive_particles.Stop();
        }
        if (last_dive_time + dive_cooldown + dive_startup_duration + dive_active_duration + dive_recovery_duration < Time.time && dive_state == 3) {
            dive_recovery_end();
        }

    }

    void dive() {
        Debug.Log("dive!");
        Debug.Log(last_dive_time + dive_cooldown);
        dive_force_temporary_mod = Random.Range(0f, dive_force_temporary_range);
        dive_force = base_dive_force + dive_force_permanent_mod + dive_force_temporary_mod;
        
        boss_rigidbody.AddForce(transform.forward * dive_force, ForceMode.Impulse);
        dive_state = 2;
    }
    void dive_recovery_end() {
        Debug.Log("recovery frames ended");
        dive_state = 0;
        last_dive_time = Time.time;
        dive_cooldown_temporary_mod = Random.Range(0f, temporary_range);
        dive_cooldown = base_dive_cooldown + dive_cooldown_permanent_mod + dive_cooldown_temporary_mod;
    }

    private void FixedUpdate() { }
}
