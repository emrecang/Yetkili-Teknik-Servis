using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
	public static Dialogue instance;
	public PersonData person;
	public string pname;
	public SlotType brokenType;
	private void Awake()
	{
		instance = this;
	}
	[TextArea(3, 10)]
	public string[] sentences;
	public void Start()
	{
		SetData(person);
	}

	public void SetData(PersonData pers)
	{
		sentences = new string[pers.sentences.Length];
		pname = pers.personName;
		for (int i = 0; i < pers.sentences.Length; i++)
		{
			sentences[i] = pers.sentences[i];
		}
		brokenType = person.type;
	}
}
