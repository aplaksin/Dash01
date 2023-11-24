using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBorder : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup DamageBorderCanvasGroup;

    public void Show()
    {
        gameObject.SetActive(true);
        DamageBorderCanvasGroup.alpha = 1;
    }

    public void Hide()
    {
        StartCoroutine(DoFadeIn());
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
