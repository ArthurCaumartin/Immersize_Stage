using TMPro;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textName;
    [SerializeField] private TextMeshProUGUI _textDescription;

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
}


