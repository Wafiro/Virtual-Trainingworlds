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
    public GameObject buttonField;
    private MoveButtons _moveButtons;
    private DialogueObject kofferBuffer;
    private DialogueObject bodyBuffer;

    private void Start()
    {
        _dialogueUI = GetComponent<DialogueUI>();
        _cameraTransition = c.GetComponent<CameraTransition>();
        _moveButtons = buttonField.GetComponent<MoveButtons>();
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
               case 30:
                   //Zum Auto
                   break;
               case 31:
                   //Person aus dem Wagen heben
                   break;
               case 40:
                   //Zur Person auf dem Boden
                   break;
               case 41:
                   //Stabile Seitenlage
                   break;
               case 50:
                   //telefonieren
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
        _dialogueUI.ShowDialogue(next);
    }

    void Clicked()
    {
        Debug.Log("clicked me");
    }
}
