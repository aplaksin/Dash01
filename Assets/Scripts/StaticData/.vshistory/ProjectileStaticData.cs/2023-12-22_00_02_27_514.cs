using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStaticData", menuName = "StaticData/Projectile")]
public class ProjectileStaticData : ScriptableObject
{


    [Range(1f, 30f)]
    public float Damage;

    [Range(0.1f, 10f)]
    public float MoveSpeed;

    public ProjectileType Type;

    public AudioClip ShootSound;

    public SkinListStaticData SkinListStaticData;
}
