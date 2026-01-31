using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueJobGenerator : MonoBehaviour
{
    [SerializeField] private DialoguePoolsData _dialoguePoolsData;


	public List<DialogueNode> GetDialogues(DialogueType dialogueType, JobType jobType)
	{
		return _dialoguePoolsData.DialoguePools.FindAll(node => node.DialogueType == dialogueType && node.JobType == jobType);
	}
}
