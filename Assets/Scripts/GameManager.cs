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
    // 0/1/2 == building/combat/levelup
    public int current_phase = 0, current_level = 1; 
    private float phaseStartTime;
    // Start is called before the first frame update
    void Start() {
        current_phase = 0;
        current_level = 1;
        combatPhaseLength = 30;
        phaseStartTime = Time.time;
    }

    // Spawning minions
    public void Level1() {
        if (Input.GetButtonDown("Select") && current_phase == 0) {
            Debug.Log("building phase ended!");
            current_phase = 1;
            phaseStartTime = Time.time;
        }
        if (current_phase == 1 && phaseStartTime + combatPhaseLength < Time.time) {
            Debug.Log("combat phase has ended");
            current_phase = 2;
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Boss");
            foreach (GameObject minion in minions) {
                Debug.Log(minion);
                IMonsterHealth mh = minion.GetComponent<IMonsterHealth>();
                Debug.Log(mh);
                mh.die();
            }
        }

        if (current_phase == 2) {
            PlayerUpgrades pupgrades = Player1.GetComponentInChildren<PlayerUpgrades>();
            pupgrades.addUpgrade(new PlayerUpgrade("Generic Levelup", 1f, 10f));
            Player1.GetComponent<PlayerBlockPlace>().all_blocks[0].current_stock += 10;
            Player1.GetComponent<PlayerBlockPlace>().all_blocks[1].current_stock += 10;
            current_phase = 0;
            current_level = 2;
        }
    }

    public void minionDied() {
        if (current_level == 1) {

        }

        if (current_level == 2) {
            current_phase = 2;
        }
    }
    
    // First boss level
    public void Level2() {
        if (Input.GetButtonDown("Select") && current_phase == 0) {
            Debug.Log("Starting boss battle");
            current_phase = 1;
            phaseStartTime = Time.time;
        }
        if (current_phase == 1) { // check if boss is dead 
            if (Boss == null) {
                Boss = MonsterSpawner.GetComponent<MonsterSpawner>().boss;
            } 
        }

        if (current_phase == 2) {
            Debug.Log("You won!");
        }
    }

    // Update is called once per frame
    void Update() {
        if (current_level == 1) {
            Level1();
        } else if (current_level == 2) {
            Level2();
        }
        
    }
}
