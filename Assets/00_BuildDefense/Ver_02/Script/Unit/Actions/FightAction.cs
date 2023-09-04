using System.Collections;
using System.Collections.Generic;
using Lam.DefenderBuilder.Enemies;
using UnityEngine;

public class FightAction : MonoBehaviour, BaseAction
{
    private Unit unit;
    private Health enemy;
    private bool canAttack = false;

    private void Start() 
    {
        unit = GetComponent<Unit>();
        unit.GetAction<MoveAction>().OnDoneMoving += CanAttack;
    }

    private void Update() 
    {
        if (canAttack)
        {
            AttackBehavior();
        }
    }

    public void StartAction(Health enemy)
    { 
        Debug.Log(":::Prepare to attack");
        unit.actionScheduler.StartAction(this);

        this.enemy = enemy;
        enemy.OnDie += Cancel;
    }

    private void TriggerAttack()
    {
        unit.animatorController.ResetTrigger("stopAttack");
        unit.animatorController.SetTrigger("startAttack");
    }

    private void CanAttack()
    {
        if (enemy == null) return;
        canAttack = true;
    }

    private void AttackBehavior()
    {
        if (enemy == null) return;
        unit.transform.LookAt(enemy.transform.position);
        TriggerAttack();
    }

    public void Cancel()
    {
        unit.animatorController.ResetTrigger("startAttack");
        unit.animatorController.SetTrigger("stopAttack");

        enemy = null;
        canAttack = false;
    }

    //animation event
    private void Hit()
    {
        enemy.TakeDamage(gameObject, 5f);
    }
}
