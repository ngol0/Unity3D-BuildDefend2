using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAction : MonoBehaviour, BaseAction
{
    private Unit unit;
    
    private void Start() 
    {
        unit = GetComponent<Unit>();
    }

    public void StartAction()
    { 
        Debug.Log("Attacking");
        unit.actionScheduler.StartAction(this);

        unit.animatorController.ResetTrigger("stopAttack");
        unit.animatorController.SetTrigger("startAttack");
    }

    public void Cancel()
    {
        unit.animatorController.ResetTrigger("startAttack");
        unit.animatorController.SetTrigger("stopAttack");
    }
}
