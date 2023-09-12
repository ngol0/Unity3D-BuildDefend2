using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : BaseAction
{
    private ActionScheduler actionScheduler;

    private void Start() 
    {
        actionScheduler = GetComponent<ActionScheduler>();
        StartAction();
    }

    public void StartAction()
    { 
        actionScheduler.StartAction(this);
    }

    public override void Cancel()
    {
        
    }

    public override void Wait()
    {
        
    }

    public override void Presume()
    {
        
    }
}
