using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject _dialogueObject;
    public int id;

    public string ResponseText => responseText;

    public DialogueObject DialogueObject => _dialogueObject;
}
