using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InteractableData/Unit")]
public class UnitData : InteractableData
{
    [Header("Movement")]
    public float moveSpeed = 2f;
     public float distanceOffset = 0.1f;
    
    [Header("Combat")]
    public float health;
}
