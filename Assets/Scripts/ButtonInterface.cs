using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInterface : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button buttonLeft;
    public Button buttonRight;
    
    public Text info;
    
        public String B1_one;
        public String B1_two;
        public String B1_three;
        public String B1_four;
        
        public String B2_one;
        public String B2_two;
        public String B2_three;
        public String B2_four;
        
        public String B3_one;
        public String B3_two;
        public String B3_three;
        public String B3_four;
        
        public String infoText;

        public bool B1_show;
        public bool B2_show;
        public bool B3_show;
        public bool BLeft_show;
        public bool BRight_show;

        public List<Situation> situations;

        private int id = 0;
        
        public class Situation
        {
            public String B1, B2, B3, info;
            public bool B1_s, B2_s, B3_s, BLeft_s, BRight_s;

            public Situation(String B1, String B2, String B3, bool B1_s, bool B2_s, bool B3_s, bool BLeft_s, bool BRight_s)
            {
                this.B1 = B1;
                this.B2 = B1;
                this.B3 = B1;
                this.B1_s = B1_s;
                this.B2_s = B2_s;
                this.B3_s = B3_s;
                this.BLeft_s = BLeft_s;
                this.BRight_s = BRight_s;
            }
            
            public Situation(String B1, String B2, String B3, String info, bool B1_s, bool B2_s, bool B3_s, bool BLeft_s, bool BRight_s)
            {
                this.B1 = B1;
                this.B2 = B1;
                this.B3 = B1;
                this.B1_s = B1_s;
                this.B2_s = B2_s;
                this.B3_s = B3_s;
                this.BLeft_s = BLeft_s;
                this.BRight_s = BRight_s;

            
            }

        }
        
        
        // Start is called before the first frame update
    void Start()
    {
        situations.Add(new Situation(B1_one, B2_one, B3_one, B1_show, B2_show, B3_show, BLeft_show, BRight_show));
        situations.Add(new Situation(B1_two, B2_two, B3_two, B1_show, B2_show, B3_show, BLeft_show, BRight_show));
        situations.Add(new Situation(B1_three, B2_three, B3_three, B1_show, B2_show, B3_show, BLeft_show, BRight_show));
        situations.Add(new Situation(B1_four, B2_four, B3_four, B1_show, B2_show, B3_show, BLeft_show, BRight_show));
        
            button1.onClick.AddListener(GETLoadSituation);
            button2.onClick.AddListener(GETLoadSituation);
            button3.onClick.AddListener(GETLoadSituation);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(button1.GetComponentInChildren<Text>().text);

        if (Input.GetKeyDown("space"))
        {
            GETLoadSituation();
        }
    }

    public void GETLoadSituation()
    {
        Debug.Log("im hereeee");
        id++;
        button1.GetComponentInChildren<Text>().text = situations[id].B1;
        button2.GetComponentInChildren<Text>().text = situations[id].B2;
        button3.GetComponentInChildren<Text>().text = situations[id].B3;
    }
    
    public void buttonAction(String info, Button button)
    {
        button.gameObject.SetActive(false);
        infoText = info;
        this.info.gameObject.SetActive(true);
    }

    public void disappearButton(Button b1)
    {
        b1.gameObject.SetActive(false);
    }

    public void clearAll()
    {
        disappearButton(button1);
        disappearButton(button2);
        disappearButton(button3);
        disappearButton(buttonLeft);
        disappearButton(buttonRight);
        info.gameObject.SetActive(false);
    }
    
    //Todo: Dissapear certain textes.
    
    //Todo: Clear all
    
    //Todo: global buttons
}
