using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveMinionCritHurtbox : MonoBehaviour
{
    // Later this can be replaced by the stats on the player's weapon
    private float crit_multi;
    public IMonsterHealth health_manager;
    GameObject connected_body;
    // Start is called before the first frame update
    void Start()
    {
        crit_multi = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (connected_body == null) {
            connected_body = gameObject.GetComponent<FixedJoint>().connectedBody.gameObject;
        }
        if (health_manager == null) {
            health_manager = connected_body.GetComponent<IMonsterHealth>();
        }
        ActiveHitbox hitbox = collision.gameObject.GetComponent<ActiveHitbox>();
        if (hitbox) {
            health_manager.get_weakspot_hit(hitbox.damage * crit_multi, hitbox.damage_type);
        }
    }
}

