using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image slotBackground;
    [SerializeField] private Image itemIcon;

    [Header("Slot Colors")]
    [SerializeField] private Color normalColor = Color.gray;
    [SerializeField] private Color selectedColor = Color.yellow;

    public void SetItem(Sprite sprite)
    {
        if (itemIcon == null)
            return;

        if (sprite == null)
        {
            itemIcon.sprite = null;
            itemIcon.gameObject.SetActive(false);
        }
        else
        {
            itemIcon.sprite = sprite;
            itemIcon.preserveAspect = true;
            itemIcon.gameObject.SetActive(true);
        }
    }

    public void SetSelected(bool selected)
    {
        if (slotBackground == null)
            return;

        slotBackground.color = selected
            ? selectedColor
            : normalColor;
    }
}