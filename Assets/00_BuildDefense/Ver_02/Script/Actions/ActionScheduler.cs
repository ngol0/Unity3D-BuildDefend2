using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    BaseAction currentAction;
    public Action<ControlledActionType> OnActionChange;

    public void StartAction(BaseAction action)
    {
        if (currentAction == action) return;
        if (currentAction != null) currentAction.Cancel();
        currentAction = action;

        OnActionChange?.Invoke(currentAction.actionType);
    }

    public void CancelAllAction()
    {
        StartAction(null);
    }

    public void QueueAction(BaseAction action)
    {
        if (currentAction == action) return;

        if (currentAction != null)
        {
            currentAction.Wait();
            action.OnComplete += currentAction.Presume;
        }
    }

    public ControlledActionType GetActionType()
    {
        return currentAction.actionType;
    }
}
