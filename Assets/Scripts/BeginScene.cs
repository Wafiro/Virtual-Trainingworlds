using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginScene : MonoBehaviour
{
   public string scene;
   
   public void changeScene(){
        SceneManager.LoadScene(scene);
   }
   
   public void quitGame(){
        Application.Quit();
   }
}
