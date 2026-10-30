using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupEventTrigger : MonoBehaviour
{
    private PopupSpawner popupSpawner;
    private void Start()
    {
        popupSpawner = GetComponent<PopupSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (popupSpawner != null)
            popupSpawner.SpawnPopup();
    }
}
