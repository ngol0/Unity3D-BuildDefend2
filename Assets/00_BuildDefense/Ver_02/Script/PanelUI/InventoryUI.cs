using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("UI ref")]
    [SerializeField] PlaceableButtonUI buttonPrefab;
    [SerializeField] Transform rootSpawn;
    [SerializeField] GameObject guiMain;

    [Header("Logic ref")]
    [SerializeField] ItemPlacement itemPlacement;

    [Header("InventorySO")]
    [SerializeField] InventorySO inventory;

    PlaceableButtonUI currentButton;
    LoadingItem loading;

    private void Awake() 
    {
        buttonPrefab.gameObject.SetActive(false);
    }

    private void OnEnable() 
    {
        itemPlacement.OnItemPlaced += OnDonePlaced;
        itemPlacement.OnCancelPlacedItem += ResetUI;
        itemPlacement.OnTryPlacingResourceItem += DeactivateUI;
        itemPlacement.OnDoneDeciding += SetUIActive;

        inventory.OnAddComplete += InitNewItem;
    }

    private void Start() 
    {
        loading = GetComponent<LoadingItem>();

        //init initial buildings
        if (inventory.interactableItemList.Count <= 0) return;
        foreach (var item in inventory.interactableItemList)
        {
            var btn = Instantiate<PlaceableButtonUI>(buttonPrefab, rootSpawn);
            btn.SetData(item, loading);
            btn.gameObject.SetActive(true);
            btn.SetOverlayBG(0f);
        }
    }

    //set current item when clicked into each item
    public void SetActiveInventoryItem(PlaceableButtonUI btn)
    {
        if (btn == null) return;
        if (loading.GetLoadingTime(btn) > 0) 
        {
            return;
        }

        if (btn == currentButton) //when clicked onto the selected button -> unselect
        {
            currentButton.SetSelectedActive(false);
            itemPlacement.SetItemToPlaceInfo(null);
            currentButton = null;
            return;
        }

        if (currentButton != null) //select different btn
        {
            currentButton.SetSelectedActive(false);
        }
        btn.SetSelectedActive(true);
        currentButton = btn;

        itemPlacement.SetItemToPlaceInfo(currentButton.ItemData); //set item to place
    }

    private void DeactivateUI()
    {
        guiMain.SetActive(false);
    }

    public void SetUIActive()
    {
        guiMain.SetActive(true);
    }

    private void ResetUI()
    {
        if (currentButton == null) return;

        currentButton.SetSelectedActive(false);
        currentButton = null;
    }

    private void OnDonePlaced()
    {
        if (currentButton == null) return;

        currentButton.gameObject.SetActive(false);
        currentButton = null;
    }

    public void InitNewItem(InteractableData data)
    {
        var btn = Instantiate<PlaceableButtonUI>(buttonPrefab, rootSpawn);
        btn.SetData(data, loading);
        btn.gameObject.SetActive(true);

        loading.StartLoading(btn, data.loadingTime);
    }
}
