using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is this best practice???
// I have no idea! This is how I always do it!
// I think this is probably a fine use for a singleton.
public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public event System.Action onEventPopup;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this);
        }
    }

    public void InvokeEventPopup()
    {
        onEventPopup?.Invoke();
    }
}
