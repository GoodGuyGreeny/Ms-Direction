using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingPopup : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Stop Reading"))
            Close();
    }

    void Close()
    {
        Cursor.visible = false;
        EventManager.instance.Invoke2DEventClosure();
    }
}
