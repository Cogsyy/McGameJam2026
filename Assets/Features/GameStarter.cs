using UnityEngine;

public class GameStarter : MonoBehaviour
{
	[SerializeField] private DialogueManager _dialogueManager;
	[SerializeField] private DialogueNode _startNode;

	[Header("Audio")]
	[SerializeField] private AudioClip _mainTheme;

	private void Start()
	{
		if (_dialogueManager != null && _startNode != null)
			_dialogueManager.StartDialogue(_startNode);

		// Play music
		AudioManager.Instance.PlayMusic(_mainTheme);
	}
}
