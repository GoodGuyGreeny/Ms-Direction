using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Demo1Manager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    private string updatedUIText;

    // Update is called once per frame
    void Update()
    {
        ReloadSceneInputAndHandling();
    }

    // Throwaway function. Hit R at anytime to reload the demo scene. Fairly simple.
    void ReloadSceneInputAndHandling()
    {
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangeUITextUponTrigger()
    {
        text.text = updatedUIText;
    }
}
