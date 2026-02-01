using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuruUIItem : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TMP_Text _skillNameText;
	[SerializeField] private TMP_Text _priceText;
	[SerializeField] private Button _buyButton;

	private ConversationSkill _currentSkill;
	private GuruPage _owner;

	public void Setup(GuruPage owner, ConversationSkill skill)
	{
		_owner = owner;
		_currentSkill = skill;
		if (_skillNameText != null) _skillNameText.text = skill.SkillName;
		if (_priceText != null) _priceText.text = "$" + skill.Price.ToString();

		_buyButton.onClick.RemoveAllListeners();
		_buyButton.onClick.AddListener(OnBuyClicked);
	}

	private void OnBuyClicked()
	{
		if (_owner != null && _currentSkill != null)
		{
			if (_owner.TryPurchaseSkill(_currentSkill))
			{
				gameObject.SetActive(false);
			}
		}
	}
}
