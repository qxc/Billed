using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumbers : MonoBehaviour
{
    float lifetime = 1f;
    float spawn_time;
    //float fade_level = 1f;
    //float fade_increment = .05f;
    // Start is called before the first frame update
    void Start()
    {
        spawn_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //TextMesh text_mesh = gameObject.GetComponent<TextMesh>();
        //Color color = text_mesh.color;
        //fade_level = fade_level - fade_increment;
        //color.a = fade_level;
        //text_mesh.color = color;

        if (spawn_time + lifetime < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
