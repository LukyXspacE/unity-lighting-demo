using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    
    [Header("References")]
    public Camera playerCamera;
    
    // Private variables
    private CharacterController controller;
    private float xRotation = 0f;
    
    // Input variables
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool spacePressed;
    private bool shiftPressed;
    
    void Start()
    {
        // Get the CharacterController component
        controller = GetComponent<CharacterController>();
        
        // Lock cursor to center of screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        
        // If no camera is assigned, try to find one
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        Debug.Log("movement script is compiled v1");
    }
    
    void Update()
    {
        // Get input from the new Input System
        GetInput();
        
        // Handle mouse look
        HandleMouseLook();
        
        // Handle movement
        HandleMovement();

    }
    
    void GetInput()
    {
        // Get WASD movement input using new Input System
        if (Keyboard.current != null)
        {
            float horizontal = 0f;
            float vertical = 0f;
            
            if (Keyboard.current.aKey.isPressed) horizontal = -1f;
            if (Keyboard.current.dKey.isPressed) horizontal = 1f;
            if (Keyboard.current.sKey.isPressed) vertical = -1f;
            if (Keyboard.current.wKey.isPressed) vertical = 1f;
            
            moveInput = new Vector2(horizontal, vertical);
        }
        
        // Get mouse input using new Input System
        if (Mouse.current != null)
        {
            lookInput = Mouse.current.delta.ReadValue();
        }
        
        // Get space and shift input
        spacePressed = Keyboard.current != null && Keyboard.current.spaceKey.isPressed;
        shiftPressed = Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed;
    }
    
    void HandleMouseLook()
    {
        // Apply mouse sensitivity and adjust for frame rate
        float mouseX = lookInput.x * mouseSensitivity * 0.01f;
        float mouseY = lookInput.y * mouseSensitivity * 0.01f;
        
        // Rotate the player body left and right
        transform.Rotate(Vector3.up * mouseX);
        
        // Rotate the camera up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    
    void HandleMovement()
    {
        // Calculate movement direction relative to where player is facing
        Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;
        
        // Handle up and down movement
        float upDown = 0f;
        if (spacePressed)
        {
            upDown = 1f; // Move up
        }
        else if (shiftPressed)
        {
            upDown = -1f; // Move down
        }
        
        // Add vertical movement to direction
        direction.y = upDown;
        
        // Move the character
        controller.Move(direction * moveSpeed * Time.deltaTime);
    }
}