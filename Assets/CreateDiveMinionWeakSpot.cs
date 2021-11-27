using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDiveMinionWeakSpot : MonoBehaviour
{
    public GameObject weakspot_prefab;
    FixedJoint spring_joint;
    // Start is called before the first frame update
    void Start() {
        GameObject weakspot = Instantiate(weakspot_prefab) as GameObject;
        weakspot.transform.position = gameObject.transform.position + gameObject.transform.up * 1f;
        weakspot.transform.rotation = gameObject.transform.rotation;
        spring_joint = weakspot.GetComponent<FixedJoint>();
        spring_joint.connectedBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
