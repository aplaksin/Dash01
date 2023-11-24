using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBorder : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup DamageBorderCanvasGroup;

    private void Show()
    {
        gameObject.SetActive(true);
        DamageBorderCanvasGroup.alpha = 1;
    }

    private void Hide()
    {
        StartCoroutine(DoFadeIn());
    }
    public void ShowHide()
    {
        Show();
        Hide();
    }

    private IEnumerator DoFadeIn()
    {
        while (DamageBorderCanvasGroup.alpha > 0)
        {
            DamageBorderCanvasGroup.alpha -= 0.03f;
            yield return new WaitForSeconds(0.03f);
        }

        gameObject.SetActive(false);
    }

}
