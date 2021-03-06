using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveBossHealth : MonoBehaviour, IMonsterHealth
{
    public float max_health { get; set; }
    public float current_health { get; set; }
    public float weakspot_health;
    public float weakspot_max_health;
    public float weakspot_destroyed_time;
    public float weakspot_respawn_timer;
    public GameObject _damage_numbers_prefab;
    public GameObject crit_hurtbox_prefab;
    public GameObject damage_numbers_prefab {
        get
        {
            return _damage_numbers_prefab;
        }
        set
        {
            _damage_numbers_prefab = value;
        }
    }

    public HingeJoint hinge_joint;
    DiveBossAttack monster_script;
    // Start is called before the first frame update
    void Start() {
        weakspot_destroyed_time = Time.time;
        weakspot_respawn_timer = 5f;
        max_health = 1000;
        weakspot_max_health = 90;
        current_health = 400;
        monster_script = gameObject.GetComponent<DiveBossAttack>();
        create_weakspot();
    }

    // Update is called once per frame
    void Update() {
        if ( !hinge_joint && weakspot_destroyed_time + weakspot_respawn_timer < Time.time ){
            create_weakspot();
        }
    }

    public void create_weakspot() {
        GameObject weakspot = Instantiate(crit_hurtbox_prefab) as GameObject;
        //transform.eulerAngles = new Vector3( 0, transform.eulerAngles.y + 180, transform.eulerAngles.z );
        //weakspot.transform.position = gameObject.transform.position + new Vector3(0, 2.5f, 0);
        weakspot.transform.position = gameObject.transform.position + gameObject.transform.up * 3f;
        weakspot.transform.rotation = gameObject.transform.rotation;
        hinge_joint = weakspot.GetComponent<HingeJoint>();
        hinge_joint.anchor = new Vector3(0, -3f, 0);
        hinge_joint.connectedBody = gameObject.GetComponent<Rigidbody>();
        weakspot_health = weakspot_max_health;
        monster_script.dive_recovery_end();
    }

    public void get_hit(float damage, string damage_type, bool is_crit) {
        //Debug.Log("BOSS GOT HIT");
        make_damage_numbers(damage, is_crit);
        current_health -= damage;
        
        if (current_health <= 0) {
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().minionDied();
            die();
        }
    }

    public void get_weakspot_hit(float damage, string damage_type) {
        weakspot_health = weakspot_health - damage;
        if (weakspot_health < 0) {
            monster_script.destroyed_weakspot();
            Destroy(hinge_joint);
            weakspot_destroyed_time = Time.time;
        }
        get_hit(damage, damage_type, true);
    }



    public void make_damage_numbers(float damage_taken, bool is_crit) {
        GameObject damage_numbers = Instantiate(damage_numbers_prefab) as GameObject;
        damage_numbers.GetComponent<Rigidbody>().AddForce(Random.Range(-225f, 225f), 60f, -100f);
        TextMesh text_mesh = damage_numbers.GetComponent<TextMesh>();
        text_mesh.text = damage_taken.ToString();
        damage_numbers.transform.position = gameObject.transform.position;
        if ( is_crit ) {
            text_mesh.color = Color.red;
        }
    }

    public void die() {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log(gameObject.name);
        //Debug.Log(other.tag);
        ActiveHitbox hitbox = other.GetComponent<ActiveHitbox>();
        if ( hitbox ) {
            get_hit(hitbox.damage, hitbox.damage_type, false);
        }

    }
}
