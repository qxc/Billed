using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockPlace : MonoBehaviour
{
    private bool is_previewing_block;
    private bool is_placing_block;
    private GameObject block_preview;
    public GameObject block_preview_prefab;
    public GameObject block_prefab;
    private Movement movement_script;
    private float block_placement_distance = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        block_preview = Instantiate(block_preview_prefab) as GameObject;
        block_preview.SetActive(false);

        movement_script = gameObject.GetComponent<Movement>();
    }

    Vector3 RoundPosition(Transform trans)
    {
        Vector3 collider_size = block_preview_prefab.transform.localScale;
        float x_len = collider_size.x;
        return new Vector3(Mathf.RoundToInt(trans.position.x / x_len) * x_len, trans.position.y, trans.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (is_previewing_block)
        {
            Vector3 spawn_loc;
            Vector3 grid_position = RoundPosition(transform);
            
            if (movement_script.is_facing_left)
            {   
                spawn_loc = new Vector3(grid_position.x + -block_placement_distance, grid_position.y, grid_position.z);
            } 
            else
            {
                spawn_loc = new Vector3(grid_position.x + block_placement_distance, grid_position.y, grid_position.z);
            }

            block_preview.transform.position = spawn_loc;
            block_preview.SetActive(true);
            if (is_placing_block)
            {
                GameObject block = Instantiate(block_prefab) as GameObject;
                block.transform.position = block_preview.transform.position;
                is_placing_block = false;
                is_previewing_block = false;
                block_preview.SetActive(false);
            }
        }
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            is_previewing_block = true;
            Debug.Log(transform.position);
        }
        if (Input.GetButtonUp("Fire3"))
        {
            if (is_previewing_block)
            {
                is_placing_block = true;
            }
        }
    }
}
