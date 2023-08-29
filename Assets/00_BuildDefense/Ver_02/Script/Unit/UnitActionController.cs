using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionController : MonoBehaviour
{
    [SerializeField] Unit selectedUnit;
    BaseAction currentAction;

    public System.Action<bool> OnSelectedUnit;

    public void SetSelectedUnit(InteractableItem item)
    {
        if (selectedUnit == item) return;
        selectedUnit = (item is Unit) ? item as Unit : null;

        OnSelectedUnit?.Invoke(selectedUnit!=null); //set unit action panel
    }

    public void MoveAhead()
    {
        if (selectedUnit==null) return;
        selectedUnit.GetAction<MoveAction>().StartAction();
    }

    public void StopMoving()
    {
        if (selectedUnit==null) return; 
        selectedUnit.GetAction<IdleAction>().StartAction();
    }

    public void MoveUp()
    {
        if (selectedUnit==null) return;
        //todo: move up?
    }
}
