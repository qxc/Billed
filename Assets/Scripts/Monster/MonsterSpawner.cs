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
    public GameObject boss;
    public GameObject prefab_boss;
    public GameObject MiniMrBossMan;
    public GameObject DiveMinionCritHead;
    private GameObject player1;
    private float xSpawnDistance = 5f;
    private float ySpawnDistance = 7f;
    bool initialized = false;
    private int current_level;
    private int current_phase;
    // Start is called before the first frame update
    void Start() {
        Initialize();
    }

    void Initialize() {
        if (initialized) {
            return;
        }
        last_spawn_time = Time.time;
        spawn_cooldown_temporary_mod = Random.Range(0f, spawn_cooldown_temporary_range);
        spawn_cooldown = base_spawn_cooldown + spawn_cooldown_temporary_mod;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        getCurrentLevelPhase();
        player1 = gameManager.Player1;
        initialized = true;
    }

    void getCurrentLevelPhase()
    {
        current_level = gameManager.current_level;
        current_phase = gameManager.current_phase;
    }

    // Update is called once per frame
    void Update()
    {
        getCurrentLevelPhase();
        if (current_phase != 1) {
            return;
        }        
        if (current_level == 1) {
            Level1();
        }
        if (current_level == 2) {
            Level2();
        }
    }
    public void Level1() {
        if (last_spawn_time + spawn_cooldown < Time.time && gameManager.is_spawning_minions) {
            GameObject minion;
            last_spawn_time = Time.time;
            spawn_cooldown_temporary_mod = Random.Range(0f, spawn_cooldown_temporary_range);
            spawn_cooldown = base_spawn_cooldown + spawn_cooldown_temporary_mod;
            float spawnXMod = Random.Range(-xSpawnDistance, xSpawnDistance);
            float spawnYMod = Random.Range(0, ySpawnDistance);
            Vector3 spawn_loc = new Vector3(player1.transform.position.x + spawnXMod, player1.transform.position.y + spawnYMod, player1.transform.position.z);
            int randomMinionChoice = Random.Range(0, 2);
            Debug.Log(randomMinionChoice);
            if ( randomMinionChoice == 0 ) {
                minion = Instantiate(MiniMrBossMan);
            }
            else {
                minion = Instantiate(DiveMinionCritHead);
            }
            minion.transform.position = spawn_loc;
            gameManager.current_living_minions++;
        }
    }
    public void Level2() {
        if (boss == null) {
            float spawnXMod = Random.Range(-xSpawnDistance, xSpawnDistance);
            float spawnYMod = Random.Range(0, ySpawnDistance);
            Vector3 spawn_loc = new Vector3(player1.transform.position.x + spawnXMod, player1.transform.position.y + spawnYMod, player1.transform.position.z);
            boss = Instantiate(prefab_boss);
            boss.transform.position = spawn_loc;
        }
    }
}
