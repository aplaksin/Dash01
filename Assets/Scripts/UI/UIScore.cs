
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreTextField;
    private string _scoreText;


    private int _currentScore = 0;
    private int _scoreMultiplier = 25;//TODO переделать и почистить вообще
    private void Start()
    {
        _scoreText = _scoreTextField.text;
        _scoreMultiplier = Game.GameContext.ScoreUIMultiplier;
        UpdateText();
    }


    private void UpdateText()
    {
        _scoreTextField.text = $"{_scoreText} {_currentScore * _scoreMultiplier}";
    }

    private void OnEnable()
    {
        EventManager.OnScoreChanged += AddScore;
        EventManager.OnGameOver += HideScore;
    }
    private void OnDisable()
    {
        EventManager.OnScoreChanged -= AddScore;
        EventManager.OnGameOver -= HideScore;
    }

    private void AddScore(int score)
    {
        _currentScore = score;
        UpdateText();
    }

    private void HideScore()
    {
        _scoreTextField.transform.gameObject.SetActive(false);
    }
}
