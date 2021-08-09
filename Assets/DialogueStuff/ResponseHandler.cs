using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI _dialogueUI;

    private List<GameObject> tempResponseButton = new List<GameObject>();
    public GameObject c;
    public GameObject panel;
    public GameObject intro;
    private CameraTransition _cameraTransition;
    public bool KofferFertig = false;
    public bool TelefonFertig = false;
    public bool AutoFertig = false;
    public bool PersonFertig = false;
    public DialogueObject personEnd, kofferEnd, telefonEnd, AutoEnd;
   


    private void Start()
    {
        _dialogueUI = GetComponent<DialogueUI>();
        _cameraTransition = c.GetComponent<CameraTransition>();
    }

    public void ShowResponses(Response[] responses)
    {

        foreach (Response response in responses)
        {
            Debug.Log("here");
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponentInChildren<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(delegate { OnPickedResponse(response); });
            
            tempResponseButton.Add(responseButton);

          
        }
        
        responseBox.gameObject.SetActive(true);
        //buttonField.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        Debug.Log("clicked");
       responseBox.gameObject.SetActive(true);

       foreach (GameObject button in tempResponseButton)
       {
           Destroy(button);
       }
       tempResponseButton.Clear();

       if (response.id == 0)
       {
           _dialogueUI.ShowDialogue(response.DialogueObject);
       }
       else
       {
           switch (response.id)
           {
               case 10:
                   //Animation fürs Intro
                   Debug.Log("animation");
                   StartCoroutine(Intro(response.DialogueObject));
                   break;
               case 20:
                   //Szenenwechsel zum Kofferraum
                   StartCoroutine(Kofferraum(response.DialogueObject));
                   break;
               case 21:
                   //Animation fürs Warndreieck aufstellen
                   Debug.Log("animation");
                   StartCoroutine(Warndreieck(response.DialogueObject));
                   break;
               case 29:
                   _dialogueUI.ShowDialogue(response.DialogueObject);
                   KofferFertig = true;
                   break;
               case 30:
                   //Zum Auto
                   StartCoroutine(Auto(response.DialogueObject));
                   break;
               case 31:
                   //Person aus dem Wagen heben
                   _dialogueUI.ShowDialogue(response.DialogueObject);
                   _cameraTransition.bodyInCar.SetActive(false);
                   _cameraTransition.bodyOnFloor.SetActive(true);
                   break;
               case 39:
                   _dialogueUI.ShowDialogue(response.DialogueObject);
                   AutoFertig = true;
                   break;
               case 40:
                   //Zur Person auf dem Boden
                   StartCoroutine(Person(response.DialogueObject));
                   break;
               case 41:
                   //Stabile Seitenlage
                   break;
               case 49:
                   PersonFertig = true;
                   _dialogueUI.ShowDialogue(response.DialogueObject);
                   break;
               case 50:
                   //telefonieren
                   StartCoroutine(Telefon(response.DialogueObject));
                   break;
               case 59:
                   _dialogueUI.ShowDialogue(response.DialogueObject);
                   TelefonFertig = true;
                   break;

           }
       }
       
    }

    public IEnumerator Warndreieck(DialogueObject next)
    {
        panel.SetActive(false);
        _cameraTransition.SwitchState(4);
        yield return new WaitForSeconds(3);
        _cameraTransition.dreieck.SetActive(true);
        yield return new WaitForSeconds(1);
        _cameraTransition.SwitchState(3);
        yield return new WaitForSeconds(3);
        panel.SetActive(true);
        _dialogueUI.ShowDialogue(next);

    }

    public IEnumerator Intro(DialogueObject next)
    {
        panel.SetActive(false);
        intro.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSeconds(5);
        panel.SetActive(true);
        _dialogueUI.ShowDialogue(next);
    }
    
    public IEnumerator Kofferraum(DialogueObject next)
    {
        panel.SetActive(false);
        _cameraTransition.SwitchState(3);
        yield return new WaitForSeconds(1);
        panel.SetActive(true);
        if (KofferFertig)
        {
            _dialogueUI.ShowDialogue(kofferEnd);
        }
        else
        {
            _dialogueUI.ShowDialogue(next);
        }
       
    }

    public IEnumerator Telefon(DialogueObject next)
    {
        panel.SetActive(false);
        _cameraTransition.SwitchState(5);
        _cameraTransition.telefon.SetActive(true);
        yield return new WaitForSeconds(1);
        panel.SetActive(true);
        if (TelefonFertig)
        {
            _dialogueUI.ShowDialogue(telefonEnd);
        }
        else
        {
            _dialogueUI.ShowDialogue(next);
        }
    }

    public IEnumerator Auto(DialogueObject next)
    {
        panel.SetActive(false);
        _cameraTransition.SwitchState(1);
        yield return new WaitForSeconds(1);
        panel.SetActive(true);
        if (AutoFertig)
        {
            _dialogueUI.ShowDialogue(AutoEnd);
        }
        else
        {
            _dialogueUI.ShowDialogue(next);
        }
    }

    public IEnumerator Person(DialogueObject next)
    {
        panel.SetActive(false);
        _cameraTransition.SwitchState(2);
        yield return new WaitForSeconds(2);
        panel.SetActive(true);
        if (PersonFertig)
        {
            _dialogueUI.ShowDialogue(personEnd);
        }
        else
        {
            _dialogueUI.ShowDialogue(next);
        }
    }
    

    void Clicked()
    {
        Debug.Log("clicked me");
    }
}
