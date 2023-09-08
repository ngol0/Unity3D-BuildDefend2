using UnityEngine.UI;
using UnityEngine;

public class UnitActionButtonUI : MonoBehaviour
{
    public ControlledActionType actionType;
    [SerializeField] Image selectedBG;

    public void UpdateUI(ControlledActionType actionType)
    {
        selectedBG.enabled = actionType == this.actionType;
    }
}
