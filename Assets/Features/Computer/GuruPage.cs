using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GuruPage : MonoBehaviour, IShop
{
	[Header("Skills")]
	[SerializeField] private List<GuruUIItem> _skillSlots = new List<GuruUIItem>();
    [SerializeField] private SkillsDisplay _skillsDisplay;
	[Header("SFX")]
	[SerializeField] private AudioClip _shuffleSFX;
	[SerializeField] private AudioClip _dealSFX;
	[SerializeField] private AudioClip _purchaseSFX;

	private List<ConversationSkill> _currentlyDisplayedSkills = new List<ConversationSkill>();

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
			if (_purchaseSFX != null && AudioManager.Instance != null)
			{
				AudioManager.Instance.PlaySFX(_purchaseSFX);
			}
			Debug.Log($"Purchased skill {skill.SkillName} for ${skill.Price}");
			Player.Instance.UnlockSkill(skill.ID);
            _skillsDisplay.DisplaySkills();
			
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
