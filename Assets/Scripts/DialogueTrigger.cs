using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

	public Dialogue dialogue;
	public List<PersonData> person;
	public int selected;
	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);	
	}
	public void Start()
	{
		StartCoroutine(StartTrigger());
	}
	public IEnumerator StartTrigger()
	{
		yield return new WaitForSeconds(2f);
		TriggerDialogue();
	}
}