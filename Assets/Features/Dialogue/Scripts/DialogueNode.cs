using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
	[SerializeField] private string _characterName;
	[SerializeField, TextArea(3, 10)] private string _dialogueText;
	[SerializeField] private List<DialogueChoice> _fixedChoices = new List<DialogueChoice>();

	[Header("Themed Pools")]
	[SerializeField] private List<DialogueChoicePool> _choicePools = new List<DialogueChoicePool>();
	[SerializeField] private int _randomChoicesLimit = 0;

	[Header("Direct Transition")]
	[SerializeField] private DialogueNode _nextNode;
	[SerializeField] private List<DialogueNode> _randomNextNodes = new List<DialogueNode>();

	public string CharacterName => _characterName;
	public string DialogueText => _dialogueText;
	public IReadOnlyList<DialogueChoice> FixedChoices => _fixedChoices;
	public IReadOnlyList<DialogueChoicePool> ChoicePools => _choicePools;
	public int RandomChoicesLimit => _randomChoicesLimit;

	public DialogueNode GetNextNode()
	{
		if (_randomNextNodes != null && _randomNextNodes.Count > 0)
		{
			int randomIndex = UnityEngine.Random.Range(0, _randomNextNodes.Count);
			return _randomNextNodes[randomIndex];
		}

		return _nextNode;
	}
}
