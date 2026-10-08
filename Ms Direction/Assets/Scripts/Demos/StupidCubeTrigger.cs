using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidCubeTrigger : MonoBehaviour
{
    public Demo1Manager demoManage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            demoManage.ChangeUITextUponTrigger();
        }
    }
}
