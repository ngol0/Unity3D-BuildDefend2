using System.Collections;
using System.Collections.Generic;
using Lam.DefenderBuilder.Enemies;
using UnityEngine;

public class FilterEnemy : MonoBehaviour
{
    [SerializeField] Unit unit;

    private void OnTriggerEnter(Collider other) 
    {
        var enemy = other.GetComponent<Health>();
        if (enemy && !enemy.IsDead())
        {
            unit.GetAction<FightAction>().StartAction(enemy);
        }
    }
}
