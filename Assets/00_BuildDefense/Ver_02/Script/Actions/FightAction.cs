using System;
using System.Collections.Generic;
using Lam.DefenderBuilder.Enemies;
using UnityEngine;

public class FightAction : BaseAction
{

    private Unit unit;
    private Health enemy;
    private bool canAttack = false;

    private void Start()
    {
        unit = GetComponent<Unit>();
        unit.GetAction<MoveAction>().OnComplete += CanAttack;
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

    public override void Cancel()
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

    public override void Wait()
    {

    }

    public override void Presume()
    {

    }
}
