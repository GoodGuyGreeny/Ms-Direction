using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompleteQuestCompleter : MonoBehaviour
{
    public string QuestID;
    public GameObject buttonPuzzle;
    public ButtonPuzzle Bpuz;
    public GameObject Servguy;
    public GameObject Repguy;
    private bool solved = false;
    public void Start()
    {
        Bpuz = buttonPuzzle.GetComponent<ButtonPuzzle>();
    }

    public void Update()
    {
        if (solved == false)
        {
            if (Bpuz.winbutton == true)
            {
                ButtonTaskComplete();
            }
        }
        
    }

    public void CompleteQuest()
    {
        QuestManager.instance.currentQuest.CompleteTask(
                    QuestID);
    }

    public void ButtonTaskComplete()
    {
        if (Bpuz.winbutton == true)
        {
            CompleteQuest();
            Servguy.gameObject.SetActive(true);
            Repguy.gameObject.SetActive(false);
            solved = true;
        }
    }

}
