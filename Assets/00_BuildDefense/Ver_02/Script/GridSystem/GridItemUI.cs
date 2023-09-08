using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItemUI : MonoBehaviour
{
    object gridItem;
    [SerializeField] TextMesh coordinateText;
    [SerializeField] MeshRenderer visualMesh;
    
    public virtual void SetGridItem(object gridItem)
    {
        this.gridItem = gridItem;
        //Debug.Log(gridItem.GetType());
    }

    protected virtual void Update() 
    {
        coordinateText.text = gridItem.ToString();
    }

    public void ShowValidMesh()
    {
        visualMesh.enabled = true;
    }

    public void HideValidMesh()
    {
        visualMesh.enabled = false;
    }
}
