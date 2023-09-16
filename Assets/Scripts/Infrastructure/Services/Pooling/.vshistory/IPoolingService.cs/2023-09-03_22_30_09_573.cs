using UnityEngine;

public interface IPoolingService : IService
{
    void Construct();
    GameObject GetEnemyByType(EnemyType enemyType);
    void ReturnEnemy(Enemy enemy);

    GameObject GetProjectileByType(ProjectileType projectileType);
    void ReturnProjectile(Projectile projectile);
}
