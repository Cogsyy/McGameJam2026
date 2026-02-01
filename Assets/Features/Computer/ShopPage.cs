using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ShopItem
{
	public string ID;
	public string Name;
	public int Price;
	public Sprite Icon;
}

public class ShopPage : MonoBehaviour
{
	[Header("Items")]
	[SerializeField] private List<ShopItem> _allPossibleItems = new List<ShopItem>();
	[SerializeField] private List<ShopUIItem> _itemSlots = new List<ShopUIItem>();

	private List<string> _purchasedItemIDs = new List<string>();
	private List<ShopItem> _currentlyDisplayedItems = new List<ShopItem>();

	private void Start()
	{
		Restock();
	}

	public void Restock()
	{
		// Filter out items already purchased
		List<ShopItem> availableItems = _allPossibleItems
			.Where(item => !_purchasedItemIDs.Contains(item.ID))
			.ToList();

		// Shuffle and pick top 3
		availableItems = availableItems.OrderBy(x => Random.value).ToList();
		
		_currentlyDisplayedItems.Clear();
		int countToDisplay = Mathf.Min(availableItems.Count, _itemSlots.Count);
		
		for (int i = 0; i < countToDisplay; i++)
		{
			_currentlyDisplayedItems.Add(availableItems[i]);
		}

		// Clear slots first
		foreach (var slot in _itemSlots)
		{
			slot.gameObject.SetActive(false);
		}

		// Setup available slots
		for (int i = 0; i < _currentlyDisplayedItems.Count; i++)
		{
			if (i < _itemSlots.Count)
			{
				_itemSlots[i].gameObject.SetActive(true);
				_itemSlots[i].Setup(this, _currentlyDisplayedItems[i]);
			}
		}
	}

	public bool TryPurchaseItem(ShopItem item)
	{
		if (Player.Instance.TryRemoveMoney(item.Price))
		{
			Debug.Log($"Purchased {item.Name} for ${item.Price}");
			_purchasedItemIDs.Add(item.ID);
			
			// Remove from currently displayed and update UI
			_currentlyDisplayedItems.Remove(item);
            return true;
		}
		else
		{
			Debug.LogWarning("Insufficient funds!");
            return false;
		}
	}
}
