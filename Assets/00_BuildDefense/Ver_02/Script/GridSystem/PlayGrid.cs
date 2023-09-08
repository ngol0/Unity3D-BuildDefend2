using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid : GridBase
{
    [Header("UI")]
    [SerializeField] GridItemUI gridItemPrefab;

    GridSystem<GridItem> gridSystem;
    private GridItemUI[,] gridItemUIArray;

    private void Awake()
    {
        //delegate using lambda
        gridSystem = new GridSystem<GridItem>(gridStats.gridWidth, gridStats.gridHeight, gridStats.cellSize,
            (GridSystem<GridItem> g, GridPosition gridPos) => new GridItem(g, gridPos)
        );
    }

    private void Start() 
    {
        CreateGridUI(gridItemPrefab, transform);
        InitialSetUp();
    }

    public void CreateGridUI(GridItemUI gridItemPrefab, Transform root)
    {
        gridItemUIArray = new GridItemUI[gridStats.gridWidth, gridStats.gridHeight];
        for (int x = 0; x < gridStats.gridWidth; x++)
        {
            for (int z = 0; z < gridStats.gridHeight; z++)
            {
                GridPosition gridPos = new(x, z);

                //create grid ui
                GridItemUI gridItemUI =
                    Instantiate<GridItemUI>(gridItemPrefab, GetWorldPosition(gridPos), Quaternion.identity, root);
                gridItemUI.SetGridItem(GetGridItem(gridPos));
                gridItemUI.HideValidMesh();

                gridItemUIArray[x, z] = gridItemUI;
            }
        }
    }
    
    public void ShowValidGridPositionUI(List<GridPosition> gridBoundary)
    {
        foreach (var position in gridBoundary)
        {
            gridItemUIArray[position.x, position.z].ShowValidMesh();
        }
    }

    public void HideValidGridPositionUI(List<GridPosition> gridBoundary)
    {
        foreach (var position in gridBoundary)
        {
            gridItemUIArray[position.x, position.z].HideValidMesh();
        }
    }

    public override void SetItemAtGrid(IGameItem item, GridPosition gridPos)
    {
        GetGridItem(gridPos).SetItem(item);
    }

    public void RemoveItemAtGrid(GridPosition gridPos)
    {
        GetGridItem(gridPos).SetItem(null);
    }

    public void ItemMoveGridPosition(InteractableItem item, GridPosition fromGridPos, GridPosition toGridPos)
    {
        RemoveItemAtGrid(fromGridPos);
        SetItemAtGrid(item, toGridPos);
    }

    public bool IsGridItemPlaceable(GridPosition gridPos)
    {
        return GetGridItem(gridPos).IsPlaceable();
    }

    public bool HasUnit(GridPosition gridPos)
    {
        return GetGridItem(gridPos).Item is Unit;
    }

    public GridItem GetGridItem(GridPosition gridPosition) => gridSystem.GetGridItem(gridPosition);
    public override GridPosition GetGridPosition(Vector3 worldPos) => gridSystem.GetGridPosition(worldPos);
    public override Vector3 GetWorldPosition(GridPosition gridPos) => gridSystem.GetWorldPosition(gridPos);
}
