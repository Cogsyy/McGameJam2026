using System.Collections.Generic;
using UnityEngine;

public class DialoguesGenerator : MonoBehaviour
{
	public static DialoguesGenerator Instance { get; private set; }

	[SerializeField] private DialoguePoolData _dialoguePoolData;

	void Awake()
	{
		Instance = this;
	}

	public List<DialogueNode> GetDialogues(DialogueType dialogueType)
	{
		return _dialoguePoolData.DialoguePool.FindAll(node => IsDialogueFirstInChain(node) && node.DialogueType == dialogueType);
	}

	public List<DialogueNode> GetDialogues(DialogueType dialogueType, JobType jobType, bool allowGeneric = true)
	{
		return _dialoguePoolData.DialoguePool.FindAll(node => IsDialogueFirstInChain(node) 
		&& node.DialogueType == dialogueType 
		&& FilterDialogueByJob(node, jobType, allowGeneric));
	}

	bool IsDialogueFirstInChain(DialogueNode dialogueNode)
	{
		return !dialogueNode.IsDialogueFollowUp;
	}

	bool FilterDialogueByJob(DialogueNode dialogueNode, JobType jobType, bool allowGeneric)
	{
		return dialogueNode.JobType == jobType || (allowGeneric && dialogueNode.JobType == JobType.None);
	}
}
