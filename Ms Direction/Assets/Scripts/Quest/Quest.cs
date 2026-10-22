using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    [Tooltip("A Quest Data scriptable object that outlines the information needed for this quest.")]
    private QuestData data;
    
    // UI Hookup for UI Manager
    public string GetCurrentTaskDescription()
    {
        return data.GetCurrentTaskDescription();
    }

    // Attempts to complete the task associated with the given ID.
    // Returns true if it succeeds, and false if it fails.
    public bool CompleteTask(string id)
    {
        if(data.CanTaskBeCompleted(id))
        {
            Debug.Log("Task: " + id + " has been completed!");
            data.SetTaskToComplete(id);
            QuestManager.instance.UpdateQuestDisplay();
            CheckQuestCompletion();
            return true;
        }

        Debug.Log("Task: " + id + " could not be completed.");

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

    // This function is only needed in the editor. See the inner functions comment for more details.
    public void SetAllTasksToBeIncomplete()
    {
        data.SetAllTasksToBeIncomplete();
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
