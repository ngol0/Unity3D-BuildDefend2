using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayController : MonoBehaviour
{
    [Header("Layer Mask for Raycast")]
    [SerializeField] LayerMask interactableMask;
    [SerializeField] LayerMask gridMask;

    [Header("Ref")]
    [SerializeField] ItemPlacement itemPlacement;

    [Header("Event to raise")]
    [SerializeField] InteractableEvent OnItemSelected;

    InteractableItem selectedItem; //selected existing item

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (InteractWithUI()) return;
            if (TryPlaceItemAtGrid()) return;

            TrySelectItem();
        }
    }

    private void TrySelectItem()
    {
        //select item
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, float.MaxValue, interactableMask))
        {
            if (hitData.transform.TryGetComponent<InteractableItem>(out InteractableItem item))
            {
                selectedItem = item;
                itemPlacement.CancelPlaceableItem(); //cancel chosen placeable item when select item
            }
        }
        else
        {
            selectedItem = null;
        }

        if (OnItemSelected) OnItemSelected.Raise(selectedItem);

    }

    //called when item is selected but then want to place item instead
    public void CancelItemSelection()
    {
        selectedItem = null;
        if (OnItemSelected) OnItemSelected.Raise(selectedItem);
    }

    public bool TryPlaceItemAtGrid()
    {
        //ray cast to get a position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, float.MaxValue, gridMask))
        {
            if (itemPlacement.CanPlaceItem(hitData.point)) return true;
        }
        return false;
    }

    private bool InteractWithUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
}
