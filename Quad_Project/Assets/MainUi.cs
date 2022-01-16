using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUi : MonoBehaviour {

    // Use this for initialization


    public class Persona
     {
        private string name;
        private int age;
        private int gender;//0 for male 1 for female
        private int sexualOri;//0 for straight 1-3 for LG/B/T

         public Persona(string Name,int Age,int Gender,int SexualOri)
         {
            name = Name;
            age = Age;
            gender = Gender;
            sexualOri = SexualOri;
         }
         public string GetName()
         {
             return name;
         }
         public int GetAge()
         {
             return age;
         }
        public int GetGender()
        {
            return gender;
        }
        public int GetS()
        {
            return sexualOri;
        }
     }
    /*
     public class Building
     {
         private string name;
    */
    public bool outside = false;
    int phase = 0;
    Color[] color_list = new Color[] { Color.red, Color.yellow, Color.green };
    public TextMeshProUGUI Conversation;
    public TextMeshProUGUI DiaT;

    void Start () {
        Persona myPersona = new Persona("John", 21, 0, 0);
        Conversation.text = "Test/ Invisible in actual simulation/n";
        if (myPersona.GetName() == "John")
        {
            if (GameObject.Find("PoliceD").GetComponent<detect>().isReady == true)
            {
                if (Input.GetKeyDown("space"))//OVRInput.GetDown(OVRInput.RawButton.X))
                {
                    Conversation.text = "What can I help you?";
                    DiaT.text = "A. Today must be tough/n" +
                        "B. What happened over there/n" +
                        "C. Dont bother the protest!";
                    phase++;
                }
                if (phase == 1)
                {
                    if (Input.GetKeyDown("Alpha1"))//OVRInput.GetDown(OVRInput.RawButton.A))
                    {
                        Conversation.text = "Yeah, you dont say";
                        DiaT.text = "Today must be tough";
                        DiaT.color = Color.green;
                    }else if (Input.GetKeyDown("Alpha2"))//(OVRInput.GetDown(OVRInput.RawButton.B))
                    {
                        Conversation.text = "go ahead, everything is under control";
                        DiaT.text = "What happened over there";
                        DiaT.color = Color.gray;
                    }else if (Input.GetKeyDown("Alpha3"))//(OVRInput.GetDown(OVRInput.RawButton.Y))
                    {
                        Conversation.text = "!(physical assaulting)";
                        DiaT.text = "Dont bother the protest!";
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
       


    }
}
