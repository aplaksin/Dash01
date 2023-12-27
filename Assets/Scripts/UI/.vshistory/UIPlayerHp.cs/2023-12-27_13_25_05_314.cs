using TMPro;
using UnityEngine;

public class UIPlayerHp : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _hpTextField;
    private string _hpText;
    private int _currentHp;

    public void Construct(int hp)
    {
        _currentHp = hp;
        _hpText = _hpTextField.text;
        UpdateText();
    }

    private void OnEnable()
    {
        EventManager.OnHpChanged += ChangeHpText;
    }
    private void OnDisable()
    {
        EventManager.OnHpChanged -= ChangeHpText;
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
