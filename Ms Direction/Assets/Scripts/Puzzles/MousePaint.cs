using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePaint : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    [SerializeField]
    private GameObject mouseColliderPrefab;
    private GameObject mouseColInstance;

    private Vector2 mouseUIPos;
    private Vector3 pos3D;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        mouseColInstance = Instantiate(mouseColliderPrefab);
    }

    // Update is called once per frame
    void Update()
    {

        // The next three lines i sourced from: https://discussions.unity.com/t/4-6-ui-image-follow-mouse-position/124459
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, null, out mouseUIPos);
        pos3D =  canvas.transform.TransformPoint(mouseUIPos);
        mouseColInstance.transform.position = pos3D;
        if (Input.GetButton("Fire1"))
        {
            mouseColInstance.SetActive(true);
        }
        else
        {
            mouseColInstance.SetActive(false);
        }
    }
}
