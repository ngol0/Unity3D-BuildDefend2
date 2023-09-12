using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionPanelUI : MonoBehaviour
{
    [Header("UI Ref")]
    [SerializeField] GameObject guiMain;
    [SerializeField] InventoryUI playPanel;

    [Header("Logic")]
    [SerializeField] UnitActionController logicController;
    [SerializeField] List<UnitActionButtonUI> buttonLists = new();
    private void OnEnable() 
    {
        logicController.OnSelectedUnit += SetUnitControllerActive;
    }

    public void SetUnitControllerActive(Unit unit)
    {
        playPanel.gameObject.SetActive(unit == null);
        guiMain.SetActive(unit != null);

        UpdateUI(unit);
    }

    private void UpdateUI(Unit unit)
    {
        if (unit == null) return;

        var actionScheduler = unit.actionScheduler;
        var actionType = actionScheduler.GetActionType();

        UpdateButtonUI(actionType);
        actionScheduler.OnActionChange += UpdateButtonUI;
    }

    private void UpdateButtonUI(ControlledActionType actionType)
    {
        foreach (var button in buttonLists)
        {
            button.UpdateUI(actionType);
        }
    }
}
