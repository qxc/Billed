using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float max_health = 1000;
    public float current_health = 400;
    public GameObject damage_numbers_prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    void get_hit(float damage, string damage_type)
    {
        // 0/1/2/3 == not attacking / attack startup / attack active / attack recovery
        MrBossMan monster_script = gameObject.GetComponent<MrBossMan>();
        int attack_state = monster_script.attack_state;
        float damage_modifier = 1f;
        if (attack_state == 1) {
            monster_script.delay_dive();
            damage_modifier = damage_modifier + .5f;
        }
        if (attack_state == 2) {
            damage_modifier = damage_modifier + 1f;
        }
        float damage_taken = damage * damage_modifier;
        make_damage_numbers(damage_taken, damage_modifier);
        current_health -= damage_taken;
        
        if (current_health <= 0)
        {
            die();
        }
    }

    void make_damage_numbers(float damage_taken, float damage_modifier)
    {
        GameObject damage_numbers = Instantiate(damage_numbers_prefab) as GameObject;
        damage_numbers.GetComponent<Rigidbody>().AddForce(Random.Range(-225f, 225f), 60f, -100f);
        TextMesh text_mesh = damage_numbers.GetComponent<TextMesh>();
        text_mesh.text = damage_taken.ToString();
        damage_numbers.transform.position = gameObject.transform.position;
        if (damage_modifier > 1) {
            text_mesh.color = Color.red;
        }
    }

    public void die()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        ActiveHitbox hitbox = other.GetComponent<ActiveHitbox>();
        if ( hitbox )
        {
            get_hit(hitbox.damage, hitbox.damage_type);
        }

    }
}
