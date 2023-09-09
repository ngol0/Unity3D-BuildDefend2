using System;
using System.Collections.Generic;
using Lam.DefenderBuilder.Enemies;
using UnityEngine;

public class FightAction : BaseAction
{
    float attackingRange;
    private Unit unit;
    private Health enemy;

    private void Start()
    {
        unit = GetComponent<Unit>();
        attackingRange = unit.unitData.hitRange + unit.unitData.distanceOffset;
        Debug.Log(attackingRange);
    }

    private void Update()
    {
        if (IsInRange())
        {
            AttackBehavior();
        }
    }

    public void StartAction(Health enemy)
    {
        Debug.Log(":::Prepare to attack");
        unit.actionScheduler.QueueAction(this);

        this.enemy = enemy;
        enemy.OnDie += Cancel;
        enemy.OnClearPath += OnComplete;
    }

    private void TriggerAttack()
    {
        unit.animatorController.ResetTrigger("stopAttack");
        unit.animatorController.SetTrigger("startAttack");
    }

    private bool IsInRange()
    {
        return enemy!=null && Vector3.Distance(transform.position, enemy.transform.position) <= attackingRange;
    }

    private void AttackBehavior()
    {
        if (enemy == null) return;
        unit.transform.LookAt(enemy.transform.position);
        TriggerAttack();
    }

    public override void Cancel()
    {
        unit.animatorController.ResetTrigger("startAttack");
        unit.animatorController.SetTrigger("stopAttack");

        enemy = null;
    }

    //animation event
    private void Hit()
    {
        enemy.TakeDamage(gameObject, unit.unitData.damageToDeal);
    }

    public override void Wait()
    {

    }

    public override void Presume()
    {

    }
}
