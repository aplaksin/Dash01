using UnityEngine;


public class ZigzagEnemyBehaviour : IEnemyBeheviour
{
    private Transform _transform;
    private Vector3 _minXPosition;
    private Vector3 _maxXPosition;
    private IEnemyAction _currentAction;
    private IEnemyAction _moveDown;
    private EnemyMoveHorizontal _moveHorizontal;
    private float _lastYP;
    private EnemyActionType _currentActionType = EnemyActionType.MoveVertical;
    private float _trashold = 0.002f;

    public ZigzagEnemyBehaviour(Transform transform, Vector3 minXPosition, Vector3 maxXPosition, float moveSpeed, Enemy enemy)
    {
        _transform = transform;
        _minXPosition = minXPosition;
        _maxXPosition = maxXPosition;

        _moveDown = new EnemyMoveDown(transform, moveSpeed, enemy);
        _moveHorizontal = new EnemyMoveHorizontal(transform, moveSpeed, enemy);

        _currentAction = _moveHorizontal;
        _lastYP = transform.position.y;
    }

    public void GoExecute(float deltaTime)
    {

        if(_currentActionType == EnemyActionType.MoveVertical)
        {
            bool changeAction = false;

            if(_transform.position.x - _minXPosition.x < _trashold)
            {
                _transform.position = new Vector3(_minXPosition.x, _transform.position.y, _transform.position.z);
                changeAction = true;
            }

            if ( _maxXPosition.x - _transform.position.x < _trashold) 
            {
                _transform.position = new Vector3(_maxXPosition.x, _transform.position.y, _transform.position.z);
                changeAction = true;
            }

            if(changeAction)
            {
                _currentAction = _moveDown;
                _currentActionType = EnemyActionType.MoveDown;
                _lastYP = _transform.position.y;
                _moveHorizontal.SwapDirection();
            }

        //} else if (_currentActionType == EnemyActionType.MoveDown && _lastYP != -100f && (Vector3.Distance(new Vector3(0, _transform.position.y, 0), new Vector3(0, _lastYP - 1 * _transform.localScale.y, 0)) < 0.002f))
        } else if (_currentActionType == EnemyActionType.MoveDown && _lastYP != -100f && (_transform.position.y - (_lastYP - 1 * _transform.localScale.y) < 0))
        {
            _transform.position = new Vector3(_transform.position.x, _lastYP - _transform.localScale.y, _transform.position.z);
            _currentAction = _moveHorizontal;
            _currentActionType = EnemyActionType.MoveVertical;
            _lastYP = -100f;
        }


        _currentAction.Act(deltaTime);
    }
}
