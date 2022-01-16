using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detect : MonoBehaviour {

    // Use this for initialization
    public bool isReady = false;
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Overall").GetComponent<MainUi>().Conversation.text = "Press X to have a chat";
        isReady = true;
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject.Find("Overall").GetComponent<MainUi>().Conversation.text = "Test not shown in actual";
        isReady = false;
    }
}
