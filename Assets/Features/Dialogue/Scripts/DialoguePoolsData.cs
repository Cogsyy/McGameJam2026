using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialoguePoolsData", menuName = "Dialogue/Pools Data")]
public class DialoguePoolsData : ScriptableObject
{
    [SerializeField] List<DialogueNode> _dialoguePools;
	public List<DialogueNode> DialoguePools => _dialoguePools;
}
