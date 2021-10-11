using System.Collections.Generic;

public class PlayerBlockPlace : MonoBehaviour
{
    private bool is_previewing_block;
    private bool is_placing_block;

    private GameObject block_preview;
    private GameObject block_preview_prefab;
    private GameObject block_prefab;

    private Movement movement_script;
    private float block_placement_distance = 3f;

    public List<PlaceableBlock> all_blocks;
    private int selected_block = 1; // an index in the all_blocks list
    // block_prefab, block_preview_prefab, current_stock

    // Start is called before the first frame update
    void Start()
    {
        //block_preview = Instantiate(block_preview_prefab) as GameObject;

        movement_script = gameObject.GetComponent<Movement>();
    }

    Vector3 RoundPosition(Transform trans)
    {
        float x_len = 2f;
        return new Vector3(Mathf.RoundToInt(trans.position.x / x_len) * x_len, trans.position.y, trans.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (is_previewing_block)
        {
            if (!all_blocks[selected_block].instantiated_block)
            {
                all_blocks[selected_block].instantiated_block = Instantiate(all_blocks[selected_block].block_preview_prefab) as GameObject;
                all_blocks[selected_block].instantiated_block.SetActive(false);
            }
            block_preview = all_blocks[selected_block].instantiated_block;
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
                GameObject block = Instantiate(all_blocks[selected_block].block_prefab) as GameObject;
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
        }
        if (Input.GetButtonUp("Fire3")) {
            if (is_previewing_block)
            {
                is_placing_block = true;
            }
        }
        float change_block_input = Input.GetAxis("NextBlock");
        if (change_block_input)
        {
            incrementIndex(all_blocks.Count);
        }
    }

    int incrementIndex(int max_depth)
    {
        if (max_depth == 0)
        {
            return;
        }
        int max_index = all_blocks.Count;
        selected_block++;
        if (selected_block == max_index)
        {
            selected_block = 0;
        }
        if (all_blocks[selected_block].current_stock < 1)
        {
            incrementIndex(max_depth - 1);
        }
    }
    // if holding dpad, change selected block in that direction, logic to wrap around from max array to 0
    // Wrap from 0 to max if going backwards
    // Max # of changes per second
}
