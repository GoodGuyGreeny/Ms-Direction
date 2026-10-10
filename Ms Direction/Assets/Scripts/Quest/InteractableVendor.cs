using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class InteractableVendor : MonoBehaviour, I_Interactable
{
    public enum DeliveryResponseType
    {
        Item,
        Wrong
    }
    
    public enum DeliveryAction
    {
        Destroy,
        Replace
    }
    

    [System.Serializable]
    public class DeliveryResponse
    {
        public DeliveryAction action;

        [Tooltip("Only used when Action is Replace.")]
        public GameObject replacementItem;
        
        public DeliveryResponseType responseType;

        [Tooltip("Only used when Response Type is Item.")]
        public InventoryItem requiredItem;

        [TextArea(2, 5)]
        public string responseText;
    }

    [Header("Interaction")]
    [SerializeField] private string localDescriptionText = "Deliver Item";
    public string DescriptionText => localDescriptionText;
    
    [Header("Response UI")]
    [SerializeField] private TMP_Text responseText;

    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private float fadeDuration = 1f;

    private Coroutine responseCoroutine;

    [Header("Delivery Responses")]
    [SerializeField] private List<DeliveryResponse> responses =
        new List<DeliveryResponse>();

    public void Interacted(GameObject caller = null)
    {
        if (caller == null)
            return;

        Inventory inventory = caller.GetComponent<Inventory>();

        if (inventory == null)
        {
            Debug.LogWarning("InteractableVendor: Caller does not have an Inventory component.");
            return;
        }

        InventoryItem equippedItem = inventory.GetActiveItem();

        CheckDelivery(equippedItem, inventory);
    }

    private void CheckDelivery(InventoryItem equippedItem, Inventory inventory)
    {
        DeliveryResponse wrongResponse = null;

        foreach (DeliveryResponse response in responses)
        {
            if (response.responseType == DeliveryResponseType.Wrong)
            {
                wrongResponse = response;
                continue;
            }

            if (equippedItem == null || response.requiredItem == null)
                continue;

            if (equippedItem.itemName == response.requiredItem.itemName)
            {
                ShowResponse(response.responseText);

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
                        Debug.LogWarning("InteractableVendor: Replace action selected but no replacement item assigned.");
                    }
                }

                return;
            }
        }

        if (wrongResponse != null)
        {
            ShowResponse(wrongResponse.responseText);
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

        // Stay fully visible
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