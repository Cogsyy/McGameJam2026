using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueChoice
{
	public string ChoiceText;
	public DialogueNode NextNode;
	
	[SerializeField] private List<DialogueNode> _randomNextNodes = new List<DialogueNode>();

	public DialogueChoice(string choiceText, DialogueNode nextNode)
	{
		ChoiceText = choiceText;
		NextNode = nextNode;
	}

	public DialogueNode GetNextNode()
	{
		if (_randomNextNodes != null && _randomNextNodes.Count > 0)
		{
			int randomIndex = UnityEngine.Random.Range(0, _randomNextNodes.Count);
			return _randomNextNodes[randomIndex];
		}
		
		return NextNode;
	}
}
