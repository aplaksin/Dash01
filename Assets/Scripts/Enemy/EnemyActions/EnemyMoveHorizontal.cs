using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveHorizontal : IEnemyAction
{
    private Transform _transform;
    private float _minXPosition;
    private float _maxXPosition;
    private float _moveSpeed;
    private Vector3 _currentDirection;
    private Enemy _enemy;
    public EnemyMoveHorizontal(Transform transform, /*float minXPosition, float maxXPosition,*/ float moveSpeed, Enemy enemy)
    {
        _transform = transform;
        /*        _minXPosition = minXPosition;
                _maxXPosition = maxXPosition;*/
        _moveSpeed = moveSpeed;
        _currentDirection = Vector3.left;
        _enemy = enemy;
    }

    public void SwapDirection()
    {
        _currentDirection = _currentDirection == Vector3.left ? Vector3.right : Vector3.left;
    }

    public void Act(float deltaTime)
    {
        var step = _enemy.MoveSpeed * Time.deltaTime;
        _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + _currentDirection, step);
    }
}
