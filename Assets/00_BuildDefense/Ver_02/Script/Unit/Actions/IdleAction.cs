using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : MonoBehaviour, BaseAction
{
    private ActionScheduler actionScheduler;
    
    private void Start() 
    {
        actionScheduler = GetComponent<ActionScheduler>();
    }

    public void StartAction()
    { 
        actionScheduler.StartAction(this);
    }

    public void Cancel()
    {
        
    }
}
