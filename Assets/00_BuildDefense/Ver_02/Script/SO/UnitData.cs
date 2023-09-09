using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InteractableData/Unit")]
public class UnitData : InteractableData
{
    public float health;

    public float distanceOffset = 0.1f;
    public float damageToDeal = 2f;

    public float hitRange = 0;
}
