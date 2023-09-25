using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    BaseAction currentAction;
    public Action<ControlledActionType> OnActionChange;
    bool isFighting;

    public void StartAction(BaseAction action)
    {
        if (currentAction == action) return;
        
        if (currentAction != null) 
        {
            currentAction.Cancel();
        }
        currentAction = action;
        OnActionChange?.Invoke(currentAction.actionType);

        if (isFighting) 
        {
            Debug.Log("waiting???");
            currentAction.Wait();
        }
    }

    public void CancelAllAction()
    {
        StartAction(null);
    }

    public void StartFighting(BaseAction fightAction)
    {
        if (currentAction == fightAction) return;

        if (currentAction != null)
        {
            currentAction.Wait();
            fightAction.OnComplete += currentAction.Presume;
            fightAction.OnComplete += OnDoneFighting;
        }
        isFighting = true;
    }

    public ControlledActionType GetActionType()
    {
        return currentAction.actionType;
    }

    private void OnDoneFighting()
    {
        isFighting = false;
    }
}
