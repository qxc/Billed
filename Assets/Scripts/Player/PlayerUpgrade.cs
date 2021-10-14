using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade {

    public string upgradeName;
    public float attackModifier;
    public float healthModifier;

    // Start is called before the first frame update
    public PlayerUpgrade(string upName, float attackMod, float healthMod) {
        attackModifier = attackMod;
        healthModifier = healthMod;
        upgradeName = upName;
    }
}
