using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompleteQuestCompleter : MonoBehaviour
{
    public string QuestID;
    public GameObject buttonPuzzle;
    public ButtonPuzzle Bpuz;
    public GameObject ButtonBadDoor;
    public void Start()
    {
        Bpuz = buttonPuzzle.GetComponent<ButtonPuzzle>();
    }

    public void CompleteQuest()
    {
        QuestManager.instance.currentQuest.CompleteTask(
                    QuestID);
    }

    public void ButtonTaskComplete()
    {
        if (Bpuz.winbutton == true| ButtonBadDoor.activeInHierarchy == false)
        {
            CompleteQuest();
        }
    }

}
