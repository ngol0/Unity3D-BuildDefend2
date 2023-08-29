using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Unit : InteractableItem
{
    BaseAction[] actionArrays;
    public Animator animatorController;
    Pathfinding pathfindingGrid;
    public Pathfinding PathfindingGrid => pathfindingGrid;
    public ActionScheduler actionScheduler;

    private void Start()
    {
        actionArrays = GetComponents<BaseAction>();
        transform.forward = Vector3.right;
    }

    public void SetCurrentGridPos(GridPosition newGridPos)
    {
        curGridPos = newGridPos;
    }

    public override void SetGridData(PlayGrid gridSystem, Pathfinding pathGrid)
    {
        base.SetGridData(gridSystem);
        pathfindingGrid = pathGrid;
        Debug.Log(":::Unit placed");
    }

    public T GetAction<T>() where T : BaseAction
    {
        foreach (BaseAction action in actionArrays)
        {
            if (action is T t)
            {
                return t;
            }
        }

        return default; //todo: ?
    }

    //----testing----//
    // public override void SetGridData(PlayGrid gridSystem)
    // {
    //     base.SetGridData(gridSystem);
    //     TestingSetup();
    // }
    // void TestingSetup()
    // {
    //     testing = FindObjectOfType<Testing>();
    //     testing.SetStartingPoint(curGridPos);
    //     testing.Move += TestMove;
    // }

    // void TestMove()
    // {
    //     GetAction<MoveAction>().SetPath(testing.list);
    // }

    //---end testing---//
}
