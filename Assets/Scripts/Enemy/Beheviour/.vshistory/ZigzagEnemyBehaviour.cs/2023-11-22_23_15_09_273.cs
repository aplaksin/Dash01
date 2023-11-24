using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZigzagEnemyBehaviour : IEnemyBeheviour
{
    private Transform _transform;
    private Vector3 _minXPosition;
    private Vector3 _maxXPosition;
    private Vector2 _scaleVector;
    private float _moveSpeed;
    private Vector2 _currentDirection;
    private IEnemyAction _currentAction;
    private IEnemyAction _moveDown;
    private EnemyMoveVertical _moveVertical;
    private float _lastYP;
    private EnemyActionType _currentActionType = EnemyActionType.MoveVertical;

    public ZigzagEnemyBehaviour(Transform transform, Vector3 minXPosition, Vector3 maxXPosition, Vector3 scaleVector, float moveSpeed)
    {
        _transform = transform;
        _minXPosition = minXPosition;
        _maxXPosition = maxXPosition;
        _scaleVector = scaleVector;
        _moveSpeed = moveSpeed;

        _moveDown = new EnemyMoveDown(transform, moveSpeed);
        _moveVertical = new EnemyMoveVertical(transform, /*minXPosition, maxXPosition,*/ moveSpeed);

        _currentAction = _moveVertical;
        _lastYP = transform.position.y;
    }

    public void GoExecute(float deltaTime)
    {
/*        if(_currentAction == _moveVertical &&
            (Vector3.Distance(new Vector3(_transform.position.x, 0, 0), new Vector3(_minXPosition.x, 0, 0)) < 0.02f 
            || Vector3.Distance(new Vector3(_transform.position.x, 0, 0), new Vector3(_maxXPosition.x, 0, 0)) < 0.02f))
        {
            

            _currentAction = _moveDown;
            _lastYP = _transform.position.y;
            _moveVertical.SwapDirection();
        }*/

        if(_currentActionType == EnemyActionType.MoveVertical)
        {
            bool changeAction = false;

            Debug.Log("================ minXPosition " + (_transform.position.x - _minXPosition.x) + "   ================ minXPosition ");


           
            Debug.Log("================ _maxXPosition " + (_transform.position.x - _maxXPosition.x ) + "   ================ _maxXPosition ");


           


//if(Vector3.Distance(new Vector3(_transform.position.x, 0, 0), new Vector3(_minXPosition.x, 0, 0)) < 0.1f)
            if(_transform.position.x - _minXPosition.x < 0.02f)
            {
                _transform.position = new Vector3(_minXPosition.x, _transform.position.y, _transform.position.z);
                changeAction = true;
            }
//if(Vector3.Distance(new Vector3(_transform.position.x, 0, 0), new Vector3(_maxXPosition.x, 0, 0)) < 0.1f)
            if (_transform.position.x -_maxXPosition.x < 0.02f) 
            {
                _transform.position = new Vector3(_maxXPosition.x, _transform.position.y, _transform.position.z);
                changeAction = true;
            }

            if(changeAction)
            {
                _currentAction = _moveDown;
                _currentActionType = EnemyActionType.MoveDown;
                _lastYP = _transform.position.y;
                _moveVertical.SwapDirection();
            }

        } else if (_currentActionType == EnemyActionType.MoveDown && _lastYP != -100f && (_transform.position.y - (_lastYP - 2 * _transform.localScale.y) <= 0))
        {
            _currentAction = _moveVertical;
            _currentActionType = EnemyActionType.MoveVertical;
            _lastYP = -100f;
        }


        //if(_currentAction != _moveVertical && (Vector3.Distance(new Vector3(0, _transform.position.y, 0), new Vector3(0, _lastYP - 2*_transform.localScale.y,0)) < 0.07f))
/*        if (_currentAction == _moveDown && (_transform.position.y - (_lastYP - 2 * _transform.localScale.y) <= 0))
        {
            _currentAction = _moveVertical;
            _lastYP = -10f;
        }*/

/*        if (_currentAction == _moveDown)
        {
            Debug.Log("-=============================  _transform.position.y:" + _transform.position.y + "   _lastYP - 2 * _transform.localScale.y " + (_lastYP - 2 * _transform.localScale.y));
        }*/

        _currentAction.Act(deltaTime);
    }
}
