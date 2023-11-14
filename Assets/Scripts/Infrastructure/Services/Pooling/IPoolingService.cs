using UnityEngine;

public interface IPoolingService : IService
{
    void Construct();
    Enemy GetEnemyByType(EnemyType enemyType);
    void ReturnEnemy(Enemy enemy);

    Projectile GetProjectileByType(ProjectileType projectileType);
    void ReturnProjectile(Projectile projectile);
}
