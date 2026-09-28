using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/QuestData", order = 0)]
public class QuestData : ScriptableObject
{
    // Array of QuestTasks that outline the steps of the quest
    [SerializeField]
    private QuestTask[] tasks;

    // Checks whether the passed task is able to be completed.
    // Tasks are unable to be completed when they require all previous tasks
    // to be completed, and a prerequisite has not been met.
    public bool CanTaskBeCompleted(string id)
    {
        // DO NOT REFACTOR! I call GetTaskByID because it provides valuable error checking in
        // the debug log.
        QuestTask task = GetTaskByID(id);
        if (task == null)
            return false;

        bool thereBeenAnIncompleteTask = false;

        foreach (QuestTask t in tasks)
        {
            if (task.GetID() == t.GetID())
                break;

            if (t.GetCompletionStatus() == false)
                thereBeenAnIncompleteTask = true;
        }

        if (thereBeenAnIncompleteTask && task.RequiresPrerequisiteTasks())
            return false;

        return true;
    }

    // Sets our tasks status to be complete.
    // Note that this won't save persistently.
    public void SetTaskToComplete(string id)
    {
        QuestTask task = GetTaskByID(id);

        if(task != null)
            task.SetCompletionStatus(true);
    }

    // Returns true if all tasks in tasks are marked as completed
    // Returns false otherwise
    public bool AllTasksCompleted()
    {
        foreach( QuestTask t in tasks)
        {
            if (!t.GetCompletionStatus())
                return false;
        }

        return true;
    }

    // Returns a string[] of all the task descriptions, in order
    public string[] GetTaskTextData()
    {
        string[] text = new string[tasks.Length];
        for(int i = 0; i < text.Length; i++)
        {
            text[i] = tasks[i].descriptionText;
        }
        return text;
    }

    // Returns a bool[] of all the task completion statuses, in order
    public bool[] GetTaskCompletionData()
    {
        bool[] completedTasks = new bool[tasks.Length];
        for (int i = 0; i < completedTasks.Length; i++)
        {
            completedTasks[i] = tasks[i].GetCompletionStatus();
        }
        return completedTasks;
    }

    // Returns the QuestTask associated with the id argument.
    // Logs an error and returns null if none can be found.
    private QuestTask GetTaskByID(string id)
    {
        foreach(QuestTask t in tasks)
        {
            if (id == t.GetID())
                return t;
        }

        string debugString = "ERROR! FindTaskById in QuestData.cs just attempted to find task with ID of: " + id + " and it was not found!" +
            "\nCurrent list of IDs:\n";
        foreach(QuestTask t in tasks)
        {
            debugString += t.GetID() + "\n";
        }

        Debug.LogError(debugString);
        
        return null;
    }
}

[System.Serializable]
public class QuestTask
{
    // Though unconventional and maybe a bit weird looking in the inspector, by having descriptionText
    // above ID, it's far more readable for our designers seeing the descriptions over the id in the array
    // element naming. At least I'd assume.
    [Tooltip("The actual text UI will show for this specific task.")]
    public string descriptionText;

    [SerializeField]
    [Tooltip("The ID one should use when one wants to reference this specific task")]
    private string id;

    [SerializeField]
    [Tooltip("When set to true, tasks that appear before this in the array must be completed before this task can be completed.")]
    private bool completePrevious;
    private bool completed;

    // Possibly temporary functions.
    public string GetID() { return id; }

    public bool RequiresPrerequisiteTasks() { return completePrevious; }

    public bool GetCompletionStatus() { return completed; }
    public void SetCompletionStatus(bool b) { completed = b; }
}
