using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Sprite _swipeSprite;
    [SerializeField] private Sprite _wasdSprite;
    [SerializeField] private float _fadeStep = 0.03f;
    [SerializeField] private float _fadeStepDelay = 0.03f;

    private void Awake()
    {
        SelectImageByDevice();
    }

    private void Start()
    {
        Hide();
    }

    private void OnEnable()
    {
        Hide();
    }


    /*    private void Update()
        {

        }*/

    private void SelectImageByDevice()
    {
        if (Application.isEditor || SystemInfo.deviceType == DeviceType.Desktop)
        {
            _image.sprite = _wasdSprite;
        }
        else
        {
            _image.sprite = _swipeSprite;
        }          
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
            yield return new WaitForSeconds(_fadeStepDelay);
        }

        gameObject.SetActive(false);
    }

}
