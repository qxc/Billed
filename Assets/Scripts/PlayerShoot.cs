using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private bool is_firing;
    public GameObject bullet_projectile;
    public float m_thrust = 100f;
    public GameObject bullet_spawn;

    // Start is called before the first frame update
    void Start()
    {
        //bullet_spawn.transform.position = transform.position;
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
            GameObject bullet = Instantiate(bullet_projectile) as GameObject;
            bullet.transform.position = bullet_spawn.transform.position;
            bullet.transform.rotation = bullet_spawn.transform.rotation;
            bullet.GetComponentInChildren<Rigidbody>().AddRelativeForce(transform.forward * 5f, ForceMode.Impulse);
            is_firing = false;
        }
    }
}
