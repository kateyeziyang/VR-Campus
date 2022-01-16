using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision1 : MonoBehaviour {

    // Use this for initialization
    public static bool touched = false;
    private void OnTriggerEnter(Collider other)
    {
        touched = true;
    }
    private void OnTriggerExit(Collider other)
    {
        touched = false;
    }
}
