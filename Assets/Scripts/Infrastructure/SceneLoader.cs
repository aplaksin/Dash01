using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public void LoadScene(string sceneName, Action onLoadedScene = null)
    {
        _coroutineRunner.StartCoroutine(Load(sceneName, onLoadedScene));
    }

    public IEnumerator Load(string sceneName, Action onLoadedScene = null)
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            onLoadedScene?.Invoke();
            yield break;
        }


        AsyncOperation waitNextSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!waitNextSceneOperation.isDone)
            yield return null;

        onLoadedScene?.Invoke();
    }
}
