using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInputManager : InputService
{

    private PlayerControls _playerControls;

    public KeyboardInputManager()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        Subscribe();

    }

    private void Subscribe()
    {
        _playerControls.Keyboard.Move.started += ctx => GetDirection(ctx);
        
    }

    private void GetDirection(InputAction.CallbackContext ctx)
    {      
        Vector2 dir = _playerControls.Keyboard.Move.ReadValue<Vector2>();
        InvokeOnMove(dir);
    }
}
