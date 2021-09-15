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
        float h = Input.GetAxis("HorizontalLook");
        if (h != 0)
        {
            //Debug.Log(h);

        }
        float v = Input.GetAxis("VerticalLook");
        if (v != 0)
        {
            //Debug.Log(v);
        }
        
        if (is_firing && shoot_cooldown + last_shoot_time < Time.time)
        {
            GameObject bullet = Instantiate(bullet_projectile) as GameObject;
            bullet.transform.position = transform.position;
            //Debug.Log(bullet.transform.eulerAngles);
            //Debug.Log(h);
            //Debug.Log(v);
            //Debug.Log(Mathf.Atan2(h, v));

            //Debug.Log(new Vector3(bullet.transform.eulerAngles.x, Mathf.Atan2(h, v) * Mathf.Rad2Deg, bullet.transform.eulerAngles.z));
            Vector3 new_bullet_angle = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y,  Mathf.Atan2(-v,h) * Mathf.Rad2Deg);
            bullet.transform.rotation = Quaternion.Euler(new_bullet_angle);
            bullet.GetComponentInChildren<Rigidbody>().AddRelativeForce(bullet.transform.right * m_thrust, ForceMode.Impulse);
            last_shoot_time = Time.time;
        }
        is_firing = false;
    }
}
