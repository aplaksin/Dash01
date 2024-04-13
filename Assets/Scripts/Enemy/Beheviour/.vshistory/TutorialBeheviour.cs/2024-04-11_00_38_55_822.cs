
using UnityEngine;

public class TutorialBeheviour : IEnemyBeheviour
{
    private EnemyMoveDown _moveDown;
    public TutorialBeheviour(Transform transform, Enemy enemy)
    {
        _moveDown = new EnemyMoveDown(transform, 0, enemy);
    }

    public void GoExecute(float deltaTime)
    {
        _moveDown.Act(deltaTime);
    }
}
