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
        UpdatText();
    }

    private void Start()
    {

    }


    private void OnEnable()
    {
        EventManager.OnDamage += ChangeHpText;
    }
    private void OnDisable()
    {
        EventManager.OnDamage -= ChangeHpText;
    }


    private void ChangeHpText(int hp)
    {
        _currentHp -= hp;
        UpdatText();
    }

    private void UpdatText()
    {
        _hpTextField.text = $"{_hpText} {_currentHp}";
    }
}
