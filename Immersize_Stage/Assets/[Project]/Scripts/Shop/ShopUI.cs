using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Canvas _shopCanvas;
    [SerializeField] private ShopItemDisplay _prefabDisplay;
    [SerializeField] private GridLayoutGroup _displayParent;
    [SerializeField] private ScrollRect _scrollRect;


    public void EnableUI(bool value)
    {
        _shopCanvas.enabled = value;
        ResetScroll();
    }

    public void InitializeDislpay(List<ScriptableItemData> itemList, List<ShopItemDisplay> displayList, Shop shop)
    {
        if (itemList.Count == 0) return;
        if (displayList.Count > 0)
            ClearDisplay(displayList);

        for (int i = 0; i < itemList.Count; i++)
        {
            ShopItemDisplay newDis = Instantiate(_prefabDisplay, _displayParent.transform);
            displayList.Add(newDis.Initialize(itemList[i], shop));
        }
        RectTransform parentRect = (RectTransform)_displayParent.transform;
        parentRect.rect.Set(0, 0, 0, (_displayParent.spacing.y + _displayParent.cellSize.y) * itemList.Count);
        ResetScroll();
    }

    public void ClearDisplay(List<ShopItemDisplay> displayList)
    {
        // print("clear");
        foreach (var item in displayList)
            Destroy(item.gameObject);
        displayList.Clear();
    }

    private void ResetScroll()
    {
        _scrollRect.verticalNormalizedPosition = 1;
    }
}
