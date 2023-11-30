using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public EnemyType _type;

    [SerializeField]
    private ParticleSystem _particleDamage;

    [SerializeField]
    private ParticleSystem _particleDeath;

    public float _moveSpeed;
    private int _damage;
    private int _hp;
    private float _currentHp;
    private int _score;

    private Vector3 direction = Vector3.down;
    private IPoolingService _poolService;
    private const float  MIN_Y_POSITION = -0.5f;
    private EnemyStaticData _enemyStaticData;
    private IAudioService _audioService;
    public EnemyType Type { get { return _type; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public IEnemyBeheviour EnemyBeheviour;


    public void Construct(EnemyStaticData enemyStaticData, IPoolingService poolingService, IAudioService audioService)
    {
        _poolService = poolingService;
        _enemyStaticData = enemyStaticData;
        _audioService = audioService;
        
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

        if (EnemyBeheviour == null)
        {
            Move();
        }
        else
        {
            EnemyBeheviour.GoExecute(Time.deltaTime);
        }

    }

    private void Move()
    {
        var step = _moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        if (projectile != null)
        {
            StartCoroutine(TakeDmage(projectile.Damage));
            //TakeDmage(projectile.Damage);
            projectile.OnDamageEnemy();
            
        }
    }

    private IEnumerator TakeDmage(float damage)
    {
        _currentHp -= damage;

        if (_currentHp<=0)
        {
            
            _particleDeath.Play();
            yield return new WaitForSeconds(_particleDeath.main.duration);
            
            _currentHp=_hp;
            //UnsubscribeOnEvents();
            InitBaseParams();

            
            
            _poolService.ReturnEnemy(this);
            EventManager.CallOnEnemyDeathEvent(_score);
            _audioService.PlaySFX(_enemyStaticData.DeathClip);
        }
        else
        {
            _particleDamage.Play();
            yield return new WaitForSeconds(_particleDamage.main.duration);
            _audioService.PlaySFX(_enemyStaticData.TakeDamageClip);
        }
    }

    private void ChangePropertiesByStage(GameStageStaticData stage)
    {
        _moveSpeed *= stage.EnemySpeedScale !=0 ? stage.EnemySpeedScale : 1;
        _damage += stage.AdditionalDamage;
    }

/*    private void SubscribeOnEvents()
    {
        EventManager.OnChangeGameStage += ChangePropertiesByStage;
    }

    private void UnsubscribeOnEvents()
    {
        EventManager.OnChangeGameStage -= ChangePropertiesByStage;
    }*/

}
