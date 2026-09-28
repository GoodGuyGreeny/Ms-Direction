using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private QuestData data;

    // Attempts to complete the task associated with the given ID.
    // Returns true if it succeeds, and false if it fails.
    public bool CompleteTask(string id)
    {
        if(data.CanTaskBeCompleted(id))
        {
            data.SetTaskToComplete(id);
            QuestManager.instance.UpdateQuestDisplay();
            CheckQuestCompletion();
            return true;
        }

        return false;
    }

    // Checks whether the quest has been completed or not
    void CheckQuestCompletion()
    {
        if(data.AllTasksCompleted())
        {
            QuestManager.instance.CurrentQuestComplete();
        }
    }
    
    // Gets an array of the description text of each task
    public string[] GetTaskTextData()
    {
        return data.GetTaskTextData();
    }

    // Gets an array of the completion status of each task
    public bool[] GetTaskCompletionData()
    {
        return data.GetTaskCompletionData();
    }
}
