
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreTextField;
    private string _scoreText;


    private int _currentScore = 0;

    private void Start()
    {
        _scoreText = _scoreTextField.text;
        UpdateText();
    }

    private void UpdateText()
    {
        _scoreTextField.text = $"{_scoreText} {_currentScore}";
    }

    private void OnEnable()
    {

        EventManager.OnEnemyDeath += AddScore;
    }
    private void OnDisable()
    {
        EventManager.OnEnemyDeath -= AddScore;
    }

    private void AddScore()
    {
        _currentScore++;
        UpdateText();
    }
}
