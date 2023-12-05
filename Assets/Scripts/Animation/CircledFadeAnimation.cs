using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircledFadeAnimation : MonoBehaviour
{
    [SerializeField]
    private Image _bgFirst;

    [SerializeField]
    private Image _bgSecond;

    [SerializeField]
    private List<Sprite> _bgList;

    [SerializeField]
    private float fadeTime = 1f;

    [SerializeField]
    private float fadeTimeDelta = 0.1f;

    private float _currentFadeTimer = 0;
    private int _currentBgIndex = 0;


    private void Start()
    {
        _bgFirst.sprite = _bgList[_currentBgIndex];
        _currentBgIndex++;
    }


    void Update()
    {
        Animate();
    }

    private void Animate()
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
