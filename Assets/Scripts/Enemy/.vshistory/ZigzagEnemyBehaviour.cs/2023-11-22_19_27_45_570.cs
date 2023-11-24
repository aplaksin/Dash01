using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigzagEnemyBehaviour : IEnemyBeheviour
{
    private Transform _transform;
    private float _minXPosition;
    private float _maxXPosition;
    private Vector2 _scaleVector;
    private float _moveSpeed;
    private Vector2 _currentDirection;
    private IEnemyAction _currentAction;
    private IEnemyAction _moveDown;
    private IEnemyAction _moveVertical;

    public ZigzagEnemyBehaviour(Transform transform, float minXPosition, float maxXPosition, Vector3 scaleVector, float moveSpeed)
    {
        _moveDown = new EnemyMoveDown(transform, moveSpeed);
        _moveVertical = new Ene
    }

    public void Execute(float deltaTime)
    {

        _currentAction.Act(deltaTime);
    }
}
