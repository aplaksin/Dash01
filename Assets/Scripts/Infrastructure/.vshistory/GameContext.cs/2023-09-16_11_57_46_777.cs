

public class GameContext
{
    public int _playerHP;
    public int _score;
    public LevelStaticData _levelStaticData;


    public GameContext(int playerHp, int score, LevelStaticData levelStaticData)
    {
        _playerHP = playerHp;
        _score = score;
        _levelStaticData = levelStaticData;
        EventManager.OnEnemyDeath += OnScoreChanged;
        EventManager.OnDamage += OnHpChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score += score;
        EventManager.CallOnScoreChanged(_score);
    }

    private void OnHpChanged(int hp)
    {
        _playerHP -= hp;
        if(_playerHP <= 0)
        {
            Clear();
            EventManager.CallOnGameOver();
        }
    }

    public void Clear()
    {
        EventManager.OnEnemyDeath -= OnScoreChanged;
        EventManager.OnDamage -= OnHpChanged;
    }

}
