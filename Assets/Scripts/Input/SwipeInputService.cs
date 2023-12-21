using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeInputService : InputService
{

    private PlayerControls _playerControls;
    private SwipeDetection _swipeDetector;

    private Vector2 _startPosition;
    private float _startTime;
    private Vector2 _endPosition;
    private float _endTime;

    public SwipeInputService()
    {

        _playerControls = new PlayerControls();
        _playerControls.Enable();
        Subscribe();
        _swipeDetector = new SwipeDetection();
        
    }

    public void SubscribeOnMoveEvent(Vector2 direction)
    {

    }

    private void Subscribe()
    {
        _playerControls.SwipeMove.TouchPrimary.started += ctx => StartTouchPrimary(ctx);
        _playerControls.SwipeMove.TouchPrimary.canceled += ctx => EndTouchPrimary(ctx);
    }


    public Vector2 TouchPosition()
    {
        return Utils.ScreenToWorld(Camera.main, _playerControls.SwipeMove.TouchPosition.ReadValue<Vector2>());
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {

        _startPosition = Utils.ScreenToWorld(Camera.main, _playerControls.SwipeMove.TouchPosition.ReadValue<Vector2>());
        _startTime = (float)ctx.startTime;

    }
    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {

        _endPosition = Utils.ScreenToWorld(Camera.main, _playerControls.SwipeMove.TouchPosition.ReadValue<Vector2>());
        _endTime = (float)ctx.time;
        InvokeOnMove(_swipeDetector.DetectSwipe(_startPosition, _startTime, _endPosition, _endTime));

    }

}
