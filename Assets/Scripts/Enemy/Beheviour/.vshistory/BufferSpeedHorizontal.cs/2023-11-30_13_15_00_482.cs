using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferSpeedHorizontal : IEnemyBeheviour
{
    private Transform _transform;
    private Vector3 _minXPosition;
    private Vector3 _maxXPosition;
    private IEnemyAction _currentAction;
    private EnemyMoveHorizontal _moveHorizontal;
    private EnemyActionType _currentActionType = EnemyActionType.MoveVertical;

    public BufferSpeedHorizontal(Transform transform, Vector3 minXPosition, Vector3 maxXPosition, float moveSpeed)
    {
        _transform = transform;
        _minXPosition = minXPosition;
        _maxXPosition = maxXPosition;

        _moveHorizontal = new EnemyMoveHorizontal(transform, moveSpeed);

        _currentAction = _moveHorizontal;
    }

    public void GoExecute(float deltaTime)
    {

        if (_currentActionType == EnemyActionType.MoveVertical)
        {

            if (_transform.position.x - _minXPosition.x < 0.002f)
            {
                _transform.position = new Vector3(_minXPosition.x, _transform.position.y, _transform.position.z);
                changeAction = true;
            }

            if (_maxXPosition.x - _transform.position.x < 0.002f)
            {
                _transform.position = new Vector3(_maxXPosition.x, _transform.position.y, _transform.position.z);
                changeAction = true;
            }

            if (changeAction)
            {
                _currentAction = _moveDown;
                _currentActionType = EnemyActionType.MoveDown;
                _lastYP = _transform.position.y;
                _moveHorizontal.SwapDirection();
            }

            //} else if (_currentActionType == EnemyActionType.MoveDown && _lastYP != -100f && (Vector3.Distance(new Vector3(0, _transform.position.y, 0), new Vector3(0, _lastYP - 1 * _transform.localScale.y, 0)) < 0.002f))
        }
        else if (_currentActionType == EnemyActionType.MoveDown && _lastYP != -100f && (_transform.position.y - (_lastYP - 1 * _transform.localScale.y) < 0))
        {
            _transform.position = new Vector3(_transform.position.x, _lastYP - _transform.localScale.y, _transform.position.z);
            _currentAction = _moveHorizontal;
            _currentActionType = EnemyActionType.MoveVertical;
            _lastYP = -100f;
        }


        _currentAction.Act(deltaTime);
    }
}
