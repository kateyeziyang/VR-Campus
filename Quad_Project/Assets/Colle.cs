using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colle : MonoBehaviour {

    // Use this for initialization
    public GameObject panel;
	public Animator animator;
    // Use this for initialization
    public bool toOfficer = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            toOfficer = true;
            panel.SetActive(true);
            Debug.Log(CharacterSelection.personaNo);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            toOfficer = false;
            panel.SetActive(false);
			animator.SetTrigger("EndTalk"); // Return to idle
        }
    }
}
