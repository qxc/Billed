using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockPlace : MonoBehaviour
{
    private bool is_previewing_blocks;
    private GameObject block_preview;
    public GameObject block_preview_prefab;
    private Movement movement_script;
    private float block_placement_distance = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        block_preview = Instantiate(block_preview_prefab) as GameObject;
        block_preview.SetActive(false);

        movement_script = gameObject.GetComponent<Movement>();
    }

    /**
     int round(double i, int v){
    return Math.round(i/v) * v;
     * 
    */
    Vector3 RoundPosition(Transform trans)
    {
        Vector3 collider_size = block_preview_prefab.transform.localScale;
        Debug.Log(collider_size);
        
        return new Vector3(Mathf.RoundToInt(trans.position.x), Mathf.RoundToInt(trans.position.y), Mathf.RoundToInt(trans.position.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (is_previewing_blocks)
        {
            if (movement_script.is_facing_left)
            {
                Vector3 grid_position = RoundPosition(transform);
                Vector3 spawn_loc = new Vector3(grid_position.x + -block_placement_distance, grid_position.y, grid_position.z);
                block_preview.transform.position = spawn_loc;
            } 
            else
            {
                Vector3 grid_position = RoundPosition(transform);
                Vector3 spawn_loc = new Vector3(grid_position.x + block_placement_distance, grid_position.y, grid_position.z);
                block_preview.transform.position = spawn_loc;
            }
            block_preview.SetActive(true);
        }
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            is_previewing_blocks = true;
            Debug.Log(transform.position);
        }
        if (Input.GetButtonUp("Fire3"))
        {
            Debug.Log("Not previewing blocks");
            block_preview.SetActive(false);
            is_previewing_blocks = false;
        }
    }
}
