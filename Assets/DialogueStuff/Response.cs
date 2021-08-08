using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response : MonoBehaviour
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject _dialogueObject;

    public string ResponseText => responseText;

    public DialogueObject DialogueObject => _dialogueObject;
}
