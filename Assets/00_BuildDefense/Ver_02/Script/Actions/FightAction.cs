using System;
using System.Collections.Generic;
using Lam.DefenderBuilder.Enemies;
using UnityEngine;

public class FightAction : BaseAction
{
    private Unit unit;
    private Health enemy;
    WeaponBase weapon;
    private void Update()
    {
        if (enemy!= null)
        {
            AttackBehavior();
        }
    }

    public void StartAction(Health enemy, WeaponBase weapon, Unit unit)
    {
        if (this.weapon != null) 
        { 
            SwitchAttackWeapon(weapon); 
            return;
        }
        this.weapon = weapon;
        this.unit = unit;
        
        Debug.Log(":::Prepare to attack");
        unit.actionScheduler.StartFighting(this);

        this.enemy = enemy;
        enemy.OnDie += Cancel;
        enemy.OnClearPath += OnComplete;
    }

    private void SwitchAttackWeapon(WeaponBase weapon)
    {
        Debug.Log(":::Switching");
        unit.animatorController.ResetTrigger(this.weapon.animationStartString);
        unit.animatorController.SetTrigger(this.weapon.animationStopString);

        this.weapon = weapon;
    }

    private void TriggerAttack()
    {
        unit.animatorController.ResetTrigger(weapon.animationStopString);
        unit.animatorController.SetTrigger(weapon.animationStartString);
    }

    // private bool IsInRange()
    // {
    //     return (weapon.minHitRange + weapon.offSet) <= Vector3.Distance(transform.position, enemy.transform.position) 
    //             && Vector3.Distance(transform.position, enemy.transform.position) <= (weapon.maxHitRange + weapon.offSet);
    // }

    public void AttackBehavior()
    {
        if (enemy == null) return;
        unit.transform.LookAt(enemy.transform.position);
        TriggerAttack();
    }

    public override void Cancel()
    {
        unit.animatorController.ResetTrigger(weapon.animationStartString);
        unit.animatorController.SetTrigger(weapon.animationStopString);

        enemy = null;
        weapon = null;
    }

    //animation event
    private void Hit()
    {
        if (enemy == null) return;
        enemy.TakeDamage(gameObject, weapon.damage);
    }

    public override void Wait()
    {

    }

    public override void Presume()
    {

    }
}
