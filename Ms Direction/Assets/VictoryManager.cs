using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public GameObject VictoryScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        VictoryScreen.SetActive(false);
    }

    public void Victory()
    {
        Time.timeScale = 0;
        VictoryScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
