using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
   [SerializeField] private InputAction cont;
   [SerializeField] private TMP_Text textLabel;
   [SerializeField] private DialogueObject testDialogue;
   public bool next = false;

   private void OnEnable()
   {
      cont.Enable();
   }

   private void OnDisable()
   {
      cont.Disable();
   }

   private TypeWriter _typeWriter;
   private ResponseHandler _responseHandler;
   
   private void Start()
   {
      _typeWriter = GetComponent<TypeWriter>();
      _responseHandler = GetComponent<ResponseHandler>();
      
      ShowDialogue(testDialogue);
      cont.performed += _ => NextText();
      
   }

   private void NextText()
   {
      next = true;
   }

   public void ShowDialogue(DialogueObject dialogueObject)
   {
      StartCoroutine(StepThroughDialogue(dialogueObject));
   }

   private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
   {

      for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
      {
         string dialogue = dialogueObject.Dialogue[i];
         yield return _typeWriter.Run(dialogue, textLabel);

         if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
         
         yield return new WaitUntil(() => next);
         
      }

      if (dialogueObject.HasResponses)
      {
         _responseHandler.ShowResponses(dialogueObject.Responses);
      }
      else
      {
         //TODO: CloseDialogueBox();
      }

   }
   
}
