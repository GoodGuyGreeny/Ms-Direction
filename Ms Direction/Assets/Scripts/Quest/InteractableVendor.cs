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
    

    [System.Serializable]
    public class DeliveryResponse
    {
        public DeliveryResponseType responseType;

        [Tooltip("Only used when Response Type is Item.")]
        public InventoryItem requiredItem;

        [TextArea(2, 5)]
        public string responseText;
    }

    [Header("Interaction")]
    [SerializeField] private string localDescriptionText = "Deliver Item";
    public string DescriptionText => localDescriptionText;

    [Header("Testing")]
    [Tooltip("Temporary fake equipped item until the inventory system is finished.")]
    [SerializeField] private InventoryItem testEquippedItem;
    
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
        CheckDelivery(testEquippedItem);
    }

    private void CheckDelivery(InventoryItem equippedItem)
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