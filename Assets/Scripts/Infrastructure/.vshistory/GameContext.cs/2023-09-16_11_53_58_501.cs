

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

    }

    private void OnScoreChanged(int score)
    {
        _score += score;
        EventManager.CallOnScoreChanged(_score);
    }

    public void Clear()
    {
        EventManager.OnEnemyDeath -= OnScoreChanged;
    }

}
