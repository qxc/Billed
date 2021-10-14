using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Boss;
    public GameObject MonsterSpawner;
    public float combatPhaseLength;
    [HideInInspector]
    public int currentPhase; // 0/1/2 == building/combat/levelup
    private float phaseStartTime;
    private int current_level = 2;
    public bool is_boss_fight;
    // Start is called before the first frame update
    void Start() {
        currentPhase = 0;
        combatPhaseLength = 2;
        phaseStartTime = Time.time;
    }

    // Spawning minions
    public void Level1() {
        if (Input.GetButtonDown("Select") && currentPhase == 0)
        {
            Debug.Log("NEXT PHASE!");
            currentPhase = 1;
            phaseStartTime = Time.time;
            MonsterSpawner.SetActive(true);
        }
        if (currentPhase == 1 && phaseStartTime + combatPhaseLength < Time.time)
        {
            Debug.Log("combat phase has ended");
            MonsterSpawner.SetActive(false);
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Boss");
            foreach (GameObject minion in minions)
            {
                BossHealth bh = minion.GetComponentInChildren<BossHealth>();
                bh.die();
            }
            currentPhase = 2;
        }

        if (currentPhase == 2)
        {
            PlayerUpgrades pupgrades = Player1.GetComponentInChildren<PlayerUpgrades>();
            pupgrades.addUpgrade(new PlayerUpgrade("Generic Levelup", 1f, 10f));
            Player1.GetComponent<PlayerBlockPlace>().all_blocks[0].current_stock += 10;
            Player1.GetComponent<PlayerBlockPlace>().all_blocks[1].current_stock += 10;
            currentPhase = 0;
            current_level = 2;
        }
    }
    
    // First boss level
    public void Level2() {
        if (Input.GetButtonDown("Select") && currentPhase == 0)
        {
            MonsterSpawner.SetActive(true);
            Debug.Log("Starting boss battle");
            currentPhase = 1;
            phaseStartTime = Time.time;
            //Spawn boss
        }
        if (currentPhase == 1) // check if boss is dead
        {
            
            MonsterSpawner.GetComponent<MonsterSpawner>().spawn_boss();
            Debug.Log("combat phase has ended");
            currentPhase = 2;
        }

        if (currentPhase == 2)
        {
            Debug.Log("You won!");
        }
    }

    // Update is called once per frame
    void Update() {
        if (current_level == 1)
        {
            Level1();
        } else if (current_level == 2) {
            Level2();
        }
        
    }
}
