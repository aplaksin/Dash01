using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour
{
    [SerializeField]
    private AnimationType _animationType;

    [SerializeField]
    private Image _bgFirst;

    [SerializeField]
    private Image _bgSecond;



    [SerializeField]
    private List<Sprite> _bgList;

    [SerializeField]
    private float _animationDelay = 0.05f;


    [SerializeField]
    private float fadeTime = 1f;

    [SerializeField]
    private float fadeTimeDelta = 0.1f;

    private float _currentFadeTimer = 0;
    private int _currentBgIndex = 0;
    private float _currentTimer = 0;
    private int _animationDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        _bgFirst.sprite = _bgList[_currentBgIndex];
        _currentBgIndex++;
    }

    // Update is called once per frame
    private void Update()
    {
        ReversedAnimation(Time.deltaTime);
    }

    private void ReversedAnimation(float deltaTime)
    {
        _currentTimer += deltaTime;

        if(_currentTimer >= _animationDelay)
        {
            _currentBgIndex += _animationDirection;
            _bgFirst.sprite = _bgList[_currentBgIndex];
            _currentTimer = 0;
        }

        if(_animationDirection > 0)
        {
            if(_currentBgIndex >= _bgList.Count -1)
            {
                _animationDirection = -1;
            }
        }
        else
        {
            if (_currentBgIndex == 0)
            {
                _animationDirection = 1;
            }
        }
    }

    private void CircledFade()
    {
        if (_currentFadeTimer < fadeTime)
        {
            _currentFadeTimer += Time.deltaTime;
        }
        else
        {
            _currentFadeTimer = 0;

            if (_currentBgIndex >= _bgList.Count)
            {
                _currentBgIndex = 0;
            }

            _bgFirst.sprite = _bgSecond.sprite;

            _bgSecond.CrossFadeAlpha(0, 0.001f, false);
            _bgSecond.sprite = _bgList[_currentBgIndex];
            _bgSecond.CrossFadeAlpha(1, fadeTime, false);

            _currentBgIndex++;
        }
    }

}
