using UnityEngine;

public class SimpleDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform doorPivot;
    public float openAngle = 90f;
    public float openSpeed = 3f;
    public KeyCode interactKey = KeyCode.E;

    private bool isPlayerNearby = false;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        if (doorPivot == null) doorPivot = transform;

        closedRotation = doorPivot.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
 
        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            isOpen = !isOpen;
        }

        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, targetRotation, Time.deltaTime * openSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}