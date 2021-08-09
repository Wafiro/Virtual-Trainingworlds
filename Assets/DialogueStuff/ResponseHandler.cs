using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine.UI;
using UnityEngine;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI _dialogueUI;
    
    private List<GameObject> tempResponseButton = new List<GameObject>();

    private void Start()
    {
        _dialogueUI = GetComponent<DialogueUI>();
    }

    public void ShowResponses(Response[] responses)
    {
        //float responseBoxHeight = 0;

        foreach (Response response in responses)
        {
            Debug.Log("here");
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponentInChildren<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(delegate { OnPickedResponse(response); });
            
            tempResponseButton.Add(responseButton);

           //responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }
        
        //responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
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
       
        _dialogueUI.ShowDialogue(response.DialogueObject);
    }

    void Clicked()
    {
        Debug.Log("clicked me");
    }
}
