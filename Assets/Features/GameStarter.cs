using UnityEngine;

public class GameStarter : MonoBehaviour
{
	[SerializeField] private DialogueManager _dialogueManager;
	[SerializeField] private DialogueNode _startNode;

	[Header("Audio")]
	[SerializeField] private AudioClip _mainTheme;

	public ClothingItem[] _clothingItems;


	private void Start()
	{
		if (_dialogueManager != null && _startNode != null)
		{
			_dialogueManager.StartDialogue(_startNode);
		}

		// Play music
		if (AudioManager.Instance != null && _mainTheme != null)
		{
			AudioManager.Instance.PlayMusic(_mainTheme);
		}

		JobPostingManager jobPostingManager = null;
		if(jobPostingManager = FindAnyObjectByType<JobPostingManager>())
		{
			int nbPostings = 3;
			jobPostingManager.InitializeJobPostings(nbPostings);
		}
	}
}
