using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveMinionCritHeadAttack : MonoBehaviour
{
    public GameObject player1;
    private float last_dive_time;
    private float base_dive_cooldown = 1.5f;

    private float dive_cooldown_temporary_mod;
    private float dive_cooldown_permanent_mod;
    private float dive_cooldown;

    private float permanent_range = 2f;
    private float temporary_range = 2f;

    private Rigidbody boss_rigidbody;
    private ParticleSystem dive_particles;
    private ActiveHitbox dive_hitbox;

    private int dive_state = 0; // 0/1/2/3 == not diving / dive startup / dive active / dive recovery
    public float base_dive_force = 250f;
    private float dive_force_permanent_mod;
    private float dive_force_temporary_mod;
    private float dive_force_permanent_range = 5f;
    private float dive_force_temporary_range = 5f;
    private float dive_force;
    private float dive_startup_time;
    public float dive_startup_duration = 1.5f;
    public float dive_active_duration = 1f;
    public float dive_recovery_duration = 1f;

    float vertical_float_speed = 100f;
    float horizontal_float_speed = 100f;
    float vertical_dive_min_range = 2f;
    float horizontal_dive_max_range = 3f;
    float vertical_dive_min_range_range = 2f;
    float horizontal_dive_max_range_range = 3f;

    [HideInInspector]
    public int attack_state = 0; // 0/1/2/3 == not attacking / attack startup / attack active / attack recovery

    // Start is called before the first frame update
    void Start() {
        GameManager gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        dive_hitbox = gameObject.GetComponentInChildren<ActiveHitbox>();
        player1 = gameManager.Player1;
        last_dive_time = Time.time;
        boss_rigidbody = GetComponent<Rigidbody>();
        dive_cooldown_permanent_mod = Random.Range(0f, permanent_range);
        dive_cooldown_temporary_mod = Random.Range(0f, temporary_range);
        dive_cooldown = base_dive_cooldown + dive_cooldown_permanent_mod + dive_cooldown_temporary_mod;

        dive_force_permanent_mod = Random.Range(0f, dive_force_permanent_range);
        dive_force_temporary_mod = Random.Range(0f, dive_force_temporary_range);
        dive_force = base_dive_force + dive_force_permanent_mod + dive_force_temporary_mod;
        dive_particles = GetComponent<ParticleSystem>();

        vertical_dive_min_range = vertical_dive_min_range + Random.Range(0f, vertical_dive_min_range_range);
        horizontal_dive_max_range = horizontal_dive_max_range + Random.Range(0f, horizontal_dive_max_range_range);


    }

    public void delay_dive() {
        dive_recovery_start();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (dive_state != 2) {
            transform.LookAt(player1.transform);
        }
        if (last_dive_time + dive_cooldown < Time.time && dive_state == 0) {
            bool do_dive = true;
            if (gameObject.transform.position.y - player1.transform.position.y < vertical_dive_min_range) {
                do_dive = false;
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * vertical_float_speed);
            }
            if (gameObject.transform.position.x - player1.transform.position.x > horizontal_dive_max_range) {
                do_dive = false;
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * horizontal_float_speed);
            }
            if (player1.transform.position.x - gameObject.transform.position.x > horizontal_dive_max_range) {
                do_dive = false;
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * -horizontal_float_speed);
            }
            if (do_dive) {
                dive_startup();
            }
        }
        if (dive_startup_time + dive_startup_duration < Time.time && dive_state == 1) {
            dive();
        }
        if (last_dive_time + dive_cooldown + dive_startup_duration + dive_active_duration < Time.time && dive_state == 2) {
            dive_recovery_start();
        }
        if (last_dive_time + dive_cooldown + dive_startup_duration + dive_active_duration + dive_recovery_duration < Time.time && dive_state == 3) {
            dive_recovery_end();
        }
    }

    void dive_startup() {
        dive_particles.Play();
        dive_startup_time = Time.time;
        dive_state = attack_state = 1;
    }

    void dive() {
        dive_hitbox.is_active = true;
        dive_force_temporary_mod = Random.Range(0f, dive_force_temporary_range);
        dive_force = base_dive_force + dive_force_permanent_mod + dive_force_temporary_mod;

        boss_rigidbody.AddForce(transform.forward * dive_force, ForceMode.Impulse);
        dive_state = attack_state = 2;
    }

    public void dive_recovery_start() {
        dive_state = attack_state = 3;
        dive_particles.Stop();
        dive_hitbox.is_active = false;
    }

    void dive_recovery_end() {
        //Debug.Log("recovery frames ended");
        dive_state = attack_state = 0;
        last_dive_time = Time.time;
        dive_cooldown_temporary_mod = Random.Range(0f, temporary_range);
        dive_cooldown = base_dive_cooldown + dive_cooldown_permanent_mod + dive_cooldown_temporary_mod;
    }

}

