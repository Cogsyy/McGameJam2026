using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public interface IShop
{
	void Restock();
}

public class ShopPage : MonoBehaviour, IShop
{
	[Header("Items")]
	[SerializeField] private List<ShopUIItem> _itemSlots = new List<ShopUIItem>();
	[Header("SFX")]
	[SerializeField] private AudioClip _shuffleSFX;
	[SerializeField] private AudioClip _dealSFX;

	private List<ClothingItem> _allPossibleItems = new List<ClothingItem>();
	private List<string> _purchasedItemIDs = new List<string>();
	private List<ClothingItem> _currentlyDisplayedItems = new List<ClothingItem>();

	private void Start()
	{
		Restock();
	}

	private void OnEnable()
	{
		if (_shuffleSFX != null && AudioManager.Instance != null)
		{
			AudioManager.Instance.PlaySFX(_shuffleSFX);
		}
	}

	public void Restock()
	{
		// Filter out items already purchased
		List<ClothingItem> availableItems = GameStarter.Instance.ClothingItems.ToList()
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

	public bool TryPurchaseItem(ClothingItem item)
	{
		if (Player.Instance.TryRemoveMoney(item.Price))
		{
			Debug.Log($"Purchased {item.itemName} for ${item.Price}");
			_purchasedItemIDs.Add(item.ID);
			
			// Remove from currently displayed and update UI
			_currentlyDisplayedItems.Remove(item);

			item.isUnlocked = true;
            return true;
		}
		else
		{
			Debug.LogWarning("Insufficient funds!");
            return false;
		}
	}
}
