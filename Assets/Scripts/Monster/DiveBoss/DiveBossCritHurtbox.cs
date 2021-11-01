using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveBossCritHurtbox : MonoBehaviour
{
    public DiveBossHealth health_manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision) {
        Debug.Log("COLLISION");
        ActiveHitbox hitbox = collision.gameObject.GetComponent<ActiveHitbox>();
        if (hitbox) {
            health_manager.get_weakspot_hit(hitbox.damage, hitbox.damage_type);
        }
         
    }
}
