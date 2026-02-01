using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueResponseButton : MonoBehaviour
{
	[SerializeField] private TMP_Text _buttonText;
	[SerializeField] private Button _button;

	public void Setup(DialogueManager manager, DialogueChoice choice, bool showCorrectChoices)
	{
		_buttonText.text = choice.ChoiceText;
		_button.onClick.RemoveAllListeners();
		_button.onClick.AddListener(() => manager.PickChoice(choice));

		if(showCorrectChoices)
		{
			bool isThisChoiceCorrect = choice.Correctness == ChoiceCorrectness.Correct || choice.Correctness == ChoiceCorrectness.Neutral;

			if(!isThisChoiceCorrect)
			{
				_button.interactable = false;
			}
		}
	}
}
