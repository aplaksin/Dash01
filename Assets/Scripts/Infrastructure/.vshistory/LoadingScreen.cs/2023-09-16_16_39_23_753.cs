using System.Collections;
using UnityEngine;


public class LoadingScreen : MonoBehaviour
{
    public CanvasGroup LoadingScreenCanvasGroup;

/*    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }*/

    public void Show()
    {
        gameObject.SetActive(true);
        LoadingScreenCanvasGroup.alpha = 1;
    }

    public void Hide()
    {
        StartCoroutine(DoFadeIn());
    }


    private IEnumerator DoFadeIn()
    {
        while (LoadingScreenCanvasGroup.alpha > 0)
        {
            LoadingScreenCanvasGroup.alpha -= 0.03f;
            yield return new WaitForSeconds(0.03f);
        }

        gameObject.SetActive(false);
    }
}
