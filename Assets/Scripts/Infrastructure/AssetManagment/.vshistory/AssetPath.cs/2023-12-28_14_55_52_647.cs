using System.Collections.Generic;
using UnityEngine;

public static class AssetPath
{
    public const string PlayerPath = "Player/player";
    public const string HudPath = "Hud/hud";
    public const string DamageBorderPath = "Hud/damageBorder";
    public static string MenuPath = "UI/menu";
    public static string UITutorial = "UI/Tutorial";
    public static string PauseMenuPath = "UI/PauseMenu";
    public const string CellPath = "Grid/cell";
    public const string BlockPath = "Grid/block";
    public const string GridPath = "Grid/Grid";
    public const string MainMenuMusicPath = "Audio/Music/MainMenuMusic";
    public const string GameOverMusicPath = "Audio/Music/GameOverMusic";
    public const string PlayerDamageSFXPath = "Audio/SFX/PlayerDamage";

    private static Dictionary<EnemyType, string> _enemyPathByType = new Dictionary<EnemyType, string>() {
        [EnemyType.Base] = "Enemies/baseEnemy",
        [EnemyType.Tank] = "Enemies/tankEnemy",
        [EnemyType.ZigZag] = "Enemies/zigZagEnemy",
        [EnemyType.SpeedBufferHorizontal] = "Enemies/bufferSpeedHorizontal",
    };

    private static Dictionary<ProjectileType, string> _projectilePathByType = new Dictionary<ProjectileType, string>()
    {
        [ProjectileType.Base] = "Projectiles/Base",
    };
    
    public static string GetEnemyPathByType(EnemyType enemyType){
        string enemyPath;
        _enemyPathByType.TryGetValue(enemyType, out enemyPath);

        if(enemyPath == null || enemyPath.Length == 0)
        {
            Debug.Log($"========== enemyPath is empty for {enemyType} type");
        }

        return enemyPath;
    }

    public static string GetProjectilePathByType(ProjectileType projectileType)
    {
        string projectilePath;
        _projectilePathByType.TryGetValue(projectileType, out projectilePath);

        if (projectilePath == null || projectilePath.Length == 0)
        {
            Debug.Log($"========== projectilePath is empty for {projectileType} type");
        }

        return projectilePath;
    }


}
