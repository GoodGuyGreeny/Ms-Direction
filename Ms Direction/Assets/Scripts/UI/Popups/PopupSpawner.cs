using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    public GameObject popupToSpawn;
    public void SpawnPopup()
    {
        EventManager.instance.Invoke2DEventPopup(popupToSpawn);
    }
}
