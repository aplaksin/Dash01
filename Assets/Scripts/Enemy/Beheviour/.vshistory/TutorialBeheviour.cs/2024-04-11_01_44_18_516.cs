
using UnityEngine;

public class TutorialBeheviour : IEnemyBeheviour
{
    private EnemyMoveDown _moveDown;
    private Enemy _enemy;
    public TutorialBeheviour(Transform transform, Enemy enemy)
    {
        _moveDown = new EnemyMoveDown(transform, 0, enemy);
        _enemy = enemy;
    }

    public void GoExecute(float deltaTime)
    {
        if (_enemy.transform.position.y > 7)
        {
            _moveDown.Act(deltaTime);
        }
    }
}
