using UnityEngine;
using UnityEngine.InputSystem;
// Need this to change the virtual mouse state.
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    [SerializeField]
    // This is the input actions that reads the Gamepad events.
    private PlayerInput playerInput;
    [SerializeField]
    // Create a reference to the console cursor ui element.
    private RectTransform cursorTransform;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private RectTransform canvasRectTransform;
    [SerializeField]
    private float cursorSpeed = 1000f;
    // Padding is so that the cursor isn't right on the edge of the screen.
    private float padding = 160f;

    // Used to keep track of the button for clicking.
    private bool previousMouseState;
    // This is the console cursor on the screen.
    private Mouse virtualMouse;
    private Camera mainCamera;

    private void OnEnable()
    {
        mainCamera = Camera.main;
        if (virtualMouse == null)
        {
            virtualMouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        // This will connect the virtual mouse with the inputs from the controller.
        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        // Setting the initial position of the cursor.
        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
    }

    // This function is for in case we disable this script.
    private void OnDisable()
    {
        InputSystem.RemoveDevice(virtualMouse);
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    // Read input of gamepad and make those changes to our virtual mouse.
    private void UpdateMotion()
    {
        if (virtualMouse == null || Gamepad.current == null)
        {
            return;
        }

        // Get value inputted by the joystick.
        Vector2 deltaValue = Gamepad.current.leftStick.ReadValue();
        deltaValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        // This new position of the virtual cursor will be equal to its
        // current position + the value inputted by the joystick.
        Vector2 newPosition = currentPosition + deltaValue;

        // Don't want to go past the screen.
        newPosition.x = Mathf.Clamp(newPosition.x, padding-80f, Screen.width-padding+80f);
        newPosition.y = Mathf.Clamp(newPosition.y, padding, Screen.height-padding);

        // Change the actual position of the virtual mouse.
        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);

        // Logic to control the click of a gamepad button.
        bool bButtonIsPressed = Gamepad.current.bButton.IsPressed();
        if (previousMouseState != bButtonIsPressed)
        {
            // Getting state of virtual mouse.
            virtualMouse.CopyState<MouseState>(out var mouseState);
            // Map east gamepad button with left click on mouse.
            mouseState.WithButton(MouseButton.Left, bButtonIsPressed);
            // Update virtual mouse info based on the click.
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = bButtonIsPressed;
        }

        // Now change the cursor on the screen.
        AnchorCursor(newPosition);
    }

    // Puts the virtual mouse in the correct spot relative to the canvas.
    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
