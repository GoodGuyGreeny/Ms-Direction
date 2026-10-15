using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance = null;

    [Tooltip("Setting this does nothing. QuestManager handles all quest setting and transitions. Element 0 of quests will always be the first quest, and it will continue down the array linearly.")]
    public Quest currentQuest = null;

    [SerializeField]
    [Tooltip("The list of quests that can be found in this level, in order.")]
    private Quest[] quests;
    private int currentQuestIndex = 0;

    [SerializeField]
    [Tooltip("The number of seconds that it takes to transition from one quest to the next")]
    private float timeBetweenQuestTransition = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(quests.Length != 0)
        {
            currentQuest = quests[currentQuestIndex];
            // This line is only needed in the editor. See the inner functions comment for more details.
            currentQuest.SetAllTasksToBeIncomplete();
        }
        
        UpdateQuestDisplay();
    }

    // Completes the active quest, and begins quest transition
    public void CurrentQuestComplete()
    {
        // Possibly update UI?
        // Possibly play voice line?
        // All stuff I can't implement just yet!

        Debug.Log("The current quest has been completed!");
        StartCoroutine("StartBufferBetweenQuestTransition");
    }

    // Transitions from one quest to the next
    // Currently does nothing if there are no more quests (will this ever happen?)
    void QuestTransition()
    {
        currentQuestIndex++;

        // Pending change upon talking with designers:
        // Each level may end with a quest that never changes, and "can never be completed"
        // where it simply says GO TO ELEVATOR.
        if (currentQuestIndex >= quests.Length)
            return;

        currentQuest = quests[currentQuestIndex];
        // This line is only needed in the editor. See the inner functions comment for more details.
        currentQuest.SetAllTasksToBeIncomplete();
        UpdateQuestDisplay();
    }

    // Calls the UI Manager to update the quest UI
    public void UpdateQuestDisplay()
    {
        string description = currentQuest.GetCurrentTaskDescription();

        QuestUIManager.instance.UpdateQuestUI(description);
    }

    // Small buffer between quest completion and quest transition
    IEnumerator StartBufferBetweenQuestTransition()
    {
        yield return new WaitForSeconds(timeBetweenQuestTransition);
        QuestTransition();
    }
}
