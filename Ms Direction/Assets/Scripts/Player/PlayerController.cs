
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Camera")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 85f;

    private Rigidbody rb;
    private Vector2 movementInput;
    private float cameraPitch;
    private float playerYaw;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        playerYaw = transform.eulerAngles.y;
    }

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        HandleInput();
        HandleMouseLook();
        HandleCursor();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        rb.MoveRotation(Quaternion.Euler(0f, playerYaw, 0f));
    }

    private void HandleInput()
    {
        movementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        movementInput = Vector2.ClampMagnitude(movementInput, 1f);
    }

    private void HandleMouseLook()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        playerYaw += mouseX;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);

        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    private void HandleMovement()
    {
        Quaternion rotation = Quaternion.Euler(0f, playerYaw, 0f);

        Vector3 direction = rotation * new Vector3(
            movementInput.x,
            0f,
            movementInput.y
        );

        Vector3 velocity = direction * moveSpeed;
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;
    }

    private void HandleCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
            LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}