using UnityEngine;

public class GameStarter : MonoBehaviour
{
	[SerializeField] private DialogueManager _dialogueManager;
	[SerializeField] private DialogueNode _startNode;

	private void Start()
	{
		_dialogueManager.StartDialogue(_startNode);
	}
}
