using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnablePaintEventMinigame : MonoBehaviour
{

   public GameObject panel;
   void OnEnable()
   {
      EventManager.instance.on2DEventPopup += EnableThePanel;
   }

   void OnDisable()
   {
      EventManager.instance.on2DEventPopup -= EnableThePanel;
   }

   void EnableThePanel()
   {
      panel.SetActive(true) 
   }
}
