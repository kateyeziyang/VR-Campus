using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class instructions : MonoBehaviour {

    // Use this for initialization
    public TextMeshProUGUI inst;
    int page;
	void Start () {
        page = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (collision1.touched == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))//OVRInput.GetDown(OVRInput.RawButton.A))
            {
                page = (page + 1)%6;
            }
        }
        if (page == 0)
        {
            inst.text = 
                "Welcome to the 60's/70's\n" +
                "Main Quad Scene, Press Space for more details";//"Main Quad Scene, Press A for more details";
        }
        else if (page == 1)
        {
            // inst.text = "Press Space to next page\n" +
            //     "Right index trigger can show you minimap\n" +
            //     "Left hand trigger can show you your persona ID\n" +
            //     "Left index trigger can speed up";
            inst.text = "Press Space to next page\n" +//"Press A to next page\n" +
            			"Use WASD or the arrow keys\n" +
            			"to move around.";
        }
        else if (page == 2)
        {
           // inst.fontSize = 2f;
            inst.text = "Touch the purple beam of light to read\n" +
            			"about the building. Once read, the beam\n" +
                		"will become green.";
        }else if (page == 3)
        {
            inst.text =
                "Have some fun with NUCs!\n" +
                "Go near them until a screen pops up.\n" + 
                "Then, press space to start talking.";//press X to start talking\n" +
                //"Press A to next page";
        }else if (page == 5)
        {
            inst.text = "YOUR PERSONA MATTERS\n" +
                "You can always head back to change\n" +
                "your persona in the Union, just enter\n" +
                "the union via the aqua square";
        }else if (page == 4)
        {
            inst.text = //"Press Space to next page\n" +//"Press A to next page\n" +
                "Be careful:\n" +
                "What you have said always has a consequence.";
        }

    }
}
