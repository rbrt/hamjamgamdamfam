using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplayController : MonoBehaviour 
{

	static CharacterDisplayController instance;

	public static CharacterDisplayController Instance
	{
		get
		{
			return instance;
		}
	}

	[SerializeField] protected GameObject airBadgerParent;
	[SerializeField] protected GameObject chiefBadgerParent;
	[SerializeField] protected Animator dialogueAnimator;
	[SerializeField] protected Text dialogueText;

	string showDialogueBool = "show";

	bool displaying = false;

	string[] positiveDialogue = new string[]
	{
		"Great work!\tI always win!",
		"Nice shot!\tRoger that!",
		"Don't get cocky!\t.....",
		"Another great job!\tOf course!"
	};
	
	string[] negativeDialogue = new string[]
	{
		"Be careful!\tThey're all over me!",
		"Watch out!\tI can't shake 'em!",
		"Bring that ship back in one piece\tNo promises!",
		"I made a mistake sending you\t**sobbing**"
	};

	string[] deathDialogue = new string[]
	{
		"Badger???...Badger???\t.....",
		"Nooooo!!!!!\tAhhhhh!!!!"
	};

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PlayPositiveDialogue();
		}
	}

	public void PlayPositiveDialogue()
	{
		int index = (int)(Random.value * positiveDialogue.Length); 
		var airBadger = positiveDialogue[index].Split('\t')[1];
		var chiefBadger = positiveDialogue[index].Split('\t')[0];

		PlayDialogue(airBadger, chiefBadger);
	}

	public void PlayNegativeDialogue()
	{
		int index = (int)(Random.value * negativeDialogue.Length); 
		var airBadger = negativeDialogue[index].Split('\t')[1];
		var chiefBadger = negativeDialogue[index].Split('\t')[0];

		PlayDialogue(airBadger, chiefBadger);
	}

	public void PlayDeathialogue()
	{
		int index = (int)(Random.value * deathDialogue.Length); 
		var airBadger = deathDialogue[index].Split('\t')[1];
		var chiefBadger = deathDialogue[index].Split('\t')[0];

		PlayDialogue(airBadger, chiefBadger);
	}

	void PlayDialogue(string airBadgerLine, string chiefBadgerLine)
	{
		if (displaying)
		{
			return;
		}
		StartCoroutine(DialogueSequence(airBadgerLine, chiefBadgerLine));
	}

	IEnumerator DialogueSequence(string airBadgerLine, string chiefBadgerLine)
	{	displaying = true;

		// Show dialogue window
		dialogueAnimator.SetBool(showDialogueBool, true);
		yield return new WaitForSeconds(.25f);

		// Show chief badger
		chiefBadgerParent.SetActive(true);
		dialogueText.text = chiefBadgerLine;
		yield return new WaitForSeconds(2.5f);

		dialogueText.text = "";
		yield return new WaitForSeconds(.25f);

		// Show air badger
		airBadgerParent.SetActive(true);
		chiefBadgerParent.SetActive(false);
		dialogueText.text = airBadgerLine;
		yield return new WaitForSeconds(2.5f);

		// Hide dialogue window
		dialogueAnimator.SetBool(showDialogueBool, false);
		yield return new WaitForSeconds(.25f);

		airBadgerParent.SetActive(false);
		chiefBadgerParent.SetActive(false);

		dialogueText.text = "";

		displaying = false;
	}
	
}
