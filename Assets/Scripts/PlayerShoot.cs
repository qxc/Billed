using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private bool is_firing;
    public GameObject bullet_projectile;
    public float m_thrust = 100f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            is_firing = true;
        }
    }
    void FixedUpdate()
    {
        if (is_firing)
        {
            GameObject bullet = Instantiate(bullet_projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponentInChildren<Rigidbody>().AddForce(0, 0, 100);
            is_firing = false;
        }
    }
}
