using Entity.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textName;
    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField] private TextMeshProUGUI _textDescription;
    [SerializeField] private TextMeshProUGUI _textLevelRequier;
    [SerializeField] private TextMeshProUGUI _textStats;
    [Space]
    [SerializeField] private Image _imageIcon;

    public int Price;

    public ShopItemDisplay Initialize(ScriptableItemData data)
    {
        Price = Random.Range(10, 50);
        _textPrice.text = Price.ToString();

        if (data is ScriptAbleWeaponData)
        {
            ScriptAbleWeaponData weaponData = data as ScriptAbleWeaponData;

            _imageIcon.sprite = weaponData.icon;

            _textName.text = weaponData.WeaponName;
            _textLevelRequier.text = $"Lvl : {weaponData.LevelRequirement}";
            if (_textDescription) _textDescription.text = weaponData.Description;

            _textStats.text =
            $"Damage : {weaponData.Damage}\n" +
            $"Attack speed : {weaponData.FireRate}\n" +
            $"Range : {weaponData.Range}";
        }

        return this;
    }
}