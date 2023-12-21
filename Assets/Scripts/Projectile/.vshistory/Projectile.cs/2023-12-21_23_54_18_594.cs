using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    public ProjectileType _type;
    public ProjectileType Type { get { return _type; } }
    public float Damage { get { return _damage; } }

    private float _moveSpeed;
    private float _damage;
    private AudioClip _shootSound;

    private IAudioService _audioService;

    private Vector3 direction = Vector3.up;
    private IPoolingService _poolService;
    private const float MAX_Y_POSITION = 15f;
    public ProjectileType Type { get { return _type; } }

    public void Construct(ProjectileStaticData projectileStaticData, IPoolingService poolingService, IAudioService audioService)
    {
        
        _poolService = poolingService;
        _audioService = audioService;
        
        if (_type != projectileStaticData.Type)
        {
            Debug.Log($"========== Wrong ProjectileStaticData for this {_type} - {projectileStaticData.Type}");
        }

        _moveSpeed = projectileStaticData.MoveSpeed;
        _damage = projectileStaticData.Damage;
        _shootSound = projectileStaticData.ShootSound;
    }
    public void OnDamageEnemy()
    {
        _poolService.ReturnProjectile(this);
    }

    public void PlayShootSFX()
    {
        _audioService.PlaySFX(_shootSound);
    }

    private void Update()
    {
        if (transform.position.y > MAX_Y_POSITION)
        {
            _poolService.ReturnProjectile(this);
        }
        var step = _moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, step);
        
    }



    
}
