using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SwipeInputManager : InputService
{

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;



    //public static InputManager Instance;

    private PlayerControls _playerControls;
    private Camera _mainCamera;
    private SwipeDetection _swipeDetector;

    private Vector2 _startPosition;
    private float _startTime;
    private Vector2 _endPosition;
    private float _endTime;

    public SwipeInputManager()
    {
        //Instance = this;
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        _mainCamera = Camera.main;
        Subscribe();
        _swipeDetector = new SwipeDetection();
        
    }
    /*    private void Awake()
        {
            //Instance = this;
            _playerControls = new PlayerControls();
            _playerControls.Enable();
            _mainCamera = Camera.main;
            //_swipeDetector = new SwipeDetection(this);
            Subscribe();
            DontDestroyOnLoad(this);
        }*/

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


        //OnStartTouch?.Invoke(Utils.ScreenToWorld(Camera.main, _playerControls.SwipeMove.TouchPosition.ReadValue<Vector2>()), (float)ctx.startTime);
        //OnStartTouch(_playerControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)ctx.startTime);
        _startPosition = Utils.ScreenToWorld(Camera.main, _playerControls.SwipeMove.TouchPosition.ReadValue<Vector2>());
        _startTime = (float)ctx.startTime;

    }
    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {

        //OnEndTouch?.Invoke(Utils.ScreenToWorld(Camera.main, _playerControls.SwipeMove.TouchPosition.ReadValue<Vector2>()), (float)ctx.time);
        //OnEndTouch(_playerControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)ctx.time);

        _endPosition = Utils.ScreenToWorld(Camera.main, _playerControls.SwipeMove.TouchPosition.ReadValue<Vector2>());
        _endTime = (float)ctx.time;
        InvokeOnMove(_swipeDetector.DetectSwipe(_startPosition, _startTime, _endPosition, _endTime));

    }

}
