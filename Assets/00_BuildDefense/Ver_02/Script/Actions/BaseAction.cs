using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    //void StartAction();
    //public abstract void StartAction();
    public abstract void Cancel();
    public abstract void Wait();
    public Action OnComplete;
    public abstract void Presume();
    public ControlledActionType actionType;
}

public enum ControlledActionType
{
    NotControlled,
    MoveStraight,
    MoveUp,
    MoveDown,
    Idle
}

