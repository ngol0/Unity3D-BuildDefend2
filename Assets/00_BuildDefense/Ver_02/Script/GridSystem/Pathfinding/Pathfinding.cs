using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : GridBase
{
    //[SerializeField] PlayGrid playGrid;
    //[SerializeField] GridItemUI pathNodePrefab;

    HexGridSystem<PathNode> pathSystem;
    public HexGridSystem<PathNode> PathSystem => pathSystem;

    private const int STRAIGHT_MOVE_COST = 10;

    private void Awake()
    {
        //delegate using anonymous function
        pathSystem = new HexGridSystem<PathNode>
            (gridStats.gridWidth, gridStats.gridHeight, gridStats.cellSize,
                delegate (HexGridSystem<PathNode> g, GridPosition gridPos)
                {
                    return new PathNode(g, gridPos);
                }
            );

        //pathSystem.CreateGridUI(pathNodePrefab, transform);
    }

    private void Start() 
    {
        InitialSetUp();
    }

    public override void SetItemAtGrid(IGameItem item, GridPosition gridPos)
    {
        GetNode(gridPos).SetItem(item);
    }

    public void RemoveItemAtGrid(GridPosition gridPos)
    {
        GetNode(gridPos).SetItem(null);
    }

    public void ItemMoveGridPosition(InteractableItem item, GridPosition fromGridPos, GridPosition toGridPos)
    {
        RemoveItemAtGrid(fromGridPos);
        SetItemAtGrid(item, toGridPos);
    }

    public bool IsNodeWalkable(GridPosition gridPos)
    {
        return GetNode(gridPos).IsWalkable();
    }

    //a-star algorithm
    public List<GridPosition> FindPath(GridPosition startPos, GridPosition endPos)
    {
        List<PathNode> openList = new();
        List<PathNode> closedList = new();

        PathNode startNode = GetNode(startPos);
        PathNode endNode = GetNode(endPos);

        openList.Add(startNode);

        ResetPathfindingData();

        startNode.SetGCost(0);
        startNode.SetHCost(CalculateHeuristicDistance(startPos, endPos));
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetNodeWithLowestFCost(openList);
            if (closedList.Contains(currentNode)) //if already searched
            {
                continue;
            }

            if (currentNode == endNode)
            {
                //Debug.Log("reached here???");
                return CalculatePath(currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //look at all neighbors
            foreach (var neighborNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighborNode)) continue;
                if (!neighborNode.IsWalkable())
                {
                    closedList.Add(neighborNode);
                    continue;
                }

                int currentGCost = currentNode.GCost + STRAIGHT_MOVE_COST;

                if (currentGCost < neighborNode.GCost)
                {
                    //reset g h and f cost
                    neighborNode.SetGCost(currentGCost);
                    neighborNode.SetHCost(CalculateHeuristicDistance(neighborNode.GridPos, endPos));
                    neighborNode.CalculateFCost();
                    neighborNode.SetCameFromNode(currentNode);

                    if (!openList.Contains(neighborNode)) 
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        return null;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new()
        {
            endNode
        };

        while (endNode.CameFromNode != null)
        {
            path.Add(endNode.CameFromNode);
            endNode = endNode.CameFromNode;
        }
        
        path.Reverse();

        List<GridPosition> gridPosList = new();
        foreach (var item in path)
        {
            gridPosList.Add(item.GridPos);
        }

        return gridPosList;
    }

    private void ResetPathfindingData()
    {
        for (int x = 0; x < gridStats.gridWidth; x++)
        {
            for (int z = 0; z < gridStats.gridHeight; z++)
            {
                GridPosition gridPos = new(x, z);
                PathNode node = GetNode(gridPos);
                node.SetCameFromNode(null);
                node.SetGCost(int.MaxValue);
                node.SetHCost(0);
                node.CalculateFCost();
            }
        }
    }

    private PathNode GetNodeWithLowestFCost(List<PathNode> listNodes)
    {
        PathNode lowestNode = listNodes[0];
        for (int i = 1; i < listNodes.Count; i++)
        {
            if (listNodes[i].CalculateFCost() < lowestNode.CalculateFCost())
            {
                lowestNode = listNodes[i];
            }
        }
        return lowestNode;
    }

    //diagnoal distance function for heuristics cost (HCost) & GCost
    private int CalculateHeuristicDistance(GridPosition currentPos, GridPosition endPos)
    {
        return Mathf.RoundToInt(STRAIGHT_MOVE_COST * Vector3.Distance(GetWorldPosition(currentPos), GetWorldPosition(endPos)));
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new();

        GridPosition gridPosition = currentNode.GridPos;

        for (int x = -1; x <=1; x++)
        {
            for (int z = -1; z <=1; z++)
            {
                if (Mathf.Abs(x) == Mathf.Abs(z)) continue;
                GridPosition potentialNeighbor = gridPosition + new GridPosition(x, z);
                AddToNeighborList(neighbourList, potentialNeighbor);
            }
        }

        bool oddRow = gridPosition.z % 2 == 1;
        GridPosition anotherPotentialGrid1 = gridPosition + new GridPosition(oddRow ? 1 : -1, 1);
        GridPosition anotherPotentialGrid2 = gridPosition + new GridPosition(oddRow ? 1 : -1, -1);

        AddToNeighborList(neighbourList, anotherPotentialGrid1);
        AddToNeighborList(neighbourList, anotherPotentialGrid2);

        return neighbourList;
    }

    private void AddToNeighborList(List<PathNode> neighbourList, GridPosition potentialNeighbor)
    {
        if (pathSystem.IsValidGridPos(potentialNeighbor))
        {
            neighbourList.Add(GetNode(potentialNeighbor));
        }
    }

    public PathNode GetNode(GridPosition gridPos) => pathSystem.GetGridItem(gridPos);
    public override Vector3 GetWorldPosition(GridPosition gridPos) => pathSystem.GetWorldPosition(gridPos);
    public override GridPosition GetGridPosition(Vector3 worldPos) => pathSystem.GetGridPosition(worldPos);
}
