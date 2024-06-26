

using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameOverMenu : WindowBase
{
    [SerializeField]
    private TextMeshProUGUI _scoreTextField;
    private string _scoreText;
    private int _score;
    private GameStateMachine _gameStateMachine;
    private int _scoreMultiplier = 10;//TODO переделать
    public void Construct(GameStateMachine gameStateMachine, int score)
    {
        _gameStateMachine = gameStateMachine;
        _scoreText = _scoreTextField.text;
        _score = score;

        UpdateText();
        PauseGame();
    }


    public void OnExitBtnClick()
    {
        Application.Quit();
    }

    public void OnRestartMenuBtnClick()
    {
        _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name);
    }

/*    public void OnPauseBtnClick()
    {
        //Debug.Log("OnPauseBtnClick");
        //UIEventManager.CallOnClickPauseBtnEvent();
    }*/

    protected override void Cleanup()
    {
        ContinueGame();
    }

    private void UpdateText()
    {
        _scoreTextField.text = $"{_scoreText} {_score * _scoreMultiplier}";
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1f;
    }
}
