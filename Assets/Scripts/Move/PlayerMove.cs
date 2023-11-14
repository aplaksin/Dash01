using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private IInputService _inputService;
    private IGameFactory _gameFactory;

    private Dictionary<Vector2, Vector3> _cellPositionByCoords;
    private Dictionary<Vector2, GameObject> _blocksByCoords;
    private Vector2 _currentPlayerCoords;
    private Vector3 _movePosition;
    private bool _isMoving = false;
    private GameObject _fireBlock;

    [SerializeField]
    private float _moveSpeed = 1f;

    public void Init(Dictionary<Vector2, Vector3> cellPositionByCoords, Dictionary<Vector2, GameObject> blocksByCoords, Vector2 currentPlayerCoords, IInputService inputService, IGameFactory gameFactory)
    {
        _cellPositionByCoords = cellPositionByCoords;
        _blocksByCoords = blocksByCoords;
        _currentPlayerCoords = currentPlayerCoords;
        _inputService = inputService;
        _inputService.SubscribeOnMoveEvent(Move);
        _gameFactory = gameFactory;
    }

    private void Update()
    {
        if (_movePosition != Vector3.zero)
        {
            _isMoving = true;
            var step = _moveSpeed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, _movePosition, step);

            if (Vector3.Distance(transform.position, _movePosition) < 0.001f)
            {

                transform.position = _movePosition;
                _movePosition = Vector3.zero;
                _isMoving = false;
                if (_fireBlock != null)
                {
                    
                    Projectile projectile = _gameFactory.CreateProjectile(_fireBlock.transform.position);
                    projectile.gameObject.SetActive(true);
                    projectile.PlayShootSFX();
                    _fireBlock = null;
                }
            }
        }
    }

    private void OnEnable()
    {
        _inputService?.SubscribeOnMoveEvent(Move);
    }

    private void OnDisable()
    {
        _inputService.UnsubscribeOnMoveEvent(Move);
    }

    private void Move(Vector2 direction)
    {
        
        if(direction != Vector2.zero)
        {
            CalcMovePlayerPosition(direction);
        }
    }

    private void CalcMovePlayerPosition(Vector2 direction)
    {
        Vector2 currentDirection = direction;
        Vector3 moveTarget = Vector3.zero;

        if (!_isMoving)
        {
            while (_cellPositionByCoords.ContainsKey(_currentPlayerCoords + direction) && !_blocksByCoords.ContainsKey(_currentPlayerCoords + direction))
            {
                currentDirection = _currentPlayerCoords + direction;
                _currentPlayerCoords = currentDirection;
                moveTarget = _cellPositionByCoords[currentDirection];
                
                if (_blocksByCoords.ContainsKey(_currentPlayerCoords + direction))
                {
                    _fireBlock = _blocksByCoords[currentDirection + direction];
                }
            }

            _movePosition = moveTarget;
        }

    }

}
