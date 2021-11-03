using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveBossCritHurtbox : MonoBehaviour
{
    // Later this can be replaced by the stats on the player's weapon
    private float crit_multi;
    public DiveBossHealth health_manager;
    GameObject hinge_anchor;
    // Start is called before the first frame update
    void Start()
    {
        crit_multi = 1.5f;
        health_manager = GameObject.Find("DiveBoss").GetComponent<DiveBossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("COLLISION");
        ActiveHitbox hitbox = collision.gameObject.GetComponent<ActiveHitbox>();
        if (hitbox) {
            health_manager.get_weakspot_hit(hitbox.damage*crit_multi, hitbox.damage_type);
        }
         
    }
}
