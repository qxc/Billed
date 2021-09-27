using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBlock : MonoBehaviour
{
    public GameObject block_preview_prefab;
    public GameObject block_prefab;
    public GameObject instantiated_block;
    
    [HideInInspector]
    public int current_stock = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
