using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textName;
    [SerializeField] private TextMeshProUGUI _textDescription;
    [SerializeField] private Image _imageHealthBar;

    public void SetName(string value)
    {
        if (!_textName) return;
        _textName.text = value;
    }

    public void SetNameHeight(float value)
    {
        _textName.transform.localPosition = new Vector3(_textName.transform.localPosition.x
                                                        , value
                                                        , _textName.transform.localPosition.z);
    }

    public void SetDescription(string value)
    {
        if (!_textDescription) return;
        _textDescription.text = value;
    }

    public void SetHealthBarValue(float healthRatio)
    {
        _imageHealthBar.fillAmount = healthRatio;
    }
}


