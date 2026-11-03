using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{

    public bool winbutton = false;


    public GameObject button1On;
    public GameObject button1Off;
    public GameObject button2On;
    public GameObject button2Off;
    public GameObject button3On;
    public GameObject button3Off;
    public GameObject button4On;
    public GameObject button4Off;
    public GameObject button1Paint;
    public GameObject button2Paint;
    public GameObject button3Paint;
    public GameObject button4Paint;
    bool alternate = false;




    public void TriggerButton1()
    {
        button1On.SetActive(!button1On.activeSelf);
        button1Off.SetActive(!button1Off.activeSelf);
        button2On.SetActive(!button2On.activeSelf);
        button2Off.SetActive(!button2Off.activeSelf);
    }

    public void TriggerButton2()
    {
        button3On.SetActive(!button3On.activeSelf);
        button3Off.SetActive(!button3Off.activeSelf);
        button2On.SetActive(!button2On.activeSelf);
        button2Off.SetActive(!button2Off.activeSelf);
    }

    public void TriggerButton3()
    {
        button1On.SetActive(!button1On.activeSelf);
        button1Off.SetActive(!button1Off.activeSelf);
        button3On.SetActive(!button3On.activeSelf);
        button3Off.SetActive(!button3Off.activeSelf);
    }

    public void TriggerButton4()
    {
        button4On.SetActive(!button4On.activeSelf);
        button4Off.SetActive(!button4Off.activeSelf);
        button2On.SetActive(!button2On.activeSelf);
        button2Off.SetActive(!button2Off.activeSelf);
    }

    public void PaintedButtons()
    {
        button4On.SetActive(false);
        button4Off.SetActive(false);
        button2On.SetActive(false);
        button2Off.SetActive(false);
        button1On.SetActive(false);
        button1Off.SetActive(false);
        button3On.SetActive(false);
        button3Off.SetActive(false);
        button1Paint.SetActive(true);
        button2Paint.SetActive(true);
        button3Paint.SetActive(true);
        button4Paint.SetActive(true);
        alternate = true;
    }
    
    public void Hinge()
    {
        alternate = true;
    }

    public void HingeOff()
    {
        alternate = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (button1On.activeSelf && button2On.activeSelf && button3On.activeSelf && button4On.activeSelf)
        {
            winbutton = true;
        }
        else if (alternate)
        {
            winbutton = true;
        }
        else
        {
            winbutton = false;
        }
    }


}
