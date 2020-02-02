using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	public Dialogue dialogue;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;
	public Button textPanel;
	Coroutine cor;
	//public Animator animator;

	public Queue<string> sentences;
	public static DialogueManager instance;
	private void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
	}
	
	public void StartDialogue(Dialogue dialogue)
	{
		textPanel.gameObject.SetActive(true);
		nameText.text = dialogue.pname;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		Debug.Log(sentences.Count);
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		if(cor!=null)
		StopCoroutine(cor);
		cor = StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		UIManager.instance.CheckButtonState(true);
		textPanel.gameObject.SetActive(false);
		GameManager.instance.BackCustomerCor();
	}

    public void SkipDialogCor()
    {
        StartCoroutine(SkipDialog());
    }

    public IEnumerator SkipDialog()
    {
        while (sentences.Count > 0)
        {
            DisplayNextSentence();
            yield return new WaitForSeconds(0.01f);
            
        }
        EndDialogue();
    }
}