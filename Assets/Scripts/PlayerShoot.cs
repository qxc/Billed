using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private bool is_firing;
    public GameObject bullet_projectile;
    public float m_thrust = 10f;
    public GameObject bullet_spawn;
    private float last_shoot_time;
    public float shoot_cooldown = .6f;
    // Start is called before the first frame update
    void Start()
    {
        //bullet_spawn.transform.position = transform.position;
        last_shoot_time = Time.time;
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
        float t = Input.GetAxis("HorizontalLook");
        if (t > 0)
        {
            Debug.Log(t);
        }
        
        if (is_firing && shoot_cooldown + last_shoot_time < Time.time)
        {
            GameObject bullet = Instantiate(bullet_projectile) as GameObject;
            bullet.transform.position = bullet_spawn.transform.position;
            bullet.transform.rotation = bullet_spawn.transform.rotation;
            bullet.GetComponentInChildren<Rigidbody>().AddRelativeForce(bullet_spawn.transform.right * m_thrust, ForceMode.Impulse);
            last_shoot_time = Time.time;
        }
        is_firing = false;
    }
}
