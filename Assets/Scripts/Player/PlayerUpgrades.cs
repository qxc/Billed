using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour {
    public float attackModifier;
    public float healthModifier;
    public PlayerHealth playerHealth;
    public List<PlayerUpgrade> upgrades = new List<PlayerUpgrade>();

    // Start is called before the first frame update
    void Start() {
        playerHealth = GetComponent<PlayerHealth>(); 
    }

    // Update is called once per frame
    void Update() {
        
    }
    public void addUpgrade(PlayerUpgrade upgrade) {
        upgrades.Add(upgrade);
        recalculateModifiers();
    }
    void recalculateModifiers() {
        //Debug.Log("Recalc");
        attackModifier = 0f;
        healthModifier = 0f;
        foreach (PlayerUpgrade playerUpgrade in upgrades) {
            attackModifier += playerUpgrade.attackModifier;
            healthModifier += playerUpgrade.healthModifier;

        }
        playerHealth.max_health = playerHealth.base_max_health + healthModifier;
        //Debug.Log(attackModifier);
        //Debug.Log(healthModifier);

    }
}
