using System.Collections;
using System.Collections.Generic;
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
    public EnemyType Type { get { return _type; } }
    public float MoveSpeed { get { return _moveSpeed + Game.GameContext.GetBuffValueByType(EnemyBuffType.Speed); } }
    public string Id { get { return _id; } }

    public List<IEnemyBuff> BuffsList = new List<IEnemyBuff>();
    public IEnemyBeheviour EnemyBeheviour;
    public bool IsTutorialEnemy = false;
    //public float AdditionalSpeed = 0;


    public float _moveSpeed;
    private int _damage;
    private int _hp;
    private float _currentHp;
    private int _score;

    private IPoolingService _poolService;
    private const float  MIN_Y_POSITION = -0.5f;
    private EnemyStaticData _enemyStaticData;
    private IAudioService _audioService;
    private bool _isDead = false;
    private string _id;

    public void Construct(EnemyStaticData enemyStaticData, IPoolingService poolingService, IAudioService audioService)
    {
        _poolService = poolingService;
        _enemyStaticData = enemyStaticData;
        _audioService = audioService;
        _id = gameObject.name;

        if (_type != enemyStaticData.Type)
        {
            Debug.Log($"========== Wrong EnemyStaticData for this {_type} - {enemyStaticData.Type}");
        }

        
        InitBaseParams();
        
        
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
        _isDead = false;
        //AdditionalSpeed = 0;
        BuffsList.Clear();
        SetupBuffs(_enemyStaticData);
    }

    private void Update()
    {
        if (transform.position.y < MIN_Y_POSITION) {
            _poolService.ReturnEnemy(this);
            EventManager.CallOnDamageEvent(_damage);
        }

        if (!_isDead)
        {
            EnemyBeheviour.GoExecute(Time.deltaTime);
        }

    }

    private void SetupBuffs(EnemyStaticData enemyStaticData)
    {
        if (enemyStaticData.EnemyBuffSettings != null && enemyStaticData.EnemyBuffSettings.Count > 0)
        {
            foreach (EnemyBuffSettings buffSettings in enemyStaticData.EnemyBuffSettings)
            {

                switch(buffSettings.Type)
                {
                    case EnemyBuffType.Speed:
                        BuffsList.Add(new EnemySpeedBuff(buffSettings.Value));
                        break;
                    default:
                        break;
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        if (projectile != null)
        {
            StartCoroutine(TakeDmage(projectile.Damage));
            projectile.OnDamageEnemy();
            
        }
    }

    private IEnumerator TakeDmage(float damage)
    {
        _currentHp -= damage;

        if (_currentHp<=0)
        {
            _isDead = true;
            _particleDeath.Play();
            yield return new WaitForSeconds(_particleDeath.main.duration);
            
            _currentHp =_hp;
            RemoveBuffs();
            

            _poolService.ReturnEnemy(this);

            if(IsTutorialEnemy)
            {
                EventManager.CallOnTutorialEnemyDeathEvent(_score);
            }
            else
            {
                EventManager.CallOnEnemyDeathEvent(_score);
            }
            
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

    private void RemoveBuffs()
    {
        if(BuffsList.Count != 0)
        {
            Game.GameContext.RemoveEnemyBuff(BuffsList);
        }
        
    }

}
