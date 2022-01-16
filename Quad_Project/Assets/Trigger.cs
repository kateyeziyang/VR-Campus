using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("Overall").GetComponent<MainUi>().Conversation.text = "Press X to have a chat";
        }
    }
}
