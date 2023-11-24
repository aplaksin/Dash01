using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigzagEnemyBehaviour : IEnemyBeheviour
{
    private float _minXPosition;
    private float _maxXPosition;
    private Vector2 _scaleVector;
    private float float _speed;

    public ZigzagEnemyBehaviour(float minXPosition, float maxXPosition, Vector3 scaleVector, float speed)
    {

    }

    public void Execute(float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}
