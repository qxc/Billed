using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody bullet_rigidbody;
    float bullet_lifetime = 2f;
    int bullet_state = 0; // 0/1, 0 is traveling, 1 is post collision
    float destroy_time = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (destroy_time < Time.time && bullet_state == 1) {
            Destroy(gameObject);            
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (bullet_state == 0)
        {
            if (bullet_rigidbody == null)
            {
                bullet_rigidbody = GetComponent<Rigidbody>();
            }
            bullet_state = 1;
            bullet_rigidbody.constraints = RigidbodyConstraints.None;
            bullet_rigidbody.useGravity = true;

            int direction = Random.Range(0, 2);
            if (direction == 0) { direction = -1; }
            else { direction = 1; }

            bullet_rigidbody.AddForce(0, 0, direction * Random.Range(25, 100));
            destroy_time = Time.time + bullet_lifetime;
        }
    }
}
