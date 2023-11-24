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
    }

    public void Execute(float deltaTime)
    {
        if(Vector3.Distance(new Vector3(_transform.position.x, 0, 0), new Vector3(_minXPosition.x, 0, 0)) < 0.001f || Vector3.Distance(new Vector3(_transform.position.x, 0, 0), new Vector3(_maxXPosition.x, 0, 0)) < 0.001f)
        {
            _currentAction = _moveDown;
            _moveVertical.SwapDirection();
        }
        else if(Vector3.Distance(new Vector3(0, _transform.position.y,0), new Vector3(0, _transform.position.y*_scaleVector.y,0)) < 0.001f)
        {
            _currentAction = _moveVertical;
        }

        _currentAction.Act(deltaTime);
    }
}
