using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance = null;

    public Quest currentQuest = null;

    [SerializeField]
    private Quest[] quests;
    private int currentQuestIndex = 0;

    [SerializeField]
    private float timeBetweenQuestTransition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(quests.Length != 0)
            currentQuest = quests[currentQuestIndex];
    }

    // Completes the active quest, and begins quest transition
    public void CurrentQuestComplete()
    {
        // Possibly update UI?
        // Possibly play voice line?
        // All stuff I can't implement just yet!

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
        UpdateQuestDisplay();
    }

    // Calls the UI Manager to update the quest UI
    public void UpdateQuestDisplay()
    {
        string[] descriptions = currentQuest.GetTaskTextData();
        bool[] completions = currentQuest.GetTaskCompletionData();
        // UI Manager needed to finalize implementation
    }

    // Small buffer between quest completion and quest transition
    IEnumerator StartBufferBetweenQuestTransition()
    {
        yield return new WaitForSeconds(timeBetweenQuestTransition);
        QuestTransition();
    }
}
