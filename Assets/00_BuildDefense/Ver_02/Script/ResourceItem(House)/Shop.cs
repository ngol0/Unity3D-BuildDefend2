using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shop : MonoBehaviour
{
    ResourceItem selectedItem;
    public System.Action<ResourceItem, ResourceController> OnSelectedResourceItem;

    [Header("Resources and Inventory")]
    [SerializeField] ResourceController resourceController;
    [SerializeField] InventorySO inventory;
    [SerializeField] InventoryUI inventoryUI;

    public Action<InteractableData> OnAddComplete;

    public void SetSelectedItem(InteractableItem item)
    {
        if (item == selectedItem) return;
        selectedItem = (item is ResourceItem) ? item as ResourceItem : null;
        OnSelectedResourceItem?.Invoke(selectedItem, resourceController);
    }

    public void OnTransaction(InteractableData itemData)
    {
        inventory.AddToInteractableList(itemData, inventoryUI.InitItemInInventory);
        resourceController.SpendResource(itemData.resourceCostToBuild);
    }
}
