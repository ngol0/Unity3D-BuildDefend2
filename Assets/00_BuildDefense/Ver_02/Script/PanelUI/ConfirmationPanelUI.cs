using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationPanelUI : MonoBehaviour
{
    [SerializeField] ItemPlacement itemPlacement;
    [SerializeField] GameObject canvas;

    private void OnEnable() 
    {
        itemPlacement.OnTryPlacingResourceItem += SetPanelActive;
        itemPlacement.OnDoneDeciding += DeactivateUI;
    }

    public void SetPanelActive()
    {
        canvas.SetActive(true);
    }

    public void DeactivateUI()
    {
        canvas.SetActive(false);
    }
}
