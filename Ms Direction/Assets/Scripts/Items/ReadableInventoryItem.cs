using UnityEngine;
using UnityEngine.UI;

public class ReadableInventoryItem : MonoBehaviour, I_InventoryItem
{
    [Header("Inventory Settings")]
    [SerializeField] private string itemName = "Document";
    [SerializeField] private Image icon;

    public string ItemName => itemName;
    public Image Icon => icon;

    [Header("Reading Settings")]
    [Tooltip("The 2D image displayed when reading this item.")]
    [SerializeField] private Sprite documentImage;

    public void Interact(Inventory inventory)
    {
        if (ReadingUIManager.Instance == null)
        {
            Debug.LogWarning("ReadingUIManager not found!");
            return;
        }

        if (documentImage == null)
        {
            Debug.LogWarning("No document image assigned!");
            return;
        }

        ReadingUIManager.Instance.OpenDocument(documentImage);
    }
}