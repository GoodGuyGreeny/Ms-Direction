using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LevelLoadTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManagement.instance.ProceedToNextSceneInBuild();
    }
}
