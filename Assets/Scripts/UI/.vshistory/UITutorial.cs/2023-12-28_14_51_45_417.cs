using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _swipeSprite;
    [SerializeField] private Image _wasdSprite;
    [SerializeField] private float _fadeStep = 0.03f;


    private void Start()
    {
        
    }

    
    private void Update()
    {
        
    }

    private void SelectImageByDevice()
    {
        if (Application.isEditor || SystemInfo.deviceType == DeviceType.Desktop)
            _image = _wasdSprite;
        else
            _image = _swipeSprite;
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
