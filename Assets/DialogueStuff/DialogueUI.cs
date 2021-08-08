using System;
using System.Collections;
using TMPro;
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
   private void Start()
   {
      _typeWriter = GetComponent<TypeWriter>();
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
      foreach (string text in dialogueObject.Dialogue)
      {
         yield return _typeWriter.Run(text, textLabel);
         yield return new WaitUntil(() => next);
         next = false;
      }
   }
}
