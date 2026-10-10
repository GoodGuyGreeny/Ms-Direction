using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintColliderGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject cell;

    [SerializeField]
    private PolygonCollider2D shapeCollider;
    [SerializeField]
    [Tooltip("How many collumns you want our grid to have. May have less if it generates outside of the polygon collider.")]
    private int baseNumberOfCols;
    [SerializeField]
    [Tooltip("How many rows you want our grid to have. May have less if it generates outside of the polygon collider.")]
    private int baseNumberOfRows;
    private float gridCellWidth;
    private float gridCellHeight;

    private int cellCount;
    private int collideCount;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.Invoke2DEventPopup();
        if(shapeCollider == null)
        {
            if(GetComponent<PolygonCollider2D>() != null)
            {
                shapeCollider = GetComponent<PolygonCollider2D>();
            }
            else
            {
                Debug.LogError("ERROR in PaintColliderGrid.cs: No shape collider provided! What do you want me to do? Make a grid for NO shape? Not happening!");
                return;
            }
        }

        GenerateGrid();
    }

    void GenerateGrid()
    {
        gridCellWidth = shapeCollider.bounds.size.x / baseNumberOfCols;
        gridCellHeight = shapeCollider.bounds.size.y / baseNumberOfRows;

        for (int i = 0; i < baseNumberOfCols; i++)
        {
            for (int j = 0; j < baseNumberOfRows; j++)
            {
                Vector2 pos = new Vector2(shapeCollider.bounds.min.x + gridCellWidth * (i+0.5f), shapeCollider.bounds.min.y + gridCellHeight * (j+0.5f));
                
                // If our position isnt in the shape, why bother making it?
                if(!shapeCollider.OverlapPoint(pos))
                {
                    continue;
                }

                GameObject cellInstance = Instantiate(cell);
                cellInstance.transform.parent = this.gameObject.transform;
                cellInstance.transform.position = pos;
                cellInstance.GetComponent<BoxCollider2D>().size = new Vector2(gridCellWidth, gridCellHeight);

                cellCount++;
            }
        }
    }

    public void CellCollided()
    {
        collideCount++;
        if (collideCount == cellCount)
            Debug.Log("Drawing was covered!");
    }
}
