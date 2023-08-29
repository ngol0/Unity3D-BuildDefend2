using System.Collections;
using System.Collections.Generic;
using Lam.DefenderBuilder.Enemies;
using UnityEngine;

public class FilterEnemy : MonoBehaviour
{
    [SerializeField] Unit unit;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<Enemy>())
        {
            unit.GetAction<FightAction>().StartAction();
        }
    }
}
