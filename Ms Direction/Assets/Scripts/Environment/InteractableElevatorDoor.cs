using UnityEngine;

public class InteractableElevatorDoor : MonoBehaviour, I_Interactable
{
    public enum DoorSide
    {
        Left,
        Right
    }

    public enum ElevatorFacing
    {
        posX,
        negX,
        posZ,
        negZ
    }

    [Header("Interaction")]
    [SerializeField] private string localDescriptionText = "Open Door";
    public string DescriptionText => localDescriptionText;

    [Header("Door Settings")]
    [SerializeField] private DoorSide doorSide = DoorSide.Left;
    [SerializeField] private ElevatorFacing elevatorFacing = ElevatorFacing.posX;

    [SerializeField] private bool openAtSpawn = false;

    [SerializeField] private float openSpeed = 3f;

    private bool isOpen;

    private Vector3 closedPosition;

    private Vector3 openPosition;

    private void Start()
    {
        closedPosition = transform.position;
        CalculateOpenTransform();

        isOpen = openAtSpawn;

        if (openAtSpawn)
        {
            transform.position = openPosition;

            localDescriptionText = "Close Door";
        }
    }

    private void Update()
    {
        Vector3 targetPosition = isOpen
            ? openPosition
            : closedPosition;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * openSpeed
        );
    }


    private void CalculateOpenTransform()
    {
        openPosition = closedPosition;
        float moveDist = 1.25f;

        if (elevatorFacing == ElevatorFacing.posX)
        {
            if (doorSide == DoorSide.Right)
            {
                openPosition.z += moveDist;
            }
            else
            {
                openPosition.z -= moveDist;
            }
        }
        else if (elevatorFacing == ElevatorFacing.negX)
        {
            if (doorSide == DoorSide.Right)
            {
                openPosition.z -= moveDist;
            }
            else
            {
                openPosition.z += moveDist;
            }
        }
        else if (elevatorFacing == ElevatorFacing.posZ)
        {
            if (doorSide == DoorSide.Right)
            {
                openPosition.x -= moveDist;
            }
            else
            {
                openPosition.x += moveDist;
            }
        }
        else if (elevatorFacing == ElevatorFacing.negZ)
        {
            if (doorSide == DoorSide.Right)
            {
                openPosition.x += moveDist;
            }
            else
            {
                openPosition.x -= moveDist;
            }
        }

    }

    public void Interacted(GameObject caller = null)
    {
        isOpen = !isOpen;

        localDescriptionText =
            isOpen ? "Close Door" : "Open Door";
    }

    public void BroadcastDescriptionText()
    {
        Debug.Log(localDescriptionText);
    }
}