using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TMP_Text _mainDialogue;
	[SerializeField] private DialogueResponseButton _responseButton;
	[SerializeField] private Transform _responsesParent;
	[SerializeField] private GameObject _dialogueCanvasObject;

	[SerializeField] private int _nbGreetingsDialogue = 1;
	[SerializeField] private int _nbConversationDialogue = 3;
	[SerializeField] private int _nbConversationsIncorrectToFail = 2;
	[SerializeField] private int _nbQuestionsDialogue = 4;
	[SerializeField] private int _nbQuestionsIncorrectToFail = 1;

	public event Action<DialogueNode> OnDialogueStarted;
	public event Action<DialogueNode> OnNodeChanged;
	public event Action OnDialogueEnded;

	private JobPostingData _jobPostingData;
	private int _currentGreetingCount;
	private int _currentConversationCount;
	private int _currentQuestionCount;

	private int _incorrectConversationsCount;
	private int _incorrectQuestionsCount;

	private DialogueType _currentDialoguePhase;

	private DialogueNode _currentNode;
	private List<DialogueNode> _currentNodesOfDialoguePhase;
	private int _currentNodeIndex;


	private bool _isDialogueActive;
	private List<DialogueResponseButton> _responseButtons = new List<DialogueResponseButton>();

	public bool IsDialogueActive => _isDialogueActive;

	public void SetJobInterviewPostingData(JobPostingData data)
	{
		// Example method to demonstrate interaction with JobPostingData
		if (data != null && _mainDialogue != null)
		{
			_jobPostingData = data;
		}
	}

	public void ResetState()
	{
		_currentNode = null;
		_currentNodesOfDialoguePhase = new List<DialogueNode>();
		_currentNodeIndex = 0;

		_isDialogueActive = false;
		_currentDialoguePhase = DialogueType.Greetings;

		_currentConversationCount = 0;
		_currentGreetingCount = 0;
		_currentQuestionCount = 0;
		_incorrectConversationsCount = 0;
		_incorrectQuestionsCount = 0;
	}

	public void StartDialogue()
	{
		ResetState();

		_dialogueCanvasObject.SetActive(true);
		_isDialogueActive = true;
		_currentDialoguePhase = DialogueType.Greetings;
		SetNextDialogueNode();
		OnDialogueStarted?.Invoke(_currentNode);
	}

	public void PickChoice(DialogueChoice choice)
	{
		ProcessChoice(choice);
		SetNextDialogueNode();
	}

	public void EndDialogue()
	{
		_isDialogueActive = false;
		_currentNode = null;
		ClearButtons();
		OnDialogueEnded?.Invoke();

		_dialogueCanvasObject.SetActive(false);
		FindAnyObjectByType<AreaMovement>().GoHome();

	}

	private void ProcessChoice(DialogueChoice choice)
	{
		bool isIncorrect = choice.Correctness == ChoiceCorrectness.Incorrect ||
						   choice.Correctness == ChoiceCorrectness.VeryIncorrect;

		if(isIncorrect)
		{
			if(_currentDialoguePhase == DialogueType.Conversation)
			{
				_incorrectConversationsCount++;
			}
			else if(_currentDialoguePhase == DialogueType.Question)
			{
				_incorrectQuestionsCount++;
			}
		}
	}

	private void UpdateDialoguePhase()
	{
		if(_currentDialoguePhase == DialogueType.Greetings)
		{
			if(_currentGreetingCount >= _nbGreetingsDialogue)
				_currentDialoguePhase = DialogueType.Conversation;
		}
		else if(_currentDialoguePhase == DialogueType.Conversation)
		{
			if(_currentConversationCount >= _nbConversationDialogue)
			{
				if(_incorrectConversationsCount >= _nbConversationsIncorrectToFail)
				{
					_currentDialoguePhase = DialogueType.ConversationRejectionHarsh;
				}
				else
				{
					_currentDialoguePhase = DialogueType.Question;
				}
			}
		}
		else if (_currentDialoguePhase == DialogueType.Question)
		{
			if(_currentQuestionCount >= _nbQuestionsDialogue)
			{
				if(_incorrectQuestionsCount >= _nbQuestionsIncorrectToFail)
				{
					_currentDialoguePhase = DialogueType.RejectionSoft;
				}
				else
				{
					_currentDialoguePhase = DialogueType.Acceptance;
				}
			}
		}
		else if (_currentDialoguePhase != DialogueType.Acceptance) // Make sure we aren't stuck
		{
			_currentDialoguePhase = DialogueType.RejectionSoft;
		}
	}

	bool IsDialoguePhaseFinal(DialogueType dialogueType)
	{
		return dialogueType == DialogueType.RejectionSoft ||
			   dialogueType == DialogueType.RejectionHarsh ||
			   dialogueType == DialogueType.Acceptance;
	}

	private void SetNextDialogueNode()
	{
		if(_currentNode)
		{
			// TODO if current node has a follow up node, use that instead, and early return

			if(IsDialoguePhaseFinal(_currentNode.DialogueType))
			{
				EndDialogue();
				return;
			}
		}

		if(_currentNodeIndex + 1 < _currentNodesOfDialoguePhase.Count)
		{
			_currentNodeIndex++;
			switch(_currentDialoguePhase)
			{
				case DialogueType.Greetings:
					_currentGreetingCount++;
					break;
				case DialogueType.Conversation:
					_currentConversationCount++;
					break;
				case DialogueType.Question:
					_currentQuestionCount++;
					break;
			}

			_currentNode = _currentNodesOfDialoguePhase[_currentNodeIndex];
			OnNodeChangedCallback();
			return;
		}

		UpdateDialoguePhase();

		List<DialogueNode> possibleNextDialogueNodes;

		JobType jobType = JobType.None;
		if(_jobPostingData != null)
			jobType = _jobPostingData.JobType;

		switch(_currentDialoguePhase)
		{
			case DialogueType.Greetings:
				_currentGreetingCount++;
				possibleNextDialogueNodes = DialoguesGenerator.Instance.GetDialogues(DialogueType.Greetings);
				break;
			case DialogueType.Conversation:
				_currentConversationCount++;
				possibleNextDialogueNodes = DialoguesGenerator.Instance.GetDialogues(DialogueType.Conversation, jobType);
				break;
			case DialogueType.Question:
				_currentQuestionCount++;
				possibleNextDialogueNodes = DialoguesGenerator.Instance.GetDialogues(DialogueType.Question, jobType);
				break;
			default:
				possibleNextDialogueNodes = DialoguesGenerator.Instance.GetDialogues(_currentDialoguePhase);
				break;
		}

		if(possibleNextDialogueNodes.Count == 0)
		{
			Debug.LogWarning("No next nodes founds for dialogue phase: " + _currentDialoguePhase);
			return;
		}

		int nbNodesInList = 1;
		if(_currentDialoguePhase == DialogueType.Conversation)
		{
			nbNodesInList = _nbConversationDialogue;
		}
		else if(_currentDialoguePhase == DialogueType.Question)
		{
			nbNodesInList = _nbQuestionsDialogue;
		}

		// Get Unique random indexes
		_currentNodesOfDialoguePhase = new List<DialogueNode>();
		_currentNodeIndex = 0;

		HashSet<int> randomIndexes = new HashSet<int>();
		for(int i = 0; i < 100; ++i) // Avoid infinite loops
		{
			if(randomIndexes.Count >= nbNodesInList)
			{
				break;
			}
			int randomIndex = Random.Range(0, possibleNextDialogueNodes.Count);
			if(!randomIndexes.Contains(randomIndex))
			{
				_currentNodesOfDialoguePhase.Add(possibleNextDialogueNodes[randomIndex]);
			}
			randomIndexes.Add(randomIndex);
		}

		_currentNode = _currentNodesOfDialoguePhase[0];
		OnNodeChangedCallback();
	}

	private bool IsDialogueNodeSkillUnlocked(DialogueNode node)
	{
		Player player =  FindAnyObjectByType<Player>();
		if(!player)
			return false;

		return player.UnlockedSkills.Contains(node.GetDialogueID());
	}

	private void OnNodeChangedCallback()
	{
		ClearButtons();

		List<DialogueChoice> displayChoices = new List<DialogueChoice>();

		// 1. Add fixed choices
		displayChoices.AddRange(_currentNode.FixedChoices);

		bool showCorrectAnswers = IsDialogueNodeSkillUnlocked(_currentNode);

		// 3. Instantiate buttons
		foreach (DialogueChoice choice in displayChoices)
		{
			DialogueResponseButton button = Instantiate(_responseButton, _responsesParent);
			_responseButtons.Add(button);
			button.Setup(this, choice, showCorrectAnswers);
			button.gameObject.SetActive(true);
		}

		_mainDialogue.text = _currentNode.DialogueText;
		OnNodeChanged?.Invoke(_currentNode);
	}

	private void ClearButtons()
	{
		foreach (DialogueResponseButton button in _responseButtons)
		{
			if (button != null)
			{
				Destroy(button.gameObject);
			}
		}

		_responseButtons.Clear();
	}
}
