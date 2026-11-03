using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class BrushCameraScaler : MonoBehaviour
{
    private RectTransform canvas;
    private Camera brushCamera;
    private void Start()
    {
        brushCamera = this.GetComponent<Camera>();
        // I dont erven care anymore man whatever sure SURE WHATEVER FINE
        canvas = this.transform.parent.parent.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        brushCamera.orthographicSize = canvas.rect.height / 2;
    }
}
