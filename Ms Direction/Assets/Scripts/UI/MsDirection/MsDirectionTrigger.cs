using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MsDirectionTrigger : MonoBehaviour
{
    [SerializeField] private string popupId;
    [SerializeField] private bool triggerOnce = true;
    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (activated || !other.CompareTag("Player")) return;
        if (MsDirectionDialogueManager.Instance == null) return;

        bool accepted = MsDirectionDialogueManager.Instance.TriggerHit(popupId);
        if (triggerOnce && accepted) activated = true;
    }
}