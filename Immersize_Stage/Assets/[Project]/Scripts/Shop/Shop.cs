using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShopUI))]
public class Shop : MonoBehaviour, IInteractible
{
    [SerializeField] private Button _buttonRefresh;
    [SerializeField] private Button _buttonClose;
    [SerializeField] private List<ScriptableItemData> _itemToSellList = new List<ScriptableItemData>();
    private List<ShopItemDisplay> _displayList = new List<ShopItemDisplay>();


    [Header("En speed / a setup autre ailleur : ")]
    [SerializeField] private int _currency;

    private ShopUI _shopUI;

    private void Start()
    {
        _shopUI = GetComponent<ShopUI>();

        _buttonRefresh.onClick.AddListener(() => RefreshDisplay(50));
        _buttonClose.onClick.AddListener(() => EnableShop(false));

        EnableShop(false);
    }

    public bool TryBuy(int price)
    {
        if (_currency >= price && _currency - price >= 0)
        {
            _currency -= price;

            return true;
        }
        return false;
    }

    public void RemoveDisplay(ShopItemDisplay display)
    {
        if (_displayList.Contains(display))
            _displayList.Remove(display);
        Destroy(display.gameObject);
    }

    public void EnableShop(bool value)
    {
        if (_displayList.Count == 0)
        {
            _shopUI.InitializeDislpay(_itemToSellList, _displayList, this);
        }

        _shopUI.EnableUI(value);
    }

    public void RefreshDisplay(int price)
    {
        if (TryBuy(price))
        {
            _shopUI.ClearDisplay(_displayList);
            _shopUI.InitializeDislpay(_itemToSellList, _displayList, this);
        }
    }

    public void Interact()
    {
        EnableShop(true);
    }
}
