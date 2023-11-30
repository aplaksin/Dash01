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


    public BufferSpeedHorizontal(Transform transform, Vector3 minXPosition, Vector3 maxXPosition, float moveSpeed, int initialYPosition)
    {
        _transform = transform;
        _transform.position = new Vector3(_transform.position.x, initialYPosition, _transform.position.z);
        _minXPosition = minXPosition;
        _maxXPosition = maxXPosition;

        _moveHorizontal = new EnemyMoveHorizontal(transform, moveSpeed);

        _currentAction = _moveHorizontal;
    }

    public void GoExecute(float deltaTime)
    {

        if (_transform.position.x - _minXPosition.x < 0.002f)
        {
            _transform.position = new Vector3(_minXPosition.x, _transform.position.y, _transform.position.z);
            _moveHorizontal.SwapDirection();
        }

        if (_maxXPosition.x - _transform.position.x < 0.002f)
        {
            _transform.position = new Vector3(_maxXPosition.x, _transform.position.y, _transform.position.z);
            _moveHorizontal.SwapDirection();
        }

        _currentAction.Act(deltaTime);
    }
}
