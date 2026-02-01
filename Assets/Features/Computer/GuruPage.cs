using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GuruPage : MonoBehaviour
{
	[Header("Skills")]
	[SerializeField] private List<GuruUIItem> _skillSlots = new List<GuruUIItem>();

	private List<ConversationSkill> _currentlyDisplayedSkills = new List<ConversationSkill>();

	private void Start()
	{
		Restock();
	}

	public void Restock()
	{
		// Filter out skills already purchased
		List<ConversationSkill> availableSkills = GameStarter.Instance.ConversationSkills.ToList()
			.Where(skill => !Player.Instance.UnlockedSkills.Contains(skill.ID))
			.ToList();

		// Shuffle and pick top slots count
		availableSkills = availableSkills.OrderBy(x => Random.value).ToList();
		
		_currentlyDisplayedSkills.Clear();
		int countToDisplay = Mathf.Min(availableSkills.Count, _skillSlots.Count);
		
		for (int i = 0; i < countToDisplay; i++)
		{
			_currentlyDisplayedSkills.Add(availableSkills[i]);
		}

		// Clear slots first
		foreach (var slot in _skillSlots)
		{
			slot.gameObject.SetActive(false);
		}

		// Setup available slots
		for (int i = 0; i < _currentlyDisplayedSkills.Count; i++)
		{
			if (i < _skillSlots.Count)
			{
				_skillSlots[i].gameObject.SetActive(true);
				_skillSlots[i].Setup(this, _currentlyDisplayedSkills[i]);
			}
		}
	}

	public bool TryPurchaseSkill(ConversationSkill skill)
	{
		if (Player.Instance.TryRemoveMoney(skill.Price))
		{
			Debug.Log($"Purchased skill {skill.SkillName} for ${skill.Price}");
			Player.Instance.UnlockSkill(skill.ID);
			
			// Remove from currently displayed
			_currentlyDisplayedSkills.Remove(skill);
			return true;
		}
		else
		{
			// Insufficient funds handled by Player.TryRemoveMoney
			return false;
		}
	}
}
