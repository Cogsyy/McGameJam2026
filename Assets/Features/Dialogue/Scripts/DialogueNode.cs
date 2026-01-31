using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
	[SerializeField] private string _characterName;
	[SerializeField, TextArea(3, 10)] private string _dialogueText;
	[SerializeField] private List<DialogueChoice> _choices = new List<DialogueChoice>();

	public string CharacterName => _characterName;
	public string DialogueText => _dialogueText;
	public IReadOnlyList<DialogueChoice> Choices => _choices;
}
