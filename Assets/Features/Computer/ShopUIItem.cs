using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIItem : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Image _iconImage;
	[SerializeField] private TMP_Text _priceText;
	[SerializeField] private Button _buyButton;

	private ClothingItem _currentItem;
	private ShopPage _owner;

	public void Setup(ShopPage owner, ClothingItem item)
	{
		_owner = owner;
		_currentItem = item;

		if (_iconImage != null) _iconImage.sprite = item.itemIcon;
		if (_priceText != null) _priceText.text = "$" + item.Price.ToString();

		_buyButton.onClick.RemoveAllListeners();
		_buyButton.onClick.AddListener(OnBuyClicked);
	}

	private void OnBuyClicked()
	{
		if (_owner != null && _currentItem != null)
		{
			if (_owner.TryPurchaseItem(_currentItem))
            {
                gameObject.SetActive(false);
            }
		}
	}
}
