using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("Escape"))//OVRInput.GetDown(OVRInput.RawButton.Start))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
		
	}
}
