using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float max_health = 1000;
    public float current_health = 400;
    float get_hit_cooldown = 1f;
    float last_get_hit_time;
    // Start is called before the first frame update
    void Start()
    {
        last_get_hit_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void get_hit(float damage, string damage_type)
    {
        current_health -= damage;
        gameObject.GetComponentInChildren<TextMesh>().text = current_health.ToString();
        //Debug.Log(current_health);
        if (current_health <= 0)
        {
            die();
        }
    }

    void die()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        ActiveHitbox hitbox = other.GetComponent<ActiveHitbox>();
        if (hitbox && hitbox.is_active)
        {
            if (last_get_hit_time + get_hit_cooldown < Time.time)
            {
                Debug.Log("getting hit");
                get_hit(hitbox.damage, hitbox.damage_type);
                last_get_hit_time = Time.time;
            }
        }
    }
}
