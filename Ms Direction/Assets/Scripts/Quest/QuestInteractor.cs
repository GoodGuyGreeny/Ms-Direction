using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteractor : MonoBehaviour
{
    [SerializeField]
    private string id;

    // Calls the QuestManager and attempts to complete a task with the associated ID.
    // Returns true if it completes, returns false if it does not.
    public bool AttemptToCompleteTask()
    {
        return QuestManager.instance.currentQuest.CompleteTask(id);
    }

    // Calls the QuestManager and attempts to complete a task with the associated ID.
    // Does not return anything. Only use with UI. In almost every other interaction,
    // the callee would probably like to know if their task completed or failed.
    public void AttemptToCompleteTaskUI()
    {
        QuestManager.instance.currentQuest.CompleteTask(id);
    }
}
