using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTeleport : MonoBehaviour
{
    public GameObject PlayerGameObj;
    //Vector3 InRoomCoord = new Vector3(-1800f, 30f, 280f);
    //Vector3 OutRoomCoord = new Vector3(-630f, 10f, -20f);
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //detects if player has made contact with the pressure plate (teleports btw UnionRoom ~ Quad)
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag=="Player")
        {
            PlayerGameObj.GetComponent<MovePlayer>().TeleportPlayer();

            // Change whether or not the user is in the persona changing room or not
            CharacterSelection.inPersonaChangingRoom = !CharacterSelection.inPersonaChangingRoom;
        }
        
    }

}
