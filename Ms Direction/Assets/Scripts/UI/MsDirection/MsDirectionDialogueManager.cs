using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MsDirectionPopup
{
public string id;
[TextArea(3, 7)] public string dialogueText;
[Min(0.1f)] public float duration = 5f;
public bool triggerOnlyOnce = true;

[Header("Conditions")]
public bool onTaskActivation;
public string activationTaskId;
public bool requireActiveTask;
public string activeTaskId;
public bool onTriggerHit;
public bool onTaskCompletion;
public string completionTaskId;
public bool timedFollowUp;
public string afterPopupId;
[Min(0f)] public float followUpDelay = 3f;
public bool onVendorEvent;

}

public class MsDirectionDialogueManager : MonoBehaviour
{
public static MsDirectionDialogueManager Instance { get; private set; }

[Header("UI References")]
[SerializeField] private GameObject dialogueRoot;
[SerializeField] private TMP_Text dialogueLabel;
[SerializeField] private Image portraitImage;
[SerializeField] private Sprite msDirectionPortrait;

[Header("Designer Popups")]
[SerializeField] private List<MsDirectionPopup> popups = new List<MsDirectionPopup>();

private readonly Queue<MsDirectionPopup> pending = new Queue<MsDirectionPopup>();
private readonly HashSet<string> alreadyTriggered = new HashSet<string>();
private readonly HashSet<string> queuedIds = new HashSet<string>();
private Coroutine displayRoutine;

private void Awake()
{
    if (Instance != null && Instance != this)
    {
        Debug.LogError("Only one MsDirectionDialogueManager is allowed per scene.", this);
        enabled = false;
        return;
    }

    Instance = this;
    if (dialogueRoot != null) dialogueRoot.SetActive(false);
}

private void OnDestroy()
{
    if (Instance == this) Instance = null;
}

private void Start()
{
    HashSet<string> ids = new HashSet<string>();
    foreach (MsDirectionPopup popup in popups)
    {
        if (string.IsNullOrWhiteSpace(popup.id) || !ids.Add(popup.id))
            Debug.LogWarning("Ms Direction popup IDs must be nonempty and unique: " + popup.id, this);
    }
}

// This is callable from UnityEvents, including InteractableVendor.onSuccess.
public void TriggerPopupById(string popupId)
{
    foreach (MsDirectionPopup popup in popups)
    {
        if (popup.id == popupId)
        {
            QueuePopup(popup);
            return;
        }
    }
    Debug.LogWarning("Ms Direction popup ID not found: " + popupId, this);
}

// Trigger-volume and vendor events use the same ID lookup, but must be enabled explicitly.
public bool TriggerHit(string popupId)
{
    bool accepted = false;
    foreach (MsDirectionPopup popup in popups)
        if (popup.onTriggerHit && popup.id == popupId) accepted |= QueuePopup(popup);
    return accepted;
}
public void VendorEvent(string popupId) => TriggerMatching(p => p.onVendorEvent && p.id == popupId);

public void TaskActivated(string taskId) => TriggerMatching(p => p.onTaskActivation && p.activationTaskId == taskId && !string.IsNullOrEmpty(taskId));
public void TaskCompleted(string taskId) => TriggerMatching(p => p.onTaskCompletion && p.completionTaskId == taskId && !string.IsNullOrEmpty(taskId));

private void TriggerMatching(Predicate<MsDirectionPopup> condition)
{
    foreach (MsDirectionPopup popup in popups)
        if (condition(popup)) QueuePopup(popup);
}

private bool QueuePopup(MsDirectionPopup popup)
{
    if (string.IsNullOrWhiteSpace(popup.id) || dialogueRoot == null || dialogueLabel == null) return false;
    if (popup.requireActiveTask && (QuestManager.instance == null || QuestManager.instance.currentQuest == null ||
        QuestManager.instance.currentQuest.GetCurrentTaskID() != popup.activeTaskId)) return false;
    if ((popup.triggerOnlyOnce && alreadyTriggered.Contains(popup.id)) || queuedIds.Contains(popup.id)) return false;

    pending.Enqueue(popup);
    queuedIds.Add(popup.id);
    if (displayRoutine == null) displayRoutine = StartCoroutine(DisplayQueue());
    return true;
}

private IEnumerator DisplayQueue()
{
    while (pending.Count > 0)
    {
        MsDirectionPopup popup = pending.Dequeue();
        if (popup.triggerOnlyOnce) alreadyTriggered.Add(popup.id);
        dialogueLabel.text = popup.dialogueText;
        if (portraitImage != null && msDirectionPortrait != null) portraitImage.sprite = msDirectionPortrait;
        dialogueRoot.SetActive(true);

        yield return new WaitForSecondsRealtime(Mathf.Max(0.1f, popup.duration));
        dialogueRoot.SetActive(false);
        queuedIds.Remove(popup.id);

        foreach (MsDirectionPopup followUp in popups)
            if (followUp.timedFollowUp && followUp.id != popup.id && followUp.afterPopupId == popup.id)
                StartCoroutine(DelayedPopup(followUp));
    }
    displayRoutine = null;
}

private IEnumerator DelayedPopup(MsDirectionPopup popup)
{
    yield return new WaitForSecondsRealtime(Mathf.Max(0f, popup.followUpDelay));
    QueuePopup(popup);
}

}