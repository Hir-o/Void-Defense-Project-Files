using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public static class Tags
{
    public static readonly string MAIN_TURRET = "Main Turret";
    public static readonly string ENEMY_TAG = "Enemy";
    public static readonly string DEAD_ENEMY_TAG = "Dead Enemy";
    public static readonly string GROUND_TAG = "Ground";
    
    //Enemy Pool Tags
    public static readonly string POOL_ENEMY = "enemy";
    public static readonly string POOL_ENEMY_BOSS = "enemy_boss";
    
    public static readonly string POOL_ENEMY_DEATH_PARTICLE = "enemy_death_particle";
    public static readonly string POOL_EMP_PARTICLE = "emp_particle";
    public static readonly string POOL_POWER_PARTICLE = "power_particle";
    public static readonly string POOL_CRIT_PARTICLE = "crit_particle";
    public static readonly string POOL_CRIT_TMP = "crit_tmp";
}
