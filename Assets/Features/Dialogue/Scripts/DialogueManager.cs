using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TMP_Text _mainDialogue;
	[SerializeField] private DialogueResponseButton _responseButton;
	[SerializeField] private Transform _responsesParent;

	public event Action<DialogueNode> OnDialogueStarted;
	public event Action<DialogueNode> OnNodeChanged;
	public event Action OnDialogueEnded;

	private DialogueNode _currentNode;
	private bool _isDialogueActive;
	private List<DialogueResponseButton> _responseButtons = new List<DialogueResponseButton>();

	public bool IsDialogueActive => _isDialogueActive;

	public void StartDialogue(DialogueNode startNode)
	{
		if (startNode == null)
		{
			Debug.LogWarning("Attempted to start dialogue with a null node.");
			return;
		}

		_currentNode = startNode;
		_isDialogueActive = true;
		OnDialogueStarted?.Invoke(_currentNode);
		OnNodeChangedCallback();
	}

	public void PickChoice(int index)
	{
		// Note: Using procedural generation makes index-based picking tricky.
		// Usually called via PickChoice(DialogueChoice) from the button.
	}

	public void PickChoice(DialogueChoice choice)
	{
		if (!_isDialogueActive || choice == null)
		{
			return;
		}

		DialogueNode nextNode = choice.GetNextNode();

		if (nextNode != null)
		{
			_currentNode = nextNode;
			OnNodeChangedCallback();
		}
		else
		{
			EndDialogue();
		}
	}

	public void PickChoice(DialogueNode nextNode)
	{
		if (!_isDialogueActive)
		{
			return;
		}

		if (nextNode != null)
		{
			_currentNode = nextNode;
			OnNodeChangedCallback();
		}
		else
		{
			EndDialogue();
		}
	}

	public void AdvanceDialogue()
	{
		if (!_isDialogueActive || _currentNode == null)
		{
			return;
		}

		// Only allow generic advancing if there are NO choices displayable
		// But since we can generate them from pools, we check the current node's definitions
		if (_currentNode.FixedChoices.Count > 0 || _currentNode.ChoicePools.Count > 0)
		{
			return;
		}

		DialogueNode nextNode = _currentNode.GetNextNode();

		if (nextNode != null)
		{
			_currentNode = nextNode;
			OnNodeChangedCallback();
		}
		else
		{
			EndDialogue();
		}
	}

	public void EndDialogue()
	{
		_isDialogueActive = false;
		_currentNode = null;
		ClearButtons();
		OnDialogueEnded?.Invoke();
	}

	private void OnNodeChangedCallback()
	{
		ClearButtons();

		List<DialogueChoice> displayChoices = new List<DialogueChoice>();

		// 1. Add fixed choices
		displayChoices.AddRange(_currentNode.FixedChoices);

		// 2. Add pooled choices
		if (_currentNode.ChoicePools.Count > 0)
		{
			List<DialogueChoice> combinedPool = new List<DialogueChoice>();
			foreach (DialogueChoicePool pool in _currentNode.ChoicePools)
			{
				if (pool != null)
				{
					combinedPool.AddRange(pool.Choices);
				}
			}

			if (_currentNode.RandomChoicesLimit > 0)
			{
				// Random subset
				for (int i = 0; i < combinedPool.Count; i++)
				{
					DialogueChoice temp = combinedPool[i];
					int randomIndex = UnityEngine.Random.Range(i, combinedPool.Count);
					combinedPool[i] = combinedPool[randomIndex];
					combinedPool[randomIndex] = temp;
				}

				int picks = Mathf.Min(_currentNode.RandomChoicesLimit, combinedPool.Count);
				for (int i = 0; i < picks; i++)
				{
					displayChoices.Add(combinedPool[i]);
				}
			}
			else
			{
				// Specific (All)
				displayChoices.AddRange(combinedPool);
			}
		}

		// 3. Instantiate buttons
		foreach (DialogueChoice choice in displayChoices)
		{
			DialogueResponseButton button = Instantiate(_responseButton, _responsesParent);
			button.gameObject.SetActive(true);
			_responseButtons.Add(button);
			button.Setup(this, choice);
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
