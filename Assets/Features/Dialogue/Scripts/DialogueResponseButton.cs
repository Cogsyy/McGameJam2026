using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueResponseButton : MonoBehaviour
{
	[SerializeField] private TMP_Text _buttonText;
	[SerializeField] private Button _button;

	public void Setup(DialogueManager manager, DialogueChoice choice)
	{
		_buttonText.text = choice.ChoiceText;
		_button.onClick.RemoveAllListeners();
		_button.onClick.AddListener(() => manager.PickChoice(choice));
	}
}
