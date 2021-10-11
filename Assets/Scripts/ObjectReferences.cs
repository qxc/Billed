using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferences : MonoBehaviour {
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Boss;
    public GameObject MonsterSpawner;
    [HideInInspector]
    public int currentPhase; // 0/1/2 == building/combat/levelup
    // Start is called before the first frame update
    void Start() {
        currentPhase = 0;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(currentPhase);
        if (Input.GetButtonDown("Select") && currentPhase == 0) {
            Debug.Log("NEXT PHASE!");
            currentPhase = 1;
            MonsterSpawner.SetActive(true);
        }
    }
}
