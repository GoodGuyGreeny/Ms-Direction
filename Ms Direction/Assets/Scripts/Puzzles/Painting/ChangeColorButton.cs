using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorButton : MonoBehaviour
{
    [SerializeField]
    private MousePaintHandler mousePaintHandler;
    [SerializeField]
    private Color color;
    public void ChangeBrushColor()
    {
        mousePaintHandler.ChangeColor(color);
    }
}
