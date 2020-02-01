using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonData", menuName = "Persons/Person Dialog")]
public class PersonData : ScriptableObject
{
    public string personName;

    [TextArea(3,10)]
    public string[] sentences;
    public SlotType type;
}
