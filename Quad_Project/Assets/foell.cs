using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foell : MonoBehaviour {

    // Use this for initialization
    public GameObject foe_p;
    public GameObject beam;

    private void OnTriggerEnter(Collider other)
    {
        foe_p.SetActive(true);
        beam.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        foe_p.SetActive(false);
        beam.SetActive(true);
        var padC = beam.GetComponent<Renderer>();
        Color new_color = Color.green;
        new_color.a = 0.1f;
        padC.material.SetColor("_Color", new_color);
    }
}
