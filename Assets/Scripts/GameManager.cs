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
    public bool is_boss_fight;
    // Start is called before the first frame update
    void Start() {
        currentPhase = 0;
        combatPhaseLength = 12;
        phaseStartTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Select") && currentPhase == 0) {
            Debug.Log("NEXT PHASE!");
            currentPhase = 1;
            phaseStartTime = Time.time;
            MonsterSpawner.SetActive(true);
        }
        if ( currentPhase == 1 && phaseStartTime + combatPhaseLength < Time.time ) {
            Debug.Log("combat phase has ended");
            MonsterSpawner.SetActive(false);
            GameObject[] minions = GameObject.FindGameObjectsWithTag("Boss");
            foreach (GameObject minion in minions) {
                Debug.Log(minion);
                BossHealth bh = minion.GetComponentInChildren<BossHealth>();
                Debug.Log(bh);
                bh.die();
            }
            currentPhase = 2;
        }

        if ( currentPhase == 2 ) {
            PlayerUpgrades pupgrades = Player1.GetComponent<PlayerUpgrades>();
            pupgrades.addUpgrade(new PlayerUpgrade("Generic Levelup", 1f, 10f));
            Player1.GetComponent<PlayerBlockPlace>().all_blocks[0].current_stock += 10;
            Player1.GetComponent<PlayerBlockPlace>().all_blocks[1].current_stock += 10;
            currentPhase = 0;
        }
    }
}
