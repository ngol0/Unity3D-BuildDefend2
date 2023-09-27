using System;
using System.Collections.Generic;
using UnityEngine;

public class HexGridSystem<TGridItem>
{
    private int gridHeight;
    private int gridWidth;
    private int cellSize;
    private TGridItem[,] gridItemArray;
    private const float VERTICAL_GRID_OFFSET_MULTIPLIER = 0.75f;

    public HexGridSystem(int gridWidth, int gridHeight, int cellSize, Func<HexGridSystem<TGridItem>, GridPosition, TGridItem> createGridItem)
    {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        this.cellSize = cellSize;

        gridItemArray = new TGridItem[gridWidth, gridHeight];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GridPosition gridPos = new(x,z);

                //create grid array data
                gridItemArray[x,z] = createGridItem(this, gridPos);

            }
        }
    }

    public TGridItem GetGridItem(GridPosition gridPos)
    {
        return gridItemArray[gridPos.x, gridPos.z];
    }

    public Vector3 GetWorldPosition(GridPosition gridPos)
    {
        return new Vector3(gridPos.x, 0, 0) * cellSize + 
            ((gridPos.z % 2 == 1) ? new Vector3(1,0,0) * cellSize/2 : Vector3.zero)
            + VERTICAL_GRID_OFFSET_MULTIPLIER * cellSize * new Vector3(0,0,gridPos.z);
    }

    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        GridPosition roughGridPos = new GridPosition(
            Mathf.RoundToInt(worldPos.x/cellSize), 
            Mathf.RoundToInt(worldPos.z/cellSize/VERTICAL_GRID_OFFSET_MULTIPLIER)
        );

        bool oddRow = roughGridPos.z % 2 == 1;

        List<GridPosition> neighborPos = new List<GridPosition>
        {
            roughGridPos + new GridPosition(0,1),
            roughGridPos + new GridPosition(0,-1),

            roughGridPos + new GridPosition(-1,0),
            roughGridPos + new GridPosition(1,0),

            roughGridPos + new GridPosition(oddRow ? 1 : -1, 1),
            roughGridPos + new GridPosition(oddRow ? 1 : -1, -1),
        };

        GridPosition closestGridPos = roughGridPos;

        foreach (var pos in neighborPos)
        {
            if (Vector3.Distance(worldPos, GetWorldPosition(pos)) < Vector3.Distance(worldPos, GetWorldPosition(closestGridPos)))
            {
                closestGridPos = pos;
            }
        }

        return closestGridPos;
    }

    public bool IsValidGridPos(GridPosition gridPos)
    {
        return 0 <= gridPos.x 
            && gridPos.x <= gridWidth-1     
            && 0 <= gridPos.z 
            && gridPos.z <= gridHeight-1;
    }
}
