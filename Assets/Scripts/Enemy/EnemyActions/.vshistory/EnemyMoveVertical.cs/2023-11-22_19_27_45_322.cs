using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveVertical : IEnemyAction
{
    private Transform _transform;
    private float _minXPosition;
    private float _maxXPosition;
    private float _moveSpeed;
    private Vector3 _currentDirection;
    public EnemyMoveVertical(Transform transform, float minXPosition, float maxXPosition, float moveSpeed)
    {
        _transform = transform;
        _minXPosition = minXPosition;
        _maxXPosition = maxXPosition;
        _moveSpeed = moveSpeed;
        _currentDirection = Vector3.left;
    }

    public void SwapDirection()
    {
        _currentDirection = _currentDirection == Vector3.left ? Vector3.right : Vector3.left;
    }

    public void Act(float deltaTime)
    {
        var step = _moveSpeed * Time.deltaTime;
        _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + _currentDirection, step);
    }
}
