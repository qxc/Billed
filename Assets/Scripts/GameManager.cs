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
    // Start is called before the first frame update
    void Start() {
        currentPhase = 0;
        combatPhaseLength = 12;
        phaseStartTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(currentPhase);
        if (Input.GetButtonDown("Select") && currentPhase == 0) {
            Debug.Log("NEXT PHASE!");
            currentPhase = 1;
            phaseStartTime = Time.time;
            MonsterSpawner.SetActive(true);
        }
        if ( currentPhase == 1 && phaseStartTime + combatPhaseLength < Time.time ) {
            Debug.Log("combat phase has ended");
            MonsterSpawner.SetActive(false);

        }
    }
}
