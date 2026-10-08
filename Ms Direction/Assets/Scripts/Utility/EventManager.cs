using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is this best practice???
// I have no idea! This is how I always do it!
// I think this is probably a fine use for a singleton.
public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public event System.Action on2DEventPopup;
    public event System.Action on2DEventClosure;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Invoke2DEventPopup()
    {
        on2DEventPopup?.Invoke();
    }

    public void Invoke2DEventClosure()
    {
        on2DEventClosure?.Invoke();
    }
}
