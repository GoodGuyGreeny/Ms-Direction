using UnityEngine;
using UnityEngine.UI;

public class ReadingUIManager : MonoBehaviour
{
    public static ReadingUIManager Instance { get; private set; }

    [Header("Reading UI")]
    [SerializeField] private GameObject readingPanel;
    [SerializeField] private Image documentImage;

    [Header("Player Controls")]
    [SerializeField] private PlayerDisabler playerDisabler;

    private bool isReading;

    public bool IsReading => isReading;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (readingPanel != null)
            readingPanel.SetActive(false);
    }

    public void OpenDocument(Sprite document)
    {
        if (isReading)
            return;

        if (document == null || documentImage == null ||
            readingPanel == null)
        {
            Debug.LogWarning("Reading UI is not configured.");
            return;
        }

        documentImage.sprite = document;
        documentImage.preserveAspect = true;

        readingPanel.SetActive(true);
        isReading = true;

        // Disable player movement and camera controls.
        if (playerDisabler != null)
            playerDisabler.DisableForReading();

        Debug.Log("Document opened.");
    }

    public void CloseDocument()
    {
        if (!isReading)
            return;

        readingPanel.SetActive(false);
        isReading = false;

        // Restore player movement and camera controls.
        if (playerDisabler != null)
            playerDisabler.EnableAfterReading();

        Debug.Log("Document closed.");
    }
}