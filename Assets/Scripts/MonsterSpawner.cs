using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private float base_spawn_cooldown = 3f;
    private float spawn_cooldown_temporary_mod;
    private float spawn_cooldown_temporary_range;
    private float spawn_cooldown;
    private float last_spawn_time;
    private GameManager gameManager;
    public GameObject MrBossMan;
    public GameObject MiniMrBossMan;
    private GameObject player1;
    private float xSpawnDistance = 5f;
    private float ySpawnDistance = 7f;
    // Start is called before the first frame update
    void Start()
    {
        last_spawn_time = Time.time;
        spawn_cooldown_temporary_mod = Random.Range(0f, spawn_cooldown_temporary_range);
        spawn_cooldown = base_spawn_cooldown + spawn_cooldown_temporary_mod;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        player1 = gameManager.Player1;


    }

    // Update is called once per frame
    void Update()
    {
        if (last_spawn_time + spawn_cooldown < Time.time)
        {
            last_spawn_time = Time.time;
            spawn_cooldown_temporary_mod = Random.Range(0f, spawn_cooldown_temporary_range);
            spawn_cooldown = base_spawn_cooldown +  spawn_cooldown_temporary_mod;
            float spawnXMod = Random.Range(-xSpawnDistance, xSpawnDistance);
            float spawnYMod = Random.Range(0, ySpawnDistance);
            Vector3 spawn_loc = new Vector3(player1.transform.position.x + spawnXMod, player1.transform.position.y + spawnYMod, player1.transform.position.z);
            GameObject minion = Instantiate(MiniMrBossMan);
            minion.transform.position = spawn_loc;
        }
    }
}
