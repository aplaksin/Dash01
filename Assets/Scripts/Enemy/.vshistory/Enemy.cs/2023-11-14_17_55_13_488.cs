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
    private EnemyStaticData _enemyStaticData;
    private IAudioService _audioService;
    public EnemyType Type { get { return _type; } }

    public void Construct(EnemyStaticData enemyStaticData, IPoolingService poolingService)
    {
        _poolService = poolingService;
        _enemyStaticData = enemyStaticData;
        _audioService = AllServices.Container.Single<IAudioService>();
        
        if (_type != enemyStaticData.Type)
        {
            Debug.Log($"========== Wrong EnemyStaticData for this {_type} - {enemyStaticData.Type}");
        }

        InitBaseParams();

        //SubscribeOnEvents();
    }

    public void InitProperties(GameStageStaticData stage)
    {
        ChangePropertiesByStage(stage);
    }
    public void InitBaseParams()
    {
        _moveSpeed = _enemyStaticData.MoveSpeed;
        _damage = _enemyStaticData.Damage;
        _hp = _enemyStaticData.Hp;
        _currentHp = _hp;
        _score =_enemyStaticData.Score;
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
            UnsubscribeOnEvents();
            InitBaseParams();
            _poolService.ReturnEnemy(this);
            EventManager.CallOnEnemyDeathEvent(_score);
            _audioService.PlayFxByType(SoundType.EnemyDeath);
        }
        else
        {
            _audioService.PlayFxByType(SoundType.EnemyDamage);
        }
    }

    private void ChangePropertiesByStage(GameStageStaticData stage)
    {
        _moveSpeed *= stage.EnemySpeedScale !=0 ? stage.EnemySpeedScale : 1;
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
