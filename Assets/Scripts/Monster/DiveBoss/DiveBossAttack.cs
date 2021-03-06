using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveBossAttack : MonoBehaviour
{
    public GameObject player1;
    private float last_dive_time;
    private float base_dive_cooldown = 1f;
    
    private float dive_cooldown_temporary_mod;
    private float dive_cooldown;

    private float temporary_range = 1f;
    
    private Rigidbody boss_rigidbody;
    private ParticleSystem dive_particles;
    private ActiveHitbox dive_hitbox;

    private int dive_state = 0; // 0/1/2/3 == not diving / dive startup / dive active / dive recovery
    public float base_dive_force = 500f;
    private float dive_force_temporary_mod;
    private float dive_force_temporary_range = 250f;
    private float dive_force;
    public float dive_startup_duration = 1.5f;
    public float dive_active_duration = 1f;
    public float dive_recovery_duration = 1f;

    [HideInInspector]
    public int attack_state = 0; // 0/1/2/3 == not attacking / attack startup / attack active / attack recovery

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        dive_hitbox = gameObject.GetComponentInChildren<ActiveHitbox>();
        player1 = gameManager.Player1;
        last_dive_time = Time.time; 
        boss_rigidbody = GetComponent<Rigidbody>();
        dive_cooldown_temporary_mod = Random.Range(0f, temporary_range);
        dive_cooldown = base_dive_cooldown + dive_cooldown_temporary_mod;
        
        dive_force_temporary_mod = Random.Range(0f, dive_force_temporary_range);
        dive_force = base_dive_force + dive_force_temporary_mod;
        dive_particles = GetComponent<ParticleSystem>();
    }

    public void destroyed_weakspot() {
        if (dive_state == 2 || dive_state == 3) {
            dive_startup();
        }
    }

    // Update is called once per frame
    void Update() {
        if (dive_state != 2 ) {
            transform.LookAt(player1.transform);
        }
        if (last_dive_time + dive_cooldown < Time.time && dive_state == 0) {
            dive_startup();
        }
        if (last_dive_time + dive_cooldown + dive_startup_duration < Time.time && dive_state == 1) {
            dive();
        }
        if (last_dive_time + dive_cooldown + dive_startup_duration + dive_active_duration < Time.time && dive_state == 2) {
            dive_recovery_start();
        }
        if (last_dive_time + dive_cooldown + dive_startup_duration + dive_active_duration + dive_recovery_duration < Time.time && dive_state == 3) {
            dive_recovery_end();
        }
    }

    void dive_startup()
    {
        dive_particles.Play();
        dive_state = attack_state = 1;
    }

    void dive() {
        //Debug.Log("dive!");
        dive_hitbox.is_active = true;
        //Debug.Log(last_dive_time + dive_cooldown);
        dive_force_temporary_mod = Random.Range(0f, dive_force_temporary_range);
        dive_force = base_dive_force + dive_force_temporary_mod;
        
        boss_rigidbody.AddForce(transform.forward * dive_force, ForceMode.Impulse);
        dive_state = attack_state = 2;
    }
    
    public void dive_recovery_start()
    {
        dive_state = attack_state = 3;
        dive_particles.Stop();
        dive_hitbox.is_active = false;
    }

    public void dive_recovery_end() {
        //Debug.Log("recovery frames ended");
        dive_state = attack_state = 0;
        last_dive_time = Time.time;
        dive_cooldown_temporary_mod = Random.Range(0f, temporary_range);
        dive_cooldown = base_dive_cooldown + dive_cooldown_temporary_mod;
    }

    private void FixedUpdate() { }
}
