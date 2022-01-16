using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerCollider : MonoBehaviour {

    // Use this for initialization
    public GameObject panel;
	public Animator officerAnimator;
    // Use this for initialization
    public bool toOfficer = false;
	
	// Triggers talkability
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
			toOfficer = true;
			Debug.Log("Cop approached");
			panel.SetActive(true);
		}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
			toOfficer = false;
			panel.SetActive(false);
			Debug.Log("Cop left");
			officerAnimator.SetTrigger("EndTalk"); // Return to idle
		}
    }
}
