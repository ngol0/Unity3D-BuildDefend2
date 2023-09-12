using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponBase", menuName = "WeaponBase/Weapon", order = 0)]
public class WeaponBase : ScriptableObject 
{
    // public float minHitRange;
    // public float maxHitRange;
    // public float offSet;
    public float damage;
    public string animationStartString;
    public string animationStopString;
}

