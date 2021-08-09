using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtons : MonoBehaviour
{
   public GameObject[] buttons;

   private void Start()
   {
      buttons[0].SetActive(false);
      buttons[1].SetActive(false);
      buttons[2].SetActive(false);
      buttons[3].SetActive(false);
      buttons[4].SetActive(false);
   }

   public void Change(bool w, bool k, bool u, bool p, bool t)
   {
      buttons[0].SetActive(w);
      buttons[1].SetActive(k);
      buttons[2].SetActive(u);
      buttons[3].SetActive(p);
      buttons[4].SetActive(t);
   }
}
