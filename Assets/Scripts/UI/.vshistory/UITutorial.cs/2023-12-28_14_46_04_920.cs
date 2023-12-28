using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Sprite _swipeSprite;
    [SerializeField] private Sprite _wasdSprite;
    [SerializeField] private float _fadeStep = 0.03f;


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
            _canvasGroup.alpha -= _fadeStep;
            yield return new WaitForSeconds(_fadeStep);
        }

        gameObject.SetActive(false);
    }

}
