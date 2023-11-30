using UnityEngine;


public class MoveDownBeheviour : IEnemyBeheviour
{
    private EnemyMoveDown _moveDown;

    public MoveDownBeheviour(Transform transform, float moveSpeed, Enemy enemy)
    {
        _moveDown = new EnemyMoveDown(transform, moveSpeed, enemy);
    }

    
    public void GoExecute(float deltaTime)
    {
        _moveDown.Act(deltaTime);
    }
}
