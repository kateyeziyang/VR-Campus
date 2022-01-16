using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    public float speed = 4f;
    bool PlayerInRoom = false;
    public Vector3 InRoomCoord = new Vector3(-1800f, 30f, 280f);
    public Vector3 OutRoomCoord = new Vector3(-630f, 10f, -20f);
    // Start is called before the first frame update
    void Start()
    {
       // Screen.lockCursor = true;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        /* controls for translation / rotation of character */
        float finalSpeed = speed;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)//yes there is movement
            animator.Play("Run");//animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
            finalSpeed = finalSpeed* 2.3f;

        controller.Move(move*finalSpeed);
    }

    void OnCollisionEnter(Collision col)
    {
    }

    /* Toggles btw PersonaChangeRoom and Quad
        controller temporarily disabled to change position smoothly */
    public void TeleportPlayer()
    {

        print("collision!");
        controller.enabled = false;

        if (PlayerInRoom)
            transform.position = OutRoomCoord;
        else
            transform.position = InRoomCoord;

        PlayerInRoom = !PlayerInRoom;
        controller.enabled = true;
    }
}
