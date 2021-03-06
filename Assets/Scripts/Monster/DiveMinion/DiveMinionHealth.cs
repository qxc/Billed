using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveMinionHealth : MonoBehaviour, IMonsterHealth
{
    public float max_health { get; set; }
    public float current_health { get; set; }
    public GameObject _damage_numbers_prefab;
    public GameObject damage_numbers_prefab {
        get {
            return _damage_numbers_prefab;
        }
        set {
            _damage_numbers_prefab = value;
        }
    }

    // Start is called before the first frame update
    void Start() {
        max_health = 100;
        current_health = max_health;
    }

    // Update is called once per frame
    void Update() { }
    public void get_hit(float damage, string damage_type, bool is_crit) {
        // 0/1/2/3 == not attacking / attack startup / attack active / attack recovery
        DiveMinionAttack monster_script = gameObject.GetComponent<DiveMinionAttack>();
        int attack_state = monster_script.attack_state;
        float damage_modifier = 1f;
        if (attack_state == 1) {
            monster_script.delay_dive();
            damage_modifier = damage_modifier + 2f;
        }
        if (attack_state == 2) {
            damage_modifier = damage_modifier + 3f;
        }
        float damage_taken = damage * damage_modifier;
        is_crit = damage_modifier > 1f;
        make_damage_numbers(damage_taken, is_crit);
        current_health -= damage_taken;

        if (current_health <= 0) {
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().minionDied();
            die();
        }
    }

    public void get_weakspot_hit(float damage, string damage_type) { }
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

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        ActiveHitbox hitbox = other.GetComponent<ActiveHitbox>();
        if (hitbox) {
            get_hit(hitbox.damage, hitbox.damage_type, false);
        }

    }
}
