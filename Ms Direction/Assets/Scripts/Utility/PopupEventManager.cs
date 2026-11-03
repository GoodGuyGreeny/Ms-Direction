using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupEventManager : MonoBehaviour
{
    GameObject currentPopup;
    void OnEnable()
    {
        EventManager.instance.on2DEventPopup += (popup) => SpawnPopup(popup);
        EventManager.instance.on2DEventClosure += DestroyPopup;
    }

    void OnDisable()
    {
        EventManager.instance.on2DEventPopup -= SpawnPopup;
        EventManager.instance.on2DEventClosure -= DestroyPopup;
    }

    void SpawnPopup(GameObject popupPrefab)
    {
        if(currentPopup != null)
        {
            Debug.LogWarning("Can't have more than one popup at once! Make sure one event reaches closure before instantiating another!");
            return;
        }
        currentPopup = Instantiate(popupPrefab);
        currentPopup.transform.SetParent(this.transform, false);
    }

    void DestroyPopup()
    {
        if (currentPopup != null)
            Destroy(currentPopup);
        currentPopup = null;
    }
}
