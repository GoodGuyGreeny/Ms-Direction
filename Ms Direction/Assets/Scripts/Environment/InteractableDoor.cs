using UnityEngine;

public class InteractableDoor : MonoBehaviour, I_Interactable
{
    public enum PivotSide
    {
        Left,
        Right
    }

    [Header("Interaction")]
    [SerializeField] private string localDescriptionText = "Open Door";
    public string DescriptionText => localDescriptionText;

    [Header("Door Settings")]
    [SerializeField] private PivotSide pivotSide = PivotSide.Left;

    [SerializeField] private bool openOutward = true;
    [SerializeField] private bool openAtSpawn = false;

    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 3f;

    private bool isOpen;

    private Vector3 closedPosition;
    private Quaternion closedRotation;

    private Vector3 openPosition;
    private Quaternion openRotation;

    private Vector3 pivotPoint;

    private void Start()
    {
        closedPosition = transform.position;
        closedRotation = transform.rotation;

        CalculatePivot();
        CalculateOpenTransform();

        isOpen = openAtSpawn;

        if (openAtSpawn)
        {
            transform.position = openPosition;
            transform.rotation = openRotation;

            localDescriptionText = "Close Door";
        }
    }

    private void Update()
    {
        Vector3 targetPosition = isOpen
            ? openPosition
            : closedPosition;

        Quaternion targetRotation = isOpen
            ? openRotation
            : closedRotation;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * openSpeed
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * openSpeed
        );
    }

    private void CalculatePivot()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null)
        {
            Debug.LogError("InteractableDoor needs a MeshFilter.");
            pivotPoint = transform.position;
            return;
        }

        Bounds bounds = meshFilter.sharedMesh.bounds;

        Vector3 localCenter = bounds.center;

        Vector3 localSideA = bounds.center;
        Vector3 localSideB = bounds.center;

        Vector3 widthDirection;

        // Figure out which axis is the width of the door
        if (bounds.size.x >= bounds.size.z)
        {
            localSideA.x = bounds.min.x;
            localSideB.x = bounds.max.x;

            widthDirection = transform.right;
        }
        else
        {
            localSideA.z = bounds.min.z;
            localSideB.z = bounds.max.z;

            widthDirection = transform.forward;
        }

        Vector3 worldCenter = transform.TransformPoint(localCenter);

        Vector3 worldSideA = transform.TransformPoint(localSideA);
        Vector3 worldSideB = transform.TransformPoint(localSideB);

        // Determine which side is actually left/right in world space
        float sideADirection =
            Vector3.Dot(worldSideA - worldCenter, widthDirection);

        float sideBDirection =
            Vector3.Dot(worldSideB - worldCenter, widthDirection);

        Vector3 leftPivot;
        Vector3 rightPivot;

        if (sideADirection < sideBDirection)
        {
            leftPivot = worldSideA;
            rightPivot = worldSideB;
        }
        else
        {
            leftPivot = worldSideB;
            rightPivot = worldSideA;
        }
        
        if (pivotSide == PivotSide.Left)
        {
            pivotPoint = leftPivot;
        }
        else
        {
            pivotPoint = rightPivot;
        }
    }

    private void CalculateOpenTransform()
    {
        float direction = openOutward ? 1f : -1f;

        float angle = openAngle * direction;

        Quaternion rotationAmount = Quaternion.AngleAxis(
            angle,
            transform.up
        );

        openRotation = rotationAmount * closedRotation;

        Vector3 offsetFromPivot =
            closedPosition - pivotPoint;

        openPosition =
            pivotPoint +
            rotationAmount * offsetFromPivot;
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