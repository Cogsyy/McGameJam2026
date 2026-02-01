using UnityEngine;

public class GameStarter : MonoBehaviour
{
	[SerializeField] private DialogueManager _dialogueManager;

	[Header("Audio")]
	[SerializeField] private AudioClip _mainTheme;

	[SerializeField] private ClothingItem[] _clothingItems;

	private static GameStarter _instance;

	private void Awake()
	{
		_instance = this;
	}

	public static GameStarter Instance => _instance;

	public ClothingItem[] ClothingItems => _clothingItems;

	private void Start()
	{
		if (_dialogueManager != null)
		{
			//_dialogueManager.StartDialogue();
			_dialogueManager.OnDialogueEnded += () =>
			{
				Debug.Log("Dialogue ended. Go home.");
			};
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
