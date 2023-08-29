using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPlacement : MonoBehaviour
{
    [Header("Inventory Data")]
    [SerializeField] InventorySO inventory;

    [Header("Grid Ref")]
    [SerializeField] PlayGrid playGrid;
    [SerializeField] Pathfinding pathfindingGrid;
    InteractableItem activePlaceableItem; //item that is waiting to be confirmed to place

    public Action OnTryPlacingResourceItem;
    public Action OnDoneDeciding;
    public Action OnItemPlaced;
    public Action OnCancelPlacedItem;
    private InteractableData itemToPlaceInfo; //data of item to place

    public bool CanPlaceItem(Vector3 worldPos)
    {
        if (itemToPlaceInfo == null) return false;
        GridPosition gridPos = playGrid.GetGridPosition(worldPos);
        GridItem gridItem = playGrid.GetGridItem(gridPos);

        if (!gridItem.IsPlaceable()) return false;

        InteractableItem item = Instantiate
            (itemToPlaceInfo.prefab,
            playGrid.GetWorldPosition(gridPos), Quaternion.identity);

        item.SetGridData(playGrid, pathfindingGrid);
        CheckItemOnPlace(item);
        return true;
    }

    private void CheckItemOnPlace(InteractableItem item)
    {
        activePlaceableItem = item;

        if (activePlaceableItem is ResourceItem) //if item resource item (i.e: houses)
        {
            activePlaceableItem.GetComponent<ResourceGeneratorRepresentation>().FindResourceNodeNearby(); //find resource to show ++ sign
            OnTryPlacingResourceItem?.Invoke(); //update ui: show yes or no panel
        }
        else if (activePlaceableItem is Unit)
        {
            OnCompletePlacing();
        }
    }

    private void OnCompletePlacing()
    {
        playGrid.SetItemAtGrid(activePlaceableItem, activePlaceableItem.CurGridPos);

        inventory.RemoveInteractableItem(itemToPlaceInfo);
        OnItemPlaced?.Invoke(); //update inventory ui

        SetItemToPlaceInfo(null);
        activePlaceableItem = null;
    }

    //bind to button at Confirmation Panel
    public void OnDecideToPlace(bool isPlaced)
    {
        if (isPlaced)
        {
            //set timer for resource item to start collecting resource
            activePlaceableItem.GetComponent<ResourceGenerator>().SetMaxTimer();
            pathfindingGrid.SetItemAtGrid(activePlaceableItem, activePlaceableItem.CurGridPos);
            OnCompletePlacing();
        }
        else
        {
            Destroy(activePlaceableItem.gameObject);
            CancelPlaceableItem();
        }

        OnDoneDeciding?.Invoke(); //show inventory ui
    }

    //set item to place into grid
    public void SetItemToPlaceInfo(InteractableData activeItemData)
    {
        itemToPlaceInfo = activeItemData;
    }

    public void CancelPlaceableItem()
    {
        itemToPlaceInfo = null;
        OnCancelPlacedItem?.Invoke();
    }
}
