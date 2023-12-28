using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private CanvasGroup _canvasGroup;
    private Sprite _swipeSprite;
    private Sprite _wasdSprite;


    private void Start()
    {
        
    }

    
    private void Update()
    {
        
    }

    public void Hide()
    {
        StartCoroutine(DoFadeIn());
    }

    private IEnumerator DoFadeIn()
    {
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= 0.03f;
            yield return new WaitForSeconds(0.03f);
        }

        gameObject.SetActive(false);
    }

}
