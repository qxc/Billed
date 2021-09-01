using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Debug.Log(transform.position.x - player.position.x);
        if (transform.position.x - player.position.x > 2f ){
            transform.position = new Vector3(player.position.x + 2f, transform.position.y, transform.position.z);
        }
        if (transform.position.x - player.position.x < -2f ){
            transform.position = new Vector3(player.position.x - 2f, transform.position.y, transform.position.z);
        }
        
    }
}
