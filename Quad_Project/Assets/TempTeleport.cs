using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTeleport : MonoBehaviour
{
    public Vector3 telePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("pressed Space\n");
            transform.position = telePos;
        }
    }
}
