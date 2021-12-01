using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveMinionCritHeadHealth : MonoBehaviour, IMonsterHealth
{
    public float max_health { get; set; }
    public float current_health { get; set; }
    public GameObject _damage_numbers_prefab;
    public GameObject damage_numbers_prefab
    {
        get
        {
            return _damage_numbers_prefab;
        }
        set
        {
            _damage_numbers_prefab = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        max_health = 50;
        current_health = 50;
    }

    // Update is called once per frame
    void Update() { }
    public void get_hit(float damage, string damage_type, bool is_crit) {
        // 0/1/2/3 == not attacking / attack startup / attack active / attack recovery
        DiveMinionCritHeadAttack monster_script = gameObject.GetComponent<DiveMinionCritHeadAttack>();
        make_damage_numbers(damage, is_crit);
        current_health -= damage;

        if (current_health <= 0) {
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().minionDied();
            die();
        }
    }

    public void get_weakspot_hit(float damage, string damage_type) {
        get_hit(damage, damage_type, true);
        Debug.Log("Critical Hit");
    }
    public void make_damage_numbers(float damage_taken, bool is_crit)
    {
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
        //Debug.Log(other.tag);
        ActiveHitbox hitbox = other.GetComponent<ActiveHitbox>();
        if (hitbox) {
            get_hit(hitbox.damage, hitbox.damage_type, true);
        }

    }
}

