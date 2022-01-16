using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateIcon : MonoBehaviour
{
    public float rotateSpeed = 30;
    public float acceleration = 10;
    private int count = 0;
    public float speed = 0.0005f;
    private int speedLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (count < 600) 
        {
        }
        else if (count < 650)
        {
            speedLevel = -13;
            rotateSpeed += acceleration;
        }
        else if (count < 700)
        {
            speedLevel = 1;
            rotateSpeed -= acceleration;
        }
        else
        {
            count = 0;
        }
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        count += 1;

        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed * speedLevel, transform.position.z);
    }
}
