using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveDown : IEnemyAction
{
    private Transform _transform;
    private float _moveSpeed;
    public EnemyMoveDown(Transform transform, float moveSpeed)
    {
        _transform = transform;
        _moveSpeed = moveSpeed;
    }

    public void Act(float deltaTime)
    {
        var step = _moveSpeed * Time.deltaTime;
        _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + Vector3.down, step);
    }
}
