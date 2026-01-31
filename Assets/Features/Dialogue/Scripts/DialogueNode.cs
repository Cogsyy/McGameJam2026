using System.Collections.Generic;
using UnityEngine;

public enum DialogueType
{ 
	Generic,
	Greetings,
	Conversation,
	ConversationRejectionSoft,
	ConversationRejectionHarsh,
	Question,
	RejectionSoft,
	RejectionHarsh,
	Acceptance
}


[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
	[SerializeField] private string _characterName;
	[SerializeField, TextArea(3, 10)] private string _dialogueText;
	[SerializeField] private List<DialogueChoice> _fixedChoices = new List<DialogueChoice>();

	[Header("MetaData")]
	[SerializeField] private JobType _jobType = JobType.None;
	[SerializeField] private DialogueType _dialogueType = DialogueType.Generic;
	[SerializeField, Tooltip("Only relevant if its a question")] private int _questionDifficulty = 0;
	[SerializeField, Tooltip("Used to tag specific dialogue")] private string _specialTag = string.Empty;
	[SerializeField, Tooltip("If this dialogue only comes up after another one")] private bool _isDialogueFollowUp = false;

	[Header("Themed Pools")]
	[SerializeField] private List<DialogueChoicePool> _choicePools = new List<DialogueChoicePool>();
	[SerializeField] private int _randomChoicesLimit = 0;

	[Header("Direct Transition")]
	[SerializeField] private DialogueNode _nextNode;
	[SerializeField] private List<DialogueNode> _randomNextNodes = new List<DialogueNode>();

	public string CharacterName => _characterName;
	public string DialogueText => _dialogueText;
	public JobType JobType => _jobType;
	public DialogueType DialogueType => _dialogueType;
	public int QuestionDifficulty => _questionDifficulty;
	public string SpecialTag => _specialTag;
	public bool IsDialogueFollowUp => _isDialogueFollowUp;
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
