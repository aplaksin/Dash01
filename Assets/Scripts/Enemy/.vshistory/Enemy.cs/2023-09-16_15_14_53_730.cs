using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public EnemyType _type;
    private float _moveSpeed;
    private int _damage;
    private int _hp;
    private float _currentHp;
    private int _score;

    private Vector3 direction = Vector3.down;
    private IPoolingService _poolService;
    private const float  MIN_Y_POSITION = -1f;
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
        _currentHp = _hp;
        _score = enemyStaticData.Score;
    }


    private void Update()
    {
        if (transform.position.y < MIN_Y_POSITION) {
            _poolService.ReturnEnemy(this);
            EventManager.CallOnDamageEvent(_damage);
        }
        var step = _moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, transform.position+direction, step);
    }

/*    private void OnEnable()
    {
        EventManager.OnEnemyDeath += EnmemyDeath;
    }  
    private void OnDisable()
    {
        EventManager.OnEnemyDeath -= EnmemyDeath;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        if (projectile != null)
        {
            TakeDmage(projectile.Damage);
            projectile.OnDamageEnemy();
            
        }
    }

/*    private void EnmemyDeath()
    {

    }*/

    private void TakeDmage(float damage)
    {
        _currentHp -= damage;
        if(_currentHp<=0)
        {
            _currentHp=_hp;
            _poolService.ReturnEnemy(this);
            EventManager.CallOnEnemyDeathEvent(_score);
        }
    }

    private void ChangePropertiesByStage(GameProgressionStaticData stage)
    {
        _moveSpeed *= stage.EnemySpeedScale;
        _damage += stage.AdditionalDamage;
    }

    private void SubscribeOnEvents()
    {
        EventManager.OnChangeGameStage += ChangePropertiesByStage;
    }

    private void UnsubscribeOnEvents()
    {
        EventManager.OnChangeGameStage -= ChangePropertiesByStage;
    }

}
