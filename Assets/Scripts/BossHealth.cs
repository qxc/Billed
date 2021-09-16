using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int max_health = 1000;
    public int current_health = 400;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void get_hit(int damage, string damage_type)
    {
        Debug.Log("OW!");
        current_health -= damage;
        Debug.Log(current_health);
        if ( current_health <= 0 )
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
        Debug.Log(other.tag);
        ActiveHitbox hitbox = other.GetComponent<ActiveHitbox>();
        if ( hitbox )
        {
            get_hit(hitbox.damage, hitbox.damage_type);
        }

    }
}
