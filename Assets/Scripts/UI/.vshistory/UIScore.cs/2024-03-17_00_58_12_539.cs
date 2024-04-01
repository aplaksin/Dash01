
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreTextField;
    private string _scoreText;


    private int _currentScore = 0;
    private int _scoreMultiplier = 25;
    private void Start()
    {
        _scoreText = _scoreTextField.text;
        UpdateText();
    }

    private void UpdateText()
    {
        _scoreTextField.text = $"{_scoreText} {_currentScore * _scoreMultiplier}";
    }

    private void OnEnable()
    {

        EventManager.OnScoreChanged += AddScore;
    }
    private void OnDisable()
    {
        EventManager.OnScoreChanged -= AddScore;
    }

    private void AddScore(int score)
    {
        _currentScore = score;
        UpdateText();
    }
}
