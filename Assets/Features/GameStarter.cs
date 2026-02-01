using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
	[SerializeField] private DialogueManager _dialogueManager;

	[Header("Audio")]
	[SerializeField] private AudioClip _mainTheme;

	[SerializeField] private ClothingItem[] _clothingItems;

	[SerializeField] private int _numberJobPostings = 3;
	[SerializeField] private int _skillprice = 100;
	private List<ConversationSkill> _conversationSkills = new List<ConversationSkill>();

	private static GameStarter _instance;


	public static GameStarter Instance => _instance;

	public ClothingItem[] ClothingItems => _clothingItems;
	public List<ConversationSkill> ConversationSkills => _conversationSkills;

	private void Awake()
	{
		_instance = this;
		PopulateSkills();
	}

	private void Start()
	{
		if (_dialogueManager != null)
		{
			_dialogueManager.OnDialogueEnded += OnDialogueEnded;
		}

		// Play music
		if (AudioManager.Instance != null && _mainTheme != null)
		{
			AudioManager.Instance.PlayMusic(_mainTheme);
		}
		
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.F6))
		{
			UnlockAllSkills();
		}
	}

	private void UnlockAllSkills()
	{
		foreach(var skill in ConversationSkills)
		{
			Player.Instance.UnlockSkill(skill);
		}
	}

	private void OnDialogueEnded()
	{
		Debug.Log("Dialogue ended. Go home.");
	}

	private string JobTypeToSkillName(JobType jobType)
	{
		return jobType switch
		{
			JobType.Programmer => "Programming",
			JobType.Designer => "Game Design",
			JobType.Musician => "Music",
			JobType.Artist => "Art",
			_ => "Hot Tip",
		};
	}

	private void PopulateSkills()
	{
		List<DialogueNode> allQuestions = FindAnyObjectByType<DialoguesGenerator>().GetDialogues(DialogueType.Question);

		foreach (var question in allQuestions)
		{
			ConversationSkill skill = new ConversationSkill();
			skill.Price = _skillprice;
			skill.ID = question.GetDialogueID(); // Using dialogue text as ID for simplicity
			skill.SkillName = JobTypeToSkillName(question.JobType);

			_conversationSkills.Add(skill);
		}
	}
}
