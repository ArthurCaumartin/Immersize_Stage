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


    [Header("En speed : ")]
    [SerializeField] private int _currency;

    private ShopUI _shopUI;

    private void Start()
    {
        _shopUI = GetComponent<ShopUI>();

        _buttonRefresh.onClick.AddListener(RefreshDisplay);
        _buttonClose.onClick.AddListener(() => EnableShop(false));

        EnableShop(false);
    }

    public void EnableShop(bool value)
    {
        if (_displayList.Count == 0)
        {
            _shopUI.InitializeDislpay(_itemToSellList, _displayList);
        }

        _shopUI.EnableUI(value);
    }

    public void RefreshDisplay()
    {
        _shopUI.ClearDisplay(_displayList);
        _shopUI.InitializeDislpay(_itemToSellList, _displayList);
    }

    public void Interact()
    {
        EnableShop(true);
    }
}
