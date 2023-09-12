using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    private PlayGrid playGrid;
    private Unit unit;
    private Vector3 targetPos;
    private Vector3 moveDirection;

    private Queue<GridPosition> gridTargets = new();
    private List<GridPosition> paths = new();
    private Pathfinding pathfinding;
    private bool setNextTarget = false;
    private bool isWaiting = false;

    private void Awake()
    {
        unit = GetComponentInParent<Unit>();
        Debug.Log(unit.name);
    }

    private void Start()
    {
        targetPos = transform.position;
        playGrid = unit.PlayGrid;
        pathfinding = unit.PathfindingGrid;
    }

    public void StartAction()
    {
        unit.actionScheduler.StartAction(this);
        SetPath();
    }

    private void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, targetPos));
        SetNextWaypoint();
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        if (Vector3.Distance(transform.position, targetPos) > unit.unitData.distanceOffset)
        {
            moveDirection = (targetPos - transform.position).normalized;
            unit.transform.position += unit.unitData.moveSpeed * Time.deltaTime * moveDirection;

            unit.animatorController.SetBool("isWalking", true);

            float rotatingSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotatingSpeed);
        }
        else if (isWaiting)
        {
            unit.animatorController.SetBool("isWalking", false);
        }
        else
        {
            setNextTarget = true;
        }
    }

    void SetNextWaypoint()
    {
        if (setNextTarget)
        {
            if (gridTargets.Count > 0)
            {
                var gridTarget = gridTargets.Dequeue();
                CheckObstaclesOnNextNode(gridTarget);
            }
            //endpoint
            if (gridTargets.Count == 0)
            {
                unit.animatorController.SetBool("isWalking", false);
            }
            setNextTarget = false;
        }
    }

    //todo: find another more efficient way??
    private void CheckObstaclesOnNextNode(GridPosition gridTarget)
    {
        if (HasUnitOnTarget(gridTarget)) 
        {
            unit.GetAction<IdleAction>().StartAction();
            return;
        }

        if (pathfinding.IsNodeWalkable(gridTarget))
        {
            targetPos = playGrid.GetWorldPosition(gridTarget);

            //when target is set -> set current grid pos for unit
            playGrid.ItemMoveGridPosition(unit, unit.CurGridPos, gridTarget);
            //pathfinding.ItemMoveGridPosition(unit, unit.CurGridPos, gridTarget);
            unit.SetCurrentGridPos(gridTarget);
        }
        else
        {
            SetPath();
        }
    }

    private bool HasUnitOnTarget(GridPosition gridPos)
    {
        return playGrid.HasUnit(gridPos);    
    }

    public void EnqueuePathPoints(List<GridPosition> positionTargets)
    {
        if (positionTargets == null)
        {
            Debug.Log(":::Can't find path???");
            return;
        }

        gridTargets.Clear();

        for (int i = 1; i < positionTargets.Count; i++)
        {
            gridTargets.Enqueue(positionTargets[i]);
        }
    }

    private void SetPath()
    {
        GridPosition destination = GetDestination(unit.CurGridPos);
        //Debug.Log(":::Destination set: " + destination.x + ", " + destination.z);

        paths = pathfinding.FindPath(unit.CurGridPos, destination);
        EnqueuePathPoints(paths);
    }

    public GridPosition GetDestination(GridPosition curGridPos)
    {
        //get the farthest available tile in row
        for (int i = playGrid.gridStats.gridWidth - 1; i > curGridPos.x; i--)
        {
            GridPosition gridPos = new GridPosition(i, curGridPos.z);
            if (pathfinding.GetNode(gridPos).IsWalkable())
            {
                return gridPos;
            }
        }
        return curGridPos;
    }

    public override void Cancel()
    {
        gridTargets.Clear();
    }

    public override void Wait()
    {
        isWaiting = true;
        Debug.Log(222);
    }

    public override void Presume()
    {
        isWaiting = false;
    }
}
