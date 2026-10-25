using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// PARTIALLY DERIVED FROM YOUTUBE USER Game Dev Guide AND zawo FROM THE VIDEO:
// https://www.youtube.com/watch?v=--LB7URk60A
public class UILineRenderer : MaskableGraphic
{
    public float thickness;

    public List<Vector2> points = new List<Vector2>();
    public int positionCount
    {
        get { return points.Count; }
        set
        {
            while (positionCount < value)
                points.Add(Vector2.zero);
            SetVerticesDirty();
        }
    }

    public float width;
    public float height;

    public void SetPosition(int index, Vector2 position)
    {
        if (index >= 0 && index < points.Count)
        {
            points[index] = position;
            SetVerticesDirty();
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        if (points.Count < 2) return;


        float angle = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {

            Vector2 point = points[i];
            Vector2 point2 = points[i + 1];

            if (i < points.Count - 1)
            {
                angle = GetAngle(points[i], points[i + 1]) + 90f;
            }

            DrawVerticesForPoint(point, point2, angle, vh);
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            int index = i * 4;
            vh.AddTriangle(index + 0, index + 1, index + 2);
            vh.AddTriangle(index + 1, index + 2, index + 3);
        }
    }
    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(9f * (target.y - me.y), 16f * (target.x - me.x)) * (180 / Mathf.PI));
    }
    void DrawVerticesForPoint(Vector2 point, Vector2 point2, float angle, VertexHelper vh)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(rectTransform.rect.xMin + width * point.x,rectTransform.rect.yMin + height * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(rectTransform.rect.xMin + width * point.x, rectTransform.rect.yMin + height * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(rectTransform.rect.xMin + width * point2.x, rectTransform.rect.yMin + height * point2.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(rectTransform.rect.xMin + width * point2.x, rectTransform.rect.yMin + height * point2.y);
        vh.AddVert(vertex);
    }

}
