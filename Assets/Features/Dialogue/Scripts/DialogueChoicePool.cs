using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueChoicePool", menuName = "Dialogue/Choice Pool")]
public class DialogueChoicePool : ScriptableObject
{
	[SerializeField] private List<DialogueChoice> _choices = new List<DialogueChoice>();

	public IReadOnlyList<DialogueChoice> Choices => _choices;
}
