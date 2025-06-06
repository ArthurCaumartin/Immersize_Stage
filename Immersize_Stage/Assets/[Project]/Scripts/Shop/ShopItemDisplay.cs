using Entity.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour
{
    [SerializeField] private Button _buttonBuy;
    [SerializeField] private TextMeshProUGUI _textName;
    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField] private TextMeshProUGUI _textDescription;
    [SerializeField] private TextMeshProUGUI _textLevelRequier;
    [SerializeField] private TextMeshProUGUI _textStats;
    [Space]
    [SerializeField] private Image _imageIcon;
    private ScriptableItemData _data;

    public ScriptableItemData ItemData { get => _data; }

    public ShopItemDisplay Initialize(ScriptableItemData data, Shop shop)
    {
        _data = data;
        _textPrice.text = data.Price.ToString();

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

        _buttonBuy.onClick.AddListener(() =>
        {
            if (shop.TryBuy(data.Price))
                shop.RemoveDisplay(this);
        });
        return this;
    }
}