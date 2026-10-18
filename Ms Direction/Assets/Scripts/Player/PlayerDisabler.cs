
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Probably not the cleanest script I've written.
// Gets a bunch of player scripts and disables them.
// Why is this not handled in individual scripts?
// I think this is preferable to having "EventManager"
// as a dependency in all of those scripts.
//
// I'm under limited time! I can't ideate!
public class PlayerDisabler : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour[] scriptsToDisable;

    void OnEnable()
    {
        EventManager.instance.on2DEventPopup += Handle2DEventPopup;
        EventManager.instance.on2DEventClosure += EnableScripts;
    }

    private void OnDisable()
    {
        EventManager.instance.on2DEventPopup -= Handle2DEventPopup;
        EventManager.instance.on2DEventClosure -= EnableScripts;
    }

    // Named handler to ensure event unsubscription finds the exact method.
    private void Handle2DEventPopup(GameObject popup)
    {
        DisableScripts();
    }

    void DisableScripts()
    {
        for (int i = 0; i < scriptsToDisable.Length; i++)
        {
            scriptsToDisable[i].enabled = false;
        }
    }

    void EnableScripts()
    {
        for (int i = 0; i < scriptsToDisable.Length; i++)
        {
            scriptsToDisable[i].enabled = true;
        }
    }

    // Added for the Reading System.
    public void DisableForReading()
    {
        DisableScripts();
    }

    // Added for the Reading System.
    public void EnableAfterReading()
    {
        EnableScripts();
    }
}
