using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempLoader : MonoBehaviour
{
    // temp to establish victory
    public string Scene;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(Scene);
    }
}
