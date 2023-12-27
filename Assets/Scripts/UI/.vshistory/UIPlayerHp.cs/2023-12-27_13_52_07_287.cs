using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPlayerHp : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _hpTextField;

    [SerializeField]
    private GameObject _hpGroupLayout;

    [SerializeField]
    private GameObject _hpImage;

    private string _hpText;
    private int _currentHp;
    private List<GameObject> _hpImagesList = new List<GameObject>();


    public void Construct(int hp)
    {
        _currentHp = hp;
        //_hpText = _hpTextField.text;
        //UpdateText();

        for(int i = 0; i < hp; i++)
        {
            GameObject hpImage = Object.Instantiate(_hpImage, _hpGroupLayout.transform, false);
            _hpImagesList.Add(hpImage);
        }

    }

    private void OnEnable()
    {
        //EventManager.OnHpChanged += ChangeHpText;
        EventManager.OnHpChanged += ChangeHpIcons;
    }
    private void OnDisable()
    {
        //EventManager.OnHpChanged -= ChangeHpText;
        EventManager.OnHpChanged -= ChangeHpIcons;
    }


    private void ChangeHpIcons(int hp)
    {
        for (int i = 0; i < _hpImagesList.Count; i++)
        {
            if(i >= hp)
            {
                _hpImagesList[i].SetActive(false);
            }
        }
    }

    private void ChangeHpText(int hp)
    {
        _currentHp = hp;
        UpdateText();
    }

    private void UpdateText()
    {
        _hpTextField.text = $"{_hpText} {_currentHp}";
    }
}
