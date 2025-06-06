using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IInteractible
{
    [SerializeField] private ShopItemDisplay _prefabDisplay;
    [SerializeField] private GridLayoutGroup _displayParent;
    [Space]
    [SerializeField] private Button _buttonRefresh;
    [SerializeField] private Button _buttonClose;
    [SerializeField] private List<ScriptableItemData> _itemToSellList = new List<ScriptableItemData>();
    private List<ShopItemDisplay> _itemDisplayList = new List<ShopItemDisplay>();


    [Header("En speed : ")]
    [SerializeField] private int _currency;

    private void Start()
    {
        _buttonRefresh.onClick.AddListener(RefreshDisplay);
        _buttonClose.onClick.AddListener(() => EnableShop(false));
        InitializeDislpay(_itemToSellList);
    }

    public void EnableShop(bool value)
    {
        gameObject.SetActive(false);
    }

    public void RefreshDisplay()
    {
        ClearCurrentDisplay();
        InitializeDislpay(_itemToSellList);
    }

    private void InitializeDislpay(List<ScriptableItemData> itemList)
    {
        if (_itemToSellList.Count == 0) return;
        if (_itemDisplayList.Count > 0)
            ClearCurrentDisplay();

        for (int i = 0; i < itemList.Count; i++)
        {
            ShopItemDisplay newDis = Instantiate(_prefabDisplay, _displayParent.transform);
            _itemDisplayList.Add(newDis.Initialize(itemList[i]));
        }
        RectTransform parentRect = (RectTransform)_displayParent.transform;
        // Rect newRect = parentRect.rect;
        // newRect.yMax = (_displayParent.cellGap.y + _displayParent.cellSize.y) * itemList.Count;
        parentRect.rect.Set(0, 0, 0, (_displayParent.spacing.y + _displayParent.cellSize.y) * itemList.Count);
    }

    private void ClearCurrentDisplay()
    {
        print("clear");
        foreach (var item in _itemDisplayList)
            Destroy(item.gameObject);
        _itemDisplayList.Clear();
    }

    public void Interact()
    {

    }
}
