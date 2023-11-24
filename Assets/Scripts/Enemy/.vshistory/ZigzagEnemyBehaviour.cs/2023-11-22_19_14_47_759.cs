using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigzagEnemyBehaviour : IEnemyBeheviour
{
    private Transform _transform;
    private float _minXPosition;
    private float _maxXPosition;
    private Vector2 _scaleVector;
    private float _speed;
    private Vector2 _currentDirection;
    private IEnemyAction _currentAction;

    public ZigzagEnemyBehaviour(Transform transform, float minXPosition, float maxXPosition, Vector3 scaleVector, float speed)
    {
        
    }

    public void Execute(float deltaTime)
    {
        _currentAction.Act(deltaTime);
    }
}
