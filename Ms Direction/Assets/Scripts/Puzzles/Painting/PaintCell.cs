using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCell : MonoBehaviour
{
    private PaintColliderGrid pg;
    private bool touched = false;
    private void Start()
    {
        pg = this.gameObject.transform.parent.GetComponent<PaintColliderGrid>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!touched && pg != null)
        {
            Debug.Log("Cell Collided!");
            pg.CellCollided();
        }
        touched = true;
    }
}
