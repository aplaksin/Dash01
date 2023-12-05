using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReversedAnimation : MonoBehaviour
{
    [SerializeField]
    private Image _bgFirst;

    [SerializeField]
    private List<Sprite> _bgList;

    [SerializeField]
    private float _animationDelay = 0.05f;


    private int _currentBgIndex = 0;
    private float _currentTimer = 0;
    private int _animationDirection = 1;

    private void Update()
    {
        Animate(Time.deltaTime);
    }

    private void Animate(float deltaTime)
    {
        _currentTimer += deltaTime;

        if (_currentTimer >= _animationDelay)
        {
            _currentBgIndex += _animationDirection;
            _bgFirst.sprite = _bgList[_currentBgIndex];
            _currentTimer = 0;
        }

        if (_animationDirection > 0)
        {
            if (_currentBgIndex >= _bgList.Count - 1)
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
}
