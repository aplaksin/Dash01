using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour
{
    [SerializeField]
    private AnimationType animationType;
    [SerializeField]
    private Image _bg;

    [SerializeField]
    private List<Sprite> _bgList;

    [SerializeField]
    private float _animationDelay = 0.05f;


    private int _currentBgIndex = 0;
    private float _currentTimer = 0;
    private int _animationDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        _bg.sprite = _bgList[_currentBgIndex];
        _currentBgIndex++;
    }

    // Update is called once per frame
    private void Update()
    {
        AnimateBg(Time.deltaTime);
    }

    private void AnimateBg(float deltaTime)
    {
        _currentTimer += deltaTime;

        if(_currentTimer >= _animationDelay)
        {
            _currentBgIndex += _animationDirection;
            _bg.sprite = _bgList[_currentBgIndex];
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

}
