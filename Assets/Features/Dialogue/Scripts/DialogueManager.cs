using System;
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

    List<DialogueResponseButton> _responseButtons = new List<DialogueResponseButton>();

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
		OnNodeChanged?.Invoke(_currentNode);
	}

	public void PickChoice(int index)
	{
		if (!_isDialogueActive || _currentNode == null)
		{
			return;
		}

		if (index < 0 || index >= _currentNode.Choices.Count)
		{
			Debug.LogWarning($"Choice index {index} out of range for node {_currentNode.name}.");
			return;
		}

		DialogueNode nextNode = _currentNode.Choices[index].NextNode;

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
		OnDialogueEnded?.Invoke();
	}

    private void OnNodeChangedCallback()
    {
        ClearButtons();
        foreach (var choice in _currentNode.Choices)
        {
            DialogueResponseButton button = Instantiate(_responseButton, _responsesParent);
            _responseButtons.Add(button);
            button.Setup(choice);
        }
        _mainDialogue.text = _currentNode.DialogueText;
        OnNodeChanged?.Invoke(_currentNode);
    }

    private void ClearButtons()
    {
        foreach (DialogueResponseButton button in _responseButtons)
        {
            Destroy(button.gameObject);
        }
        _responseButtons.Clear();
    }
}
