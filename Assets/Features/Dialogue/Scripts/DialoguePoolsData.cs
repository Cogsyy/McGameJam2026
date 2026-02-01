using System.Collections.Generic;
using System.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialoguePoolsData", menuName = "Dialogue/Pools Data")]
public class DialoguePoolData : ScriptableObject
{
    [SerializeField] List<DialogueNode> _dialoguePool;
	public List<DialogueNode> DialoguePool => _dialoguePool;
}
