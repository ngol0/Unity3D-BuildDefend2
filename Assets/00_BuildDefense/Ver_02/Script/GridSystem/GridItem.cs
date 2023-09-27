using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem
{
    private HexGridSystem<GridItem> gridSystem;
    private GridPosition gridPosition;
    private IGameItem item;
    public IGameItem Item => item;

    public GridItem(HexGridSystem<GridItem> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public void SetItem(IGameItem item)
    {
        this.item = item;
    }

    public bool IsPlaceable()
    {
        return item==null;
    }

    public override string ToString()
    {
        if (item!=null) //return item.GetName();
            return "X";
        else return gridPosition.ToString();
    }
}
