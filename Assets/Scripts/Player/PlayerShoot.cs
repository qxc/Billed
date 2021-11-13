using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private bool is_firing;
    public GameObject bullet_projectile;
    private float m_thrust = 6f;
    public GameObject bullet_spawn;
    private float last_shoot_time;
    public float shoot_cooldown = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        //bullet_spawn.transform.position = transform.position;
        last_shoot_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("FireTriggerRight") > 0)
        {
            is_firing = true;
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("HorizontalLook");
        float v = Input.GetAxis("VerticalLook");
        
        if (is_firing && shoot_cooldown + last_shoot_time < Time.time)
        {
            GameObject bullet = Instantiate(bullet_projectile) as GameObject;
            bullet.transform.position = transform.position;

            Vector3 new_bullet_angle = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y,  Mathf.Atan2(-v,h) * Mathf.Rad2Deg);
            bullet.transform.rotation = Quaternion.Euler(new_bullet_angle);
            bullet.GetComponentInChildren<Rigidbody>().AddRelativeForce(bullet.transform.right * m_thrust, ForceMode.Impulse);
            last_shoot_time = Time.time;
        }
        is_firing = false;
    }
}
