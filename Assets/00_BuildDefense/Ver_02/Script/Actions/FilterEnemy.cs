using System.Collections;
using System.Collections.Generic;
using Lam.DefenderBuilder.Enemies;
using UnityEngine;

public class FilterEnemy : MonoBehaviour
{
    [SerializeField] Unit unit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            var enemy = other.GetComponent<Health>();
            if (enemy && !enemy.IsDead())
            {
                unit.GetAction<FightAction>().StartAction(enemy);
            }
        }
        // else if (other.tag == "Unit")
        // {
        //     unit.GetAction<IdleAction>().StartAction();
        // }
    }

    // private void OnTriggerExit(Collider other) 
    // {
    //     if (other.tag == "Unit")
    //     {
    //         unit.GetAction<MoveAction>().StartAction();
    //     }
    // }
}
