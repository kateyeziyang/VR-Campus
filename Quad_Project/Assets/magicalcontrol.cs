using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class magicalcontrol : MonoBehaviour {

    // Use this for initialization
    public TextMeshProUGUI mytext;
    int phase;
    TextMeshProUGUI temp;
	void Start () {
        phase = 0;
        temp = mytext;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameObject.Find("Overall").GetComponent<MainUi>().outside)
        {
            if (Input.GetKeyDown("RightArrow"))//OVRInput.GetDown(OVRInput.RawButton.A))
            {
                phase++;
            }
            else if (Input.GetKeyDown("LeftArrow"))//(OVRInput.GetDown(OVRInput.RawButton.B))
            {
                phase--;
            }
            if (phase == 0)
            {
                mytext.text = "this paper can teach you how to use this simulator\n" +
                            "Press A for more info";
            }
            else if (phase == 1)
            {
                mytext.text = "At the top left you can see\n" +
                    "your current persona\n" +
                    "Press A for more info\n" +
                    "Press B for previou info";
            }
            if (phase > 1 || phase < 0)
            {
                phase = 0;
            }
        }
	}
}
