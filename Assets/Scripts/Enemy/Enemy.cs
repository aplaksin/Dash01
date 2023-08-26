using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public EnemyType _type;
    private float _moveSpeed;
    private float _damage;
    private float _hp;

    private Vector3 direction = Vector3.down;
    private IPoolingService _poolService;
    private const float  MIN_Y_POSITION = -5f;
    public EnemyType Type { get { return _type; } }

    public void Construct(EnemyStaticData enemyStaticData, IPoolingService poolingService)
    {
        _poolService = poolingService;

        if(_type != enemyStaticData.Type)
        {
            Debug.Log($"========== Wrong EnemyStaticData for this {_type} - {enemyStaticData.Type}");
        }
        
        _moveSpeed = enemyStaticData.MoveSpeed;
        _damage = enemyStaticData.Damage;
        _hp = enemyStaticData.Hp;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y < MIN_Y_POSITION) {
            _poolService.ReturnEnemy(this);
        }
        var step = _moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, transform.position+direction, step);
    }
}
