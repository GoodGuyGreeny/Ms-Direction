using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePaintHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject brushRendererPrefab;
    [SerializeField] 
    private GameObject brushColliderPrefab;
    [SerializeField]
    private RectTransform lineContainer;
    private RectTransform canvas;

    private GameObject brushColliderInstance;
    private UILineRenderer brushRendererInstance;
    [SerializeField]
    private Color selectedRenderColor;

    private Vector2 mouseUIPos;
    private Vector3 mousePos3D;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        // I dont erven care anymore man whatever sure SURE WHATEVER FINE
        canvas = this.transform.parent.parent.GetComponent<RectTransform>();

        brushColliderInstance = Instantiate(brushColliderPrefab);
        brushColliderInstance.transform.SetParent(this.transform);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            GetMousePosition();

            if (brushRendererInstance == null)
                CreateNewLine();
            brushColliderInstance.SetActive(true);
            brushColliderInstance.transform.position = mousePos3D;

            AddLinePosition();
        }
        else
        {
            brushColliderInstance.SetActive(false);
            brushRendererInstance = null;
        }
    }

    private void GetMousePosition()
    {
        // The next two lines i sourced from: https://discussions.unity.com/t/4-6-ui-image-follow-mouse-position/124459
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, null, out mouseUIPos);
        mousePos3D = canvas.transform.TransformPoint(mouseUIPos);

        Rect rect = canvas.rect;

        mouseUIPos.x = (mouseUIPos.x - rect.xMin) / rect.width;
        mouseUIPos.y = (mouseUIPos.y - rect.yMin) / rect.height;
    }

    private void CreateNewLine()
    {
        GameObject lineInstance = Instantiate(brushRendererPrefab);
        lineInstance.transform.SetParent(lineContainer, false);

        brushRendererInstance = lineInstance.GetComponent<UILineRenderer>();

        brushRendererInstance.color = selectedRenderColor;
        brushRendererInstance.positionCount = 2;
        brushRendererInstance.SetPosition(0, mouseUIPos);
        brushRendererInstance.SetPosition(1, mouseUIPos);
    }

    private void AddLinePosition()
    {
        brushRendererInstance.positionCount++;
        brushRendererInstance.SetPosition(brushRendererInstance.positionCount - 1, mouseUIPos);
    }

    public void ChangeColor(Color color)
    {
        selectedRenderColor = color;
    }
}
