

public class GameContext
{
    public int PlayerHP;
    public int Score;
    public LevelStaticData LevelStaticData;


    public GameContext(int playerHp, int score, LevelStaticData levelStaticData)
    {
        PlayerHP = playerHp;
        Score = score;
        LevelStaticData = levelStaticData;
    }
}
