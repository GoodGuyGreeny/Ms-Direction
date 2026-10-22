using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePaintHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject brushPrefab;
    private RectTransform canvas;

    private GameObject lineInstance;
    private LineRenderer lineInstanceRenderer;
    private Color selectedRenderColor;

    private Vector2 mouseUIPos;
    private Vector3 mousePos3D;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        // I dont erven care anymore man whatever sure SURE WHATEVER FINE
        canvas = this.transform.parent.parent.GetComponent<RectTransform>();

        selectedRenderColor = brushPrefab.GetComponent<LineRenderer>().startColor;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            GetMousePosition();

            if (lineInstance == null)
                CreateNewLine();
            lineInstance.transform.position = mousePos3D;

            AddLinePosition();
        }
        else
        {
            lineInstance = null;
            lineInstanceRenderer = null;
        }
    }

    private void GetMousePosition()
    {
        // The next two lines i sourced from: https://discussions.unity.com/t/4-6-ui-image-follow-mouse-position/124459
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, null, out mouseUIPos);
        mousePos3D = canvas.transform.TransformPoint(mouseUIPos);
    }

    private void CreateNewLine()
    {
        lineInstance = Instantiate(brushPrefab);
        lineInstance.transform.SetParent(this.transform);
        lineInstance.transform.position = mousePos3D;

        lineInstanceRenderer = lineInstance.GetComponent<LineRenderer>();

        lineInstanceRenderer.startColor = selectedRenderColor;
        lineInstanceRenderer.endColor = selectedRenderColor;
        lineInstanceRenderer.SetPosition(0, mousePos3D);
        lineInstanceRenderer.SetPosition(1, mousePos3D);
    }

    private void AddLinePosition()
    {
        lineInstanceRenderer.positionCount++;
        lineInstanceRenderer.SetPosition(lineInstanceRenderer.positionCount - 1, mousePos3D);
    }

    public void ChangeColor(Color color)
    {
        selectedRenderColor = color;
    }
}
