using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    BaseAction currentAction;
    public void StartAction(BaseAction action)
    {
        if (currentAction == action) return;
        if (currentAction!=null) currentAction.Cancel();
        currentAction = action;
    }

    public void CancelAllAction()
    {
        StartAction(null);
    }
}
