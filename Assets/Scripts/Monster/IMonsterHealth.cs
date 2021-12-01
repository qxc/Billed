using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterHealth {
    public void get_hit(float damage, string damage_type, bool is_crit);
    public void get_weakspot_hit(float damage, string damage_type);
    public void die();
    public void make_damage_numbers(float damage_taken, bool is_crit);

    public float max_health { get; set; }
    public float current_health { get; set; }
    public GameObject damage_numbers_prefab { get; set; }
}
