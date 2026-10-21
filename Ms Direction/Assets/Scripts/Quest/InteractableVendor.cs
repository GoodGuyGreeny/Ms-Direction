
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using TMPro;

public class InteractableVendor : MonoBehaviour, I_Interactable
{
    public enum DeliveryResponseType
    {
        Item,
        Wrong,
        AlternateWrong
    }

    public enum DeliveryAction
    {
        Destroy,
        Replace,
        Keep
    }

    [System.Serializable]
    public class DeliveryResponse
    {
        [Header("Response Settings")]
        public DeliveryResponseType responseType;

        [Tooltip("Only used when Response Type is Item.")]
        public GameObject requiredItemObject;

        // This will not appear in the Inspector.
        public I_InventoryItem requiredItem;

        [TextArea(2, 5)]
        public string responseText;

        [Header("Inventory Action")]
        public DeliveryAction action;

        [Tooltip("Only used when Action is Replace.")]
        public GameObject replacementItem;

        [Header("Quest")]
        public bool completeQuestTask;

        [Tooltip("Quest task ID to complete when this delivery succeeds.")]
        public string questTaskID;

        [Header("Custom Events")]
        [Tooltip("Called when the player uses the correct item.")]
        public UnityEvent onSuccess;

        [Header("Interaction Rules")]
        [Tooltip("If enabled, this response can only succeed once.")]
        public bool onlyOnce = false;

        // Tracks whether this response has already succeeded.
        // Not visible in the Inspector or saved between sessions.
        [System.NonSerialized]
        public bool hasBeenUsed = false;
    }

    [Header("Interaction")]
    [SerializeField]
    private string localDescriptionText = "Deliver Item";

    public string DescriptionText => localDescriptionText;

    [Header("Response UI")]
    [SerializeField] private TMP_Text responseText;
    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private float fadeDuration = 1f;

    private Coroutine responseCoroutine;
    
    [Header("Vendor State")]
    [SerializeField] private bool alternateWrongActive = false;

    public void SetAlternateWrongActive(bool value)
    {
        alternateWrongActive = value;
    }

    [Header("Delivery Responses")]
    [SerializeField]
    private List<DeliveryResponse> responses =
        new List<DeliveryResponse>();

    public void Interacted(GameObject caller = null)
    {
        if (caller == null)
            return;

        Inventory inventory = caller.GetComponent<Inventory>();

        if (inventory == null)
        {
            Debug.LogWarning(
                "InteractableVendor: Caller does not have an Inventory component."
            );
            return;
        }

        I_InventoryItem equippedItem = inventory.GetActiveItem();

        CheckDelivery(equippedItem, inventory);
    }

   
private void CheckDelivery(
    I_InventoryItem equippedItem,
    Inventory inventory)
{
    DeliveryResponse wrongResponse = null;
    DeliveryResponse alternateWrongResponse = null;

    foreach (DeliveryResponse response in responses)
    {
        // Save both fallback responses.
        if (response.responseType == DeliveryResponseType.Wrong)
        {
            wrongResponse = response;
            continue;
        }

        if (response.responseType == DeliveryResponseType.AlternateWrong)
        {
            alternateWrongResponse = response;
            continue;
        }

        // Only Item responses should check inventory.
        if (equippedItem == null || response.requiredItemObject == null)
            continue;

        response.requiredItem =
            response.requiredItemObject.GetComponent<I_InventoryItem>();

        if (response.requiredItem == null)
        {
            Debug.LogError(
                "InteractableVendor: Required item does not implement I_InventoryItem."
            );
            continue;
        }

        if (equippedItem.ItemName == response.requiredItem.ItemName)
        {
            // Prevent one-time interactions from repeating.
            if (response.onlyOnce && response.hasBeenUsed)
                return;

            // Record successful interaction.
            response.hasBeenUsed = true;

            ShowResponse(response.responseText);

            // Inventory handling
            if (response.action == DeliveryAction.Destroy)
            {
                inventory.Remove();
            }
            else if (response.action == DeliveryAction.Replace)
            {
                if (response.replacementItem != null)
                {
                    inventory.Replace(response.replacementItem);
                }
                else
                {
                    Debug.LogWarning(
                        "InteractableVendor: Replace selected but no replacement assigned."
                    );
                }
            }
            // Keep: The equipped item remains unchanged.

            // Quest completion
            if (response.completeQuestTask &&
                !string.IsNullOrEmpty(response.questTaskID) &&
                QuestManager.instance != null &&
                QuestManager.instance.currentQuest != null)
            {
                QuestManager.instance.currentQuest.CompleteTask(
                    response.questTaskID
                );
            }

            // Existing popup functionality
            PopupSpawner popup = GetComponent<PopupSpawner>();

            if (popup != null)
            {
                popup.SpawnPopup();
            }

            // Designer-configurable actions
            response.onSuccess?.Invoke();

            return;
        }
    }

    // Select fallback based on the vendor's current state.
    DeliveryResponse selectedWrongResponse = alternateWrongActive
        ? alternateWrongResponse
        : wrongResponse;

    if (selectedWrongResponse != null)
    {
        ShowResponse(selectedWrongResponse.responseText);
    }
}


    private void ShowResponse(string text)
    {
        Debug.Log(text);

        if (responseText == null)
            return;

        if (responseCoroutine != null)
        {
            StopCoroutine(responseCoroutine);
        }

        responseCoroutine = StartCoroutine(DisplayResponse(text));
    }

    private IEnumerator DisplayResponse(string text)
    {
        responseText.text = text;

        Color color = responseText.color;
        color.a = 1f;
        responseText.color = color;

        responseText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        // Fade out
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(
                1f,
                0f,
                elapsedTime / fadeDuration
            );

            color.a = alpha;
            responseText.color = color;

            yield return null;
        }

        color.a = 0f;
        responseText.color = color;

        responseText.gameObject.SetActive(false);

        responseCoroutine = null;
    }

    public void BroadcastDescriptionText()
    {
        Debug.Log(localDescriptionText);
    }
}
