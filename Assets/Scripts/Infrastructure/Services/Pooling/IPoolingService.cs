using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolingService : IService
{
    void Construct();
    GameObject GetEnemyByType(EnemyType enemyType);
    void ReturnEnemy(Enemy enemy);

    GameObject GetProjectileByType(ProjectileType projectileType);
    void ReturnProjectile(Projectile projectile);
}
