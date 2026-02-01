using System;
using System.Collections.Generic;
using UnityEngine;

public enum ChoiceCorrectness
{
	Neutral,
	Correct,
	Incorrect,
	VeryIncorrect
}

[Serializable]
public class DialogueChoice
{
	public string ChoiceText;
	public DialogueNode OptionalNextNode;
	public ChoiceCorrectness Correctness = ChoiceCorrectness.Neutral;
	public string ChoiceTag;
}
