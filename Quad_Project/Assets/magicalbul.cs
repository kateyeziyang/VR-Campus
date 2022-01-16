using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class magicalbul : MonoBehaviour {

    public TextMeshProUGUI mytext;
    int phase;
    TextMeshProUGUI temp;
    void Start()
    {
        phase = 0;
        temp = mytext;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!GameObject.Find("Overall").GetComponent<MainUi>().menu)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.X))
            {
                phase++;
            }
            else if (OVRInput.GetDown(OVRInput.RawButton.Y))
            {
                phase--;
            }
            if (GameObject.Find("Overall").GetComponent<MainUi>().current_building.GetName() == "union")
            {
                if (phase == 0)
                {
                    mytext.text = "What's this place?";
                }
                else if (phase == 1)
                {
                    mytext.text = "This is union\n" + "a place to eat";
                }
                if (phase > 1 || phase < 0)
                {
                    phase = 0;
                }
            }else if (GameObject.Find("Overall").GetComponent<MainUi>().current_building.GetName() == "mainQuad")
            {
                if (phase == 0)
                {
                    mytext.text = "What's this place?";
                }
                else if (phase == 1)
                {
                    mytext.text = "This is Main Quad\n" + "a place to wander";
                }else if (phase == 2)
                {
                    mytext.text = "A lot of people here\n" +
                        "when festival and after class";
                }
                if (phase > 2 || phase < 0)
                {
                    phase = 0;
                }
            }
            else if (GameObject.Find("Overall").GetComponent<MainUi>().current_building.GetName() == "foelinger")
            {
                if (phase == 0)
                {
                    mytext.text = "What's this place?";
                }
                else if (phase == 1)
                {
                    mytext.text = "This is foelinger\n" + "a place to have ceremony";
                }
                else if (phase == 2)
                {
                    mytext.text = "This is the main hall of UIUC\n";
                }
                if (phase > 2 || phase < 0)
                {
                    phase = 0;
                }
            }
        }*/
    }
}
