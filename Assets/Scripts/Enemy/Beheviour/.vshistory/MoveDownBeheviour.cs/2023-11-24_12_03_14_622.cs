using UnityEngine;


public class MoveDownBeheviour : IEnemyBeheviour
{
    private EnemyMoveDown _moveDown;

    public MoveDownBeheviour(Transform transform, float moveSpeed)
    {
        _moveDown = new EnemyMoveDown(transform, moveSpeed);
    }

    
    public void GoExecute(float deltaTime)
    {
        _moveDown.Act(deltaTime);
    }
}
