

public class GameContext
{
    private int _playerHP;
    private int _score = 0;
    private LevelStaticData _levelStaticData;

    public int Score { get { return _score; } }

    public GameContext(LevelStaticData levelStaticData)
    {
        _playerHP = levelStaticData.PlayerHP;
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

        EventManager.CallOnHpChanged(_playerHP);
        
    }

    public void Clear()
    {
        EventManager.OnEnemyDeath -= OnScoreChanged;
        EventManager.OnDamage -= OnHpChanged;
    }

}
