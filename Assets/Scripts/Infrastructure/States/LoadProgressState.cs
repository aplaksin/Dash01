using System;



public class LoadProgressState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadProgress;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadProgress)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadProgress = saveLoadProgress;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();

        //_gameStateMachine.Enter<LoadLevelState, string>(_progressService.PlayerProgress.WorldData.PositionOnLevel.Level);
        _gameStateMachine.Enter<LoadLevelState, string>("Level1");
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew()
    {
        _progressService.PlayerProgress =
          _saveLoadProgress.LoadProgress()
          ?? NewProgress();
    }

    private PlayerProgress NewProgress()
    {
        var progress = new PlayerProgress(initialLevel: "Main");

/*        progress.HeroState.MaxHP = 50;
        progress.HeroStats.Damage = 1;
        progress.HeroStats.DamageRadius = 0.5f;
        progress.HeroState.ResetHP();*/

        return progress;
    }
}
