using UnityEngine;

public class EnemyMoveDown : IEnemyAction
{
    private Transform _transform;
    private float _moveSpeed;
    private Enemy _enemy;
    public EnemyMoveDown(Transform transform, float moveSpeed, Enemy enemy)
    {
        _transform = transform;
        _moveSpeed = moveSpeed;
        _enemy = enemy;
    }

    public void Act(float deltaTime)
    {
        var step = _moveSpeed * Time.deltaTime;
        _transform.position = Vector3.MoveTowards(_transform.position, _transform.position + Vector3.down, step);
    }
}
