using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class femaleConv : MonoBehaviour {


    // Use this for initialization
    string[,] female; // Contains the dialogue the NPC can say
    string[,] you; // Contains the dialogue the user can say
    int happiness = 1; // How npc feels about you: 0 = good; 1 = soso; 2 = bad
    int phase; // How far along the user is in the conversation
    public TextMeshProUGUI DiaTree; // Text in Unity to show the response options the user can pick
    public TextMeshProUGUI FeedBack; // Text in Unity to show instructions, and the response from the NPC
    public Animator animControl;
    Colli collider;

    bool canChoose = false; // Wheter or not the user is able to pick a response option
    public GameObject pad;

    // Woman's responses
    string[] posFemalePhase0;
    string[] posFemalePhase1;
    string[] posFemalePhase2;
    string[] neutralFemalePhase0;
    string[] neutralFemalePhase1;
    string[] neutralFemalePhase2;
    string[] negFemalePhase0;
    string[] negFemalePhase1;
    string[] negFemalePhase2;

    // Player's responses
    string[] posYouPhase0;
    string[] posYouPhase1;
    string[] posYouPhase2;
    string[] neutralYouPhase0;
    string[] neutralYouPhase1;
    string[] neutralYouPhase2;
    string[] negYouPhase0;
    string[] negYouPhase1;
    string[] negYouPhase2;

    void Start()
    {
        phase = -1;//step of conversation.
        collider = GameObject.Find("Trigger").GetComponent<Colli>();
        SetUpConvo();
    }

    // Update is called once per frame
    void Update()
    {
        // When the persona of the player changes, you need to set up the dialogue again
        if (Persona.personaChanged && Persona.num_npc_update != Persona.num_npc) {
            SetUpConvo();

            // Reset the phase to be at the beginning of the convo upon changing the persona
            phase = -1;
            canChoose = false;
            happiness = 1;
            DiaTree.text = "";
            FeedBack.text = "Press space to talk";
            animControl.SetTrigger("StartTalk");
            Persona.num_npc_update++;

            // Stop updating the NPC's convo if we have updated all of them
            if (Persona.num_npc_update == Persona.num_npc) {
                Persona.personaChanged = false;
            }
        }

        if (collider.toOfficer == true)//false for test, for actual running please change it to true
        {
            // Conversation branch - The woman is positive, neutral, and negatve towards the user
            // Currently, no difference in conversation based on the persona for the woman
            if (Input.GetKeyDown("space")) //(OVRInput.GetDown(OVRInput.RawButton.X))
            {
                if (happiness < 1) {
                        animControl.SetTrigger("StartTalk");
                } else {
                        animControl.SetTrigger("Anger");
                }
                
                DiaTree.color = Color.white;
                if (phase == 2) // NPC's last response to the user
                {
                    DiaTree.text = "";
                    FeedBack.text = female[happiness, 2];
                    phase++;
                }
                else if (phase > 2) // Conversation is over
                {
                    FeedBack.text = "She ignores your words.";
                    var padC = pad.GetComponent<Renderer>();
                    padC.material.SetColor("_Color", Color.yellow);
                }
                else // Talking to the NPC
                {
                    DiaTree.text = "1. " + you[0, phase + 1] + "\n" +
                                    "2. " + you[1, phase + 1] + "\n" +
                                    "3. " + you[2, phase + 1] + "\n";
                    if (phase == -1)
                    {
                        FeedBack.text = "Hey there stranger.";
                    }
                    else
                    {
                        FeedBack.text = female[happiness, phase];
                    }
                    phase++;
                    canChoose = true;
                }
            }
            // User picked the first response option
            else if (Input.GetKeyDown(KeyCode.Alpha1) && canChoose == true) //(OVRInput.GetDown(OVRInput.RawButton.Y) && canChoose == true)
            {
                DiaTree.text = you[0, phase] + "\nPress Space to continue";
                DiaTree.color = Color.green;
                happiness = 0;
                FeedBack.text = female[happiness, phase];
                canChoose = false;
            }
            // User picked the second response option
            else if (Input.GetKeyDown(KeyCode.Alpha2) && canChoose == true) //(OVRInput.GetDown(OVRInput.RawButton.A) && canChoose == true)
            {
                DiaTree.text = you[1, phase] + "\nPress Space to continue";
                DiaTree.color = Color.white;
                happiness = 1;
                FeedBack.text = female[happiness, phase];
                canChoose = false;
            }
            // User picked the third response option
            else if (Input.GetKeyDown(KeyCode.Alpha3) && canChoose == true) //(OVRInput.GetDown(OVRInput.RawButton.B) && canChoose == true)
            {
                DiaTree.text = you[2, phase] + "\nPress Space to continue";
                DiaTree.color = Color.red;
                happiness = 2;
                FeedBack.text = female[happiness, phase];
                canChoose = false;
            }
        }
    }

    // Sets up the dialogue for both the user and npc
    void SetUpConvo() {
        // Set up the general conversation path
        // Female RESPONSES
        // Phase 0 responses for the oFemale
        posFemalePhase0 = new string[] {
            "Aw, thank you! I like your shoes as well.",
            "Hah! And that's the lowdown."
        };
        neutralFemalePhase0 =  new string[] {
            "Sure is, perfect for chillaxin'.",
            "It's stellar really."
        };
        negFemalePhase0 = new string[] {
            "You wanna fight? Then fight these tears.", 
            "Ugh, you're such a grueler.", 
        };
        // Phase 1 responses for the Female
        posFemalePhase1 = new string[] {
            "Too bad I don't get the same pay as dudes.",
            "You're one of the few people to say that to me!"
        };
        neutralFemalePhase1 =  new string[] {
            "Gonna go out with my friends later.",
            "Not much, I don't have class right now."
        };
        negFemalePhase1 = new string[] {
            "I have a knife on me and I'm not  afraid to use it!", 
            "I'm warning you, you better leave now!",
            "Geez, take a chill pill!" 
        };
        // Phase 2 responses for the Female
        posFemalePhase2 = new string[] {
            "It is! Us women don't get the respect we deserve!",
            "That's the real deal! I'm not all show and no go." 
        };
        neutralFemalePhase2 =  new string[] {
            "Catch you on the flip side.",
            "Later days."
        };
        negFemalePhase2 = new string[] {
            "I'll call the man on you if you keep harassing me!",
            "Better book it before you start more beef with me.",
            "You're blitzed. Go home, crazy punk."
        };

        // USER RESPONSES
        // Phase 0 response options for the user
        posYouPhase0 = new string[] {
            "Hello! You have beautiful hair!", 
            "Hi, I love your outfit by the way." 
        };
        neutralYouPhase0 = new string[] {
            "Hi. It's a nice day out isn't it?", 
            "Hello. The weather is so nice today!"
        };
        negYouPhase0 = new string[] {
            "Bag it, old hag.", 
            "Hey, you look like you just blew some chunks."
        };
        // Phase 1 response options for the user
        posYouPhase1 = new string[] {
            "I bet you have a great gig!",
            "You must be successful!"
        };
        neutralYouPhase1 = new string[] {
            "So, what are you doing out here?",
            "What's crackin'?"
        };
        negYouPhase1 = new string[] {
            "I'll call you out.",
            "You can't pysche me."
        };
        // Phase 2 response options for the user
        posYouPhase2 = new string[] {
            "Must be tough for you, huh?",
            "Life's rough for all of us, ain't it?"
        };
        neutralYouPhase2 = new string[] {
            "I gotta go. Bye.",
            "I have something to go to, I'll see you later."
        };
        negYouPhase2 = new string[] {
            "You're rude and nasty.",
            "You're trash.",
            "What's your bag?",
            "I bet you're wearing foam domes."
        };

        // Randomize the dialogue for the woman
        female = new string[3, 3];//0 positive 1 neutral 2 negative || 0,1,2 stands for order of conversation
        female[0, 0] = posFemalePhase0[Random.Range(0,posFemalePhase0.Length)];
        female[0, 1] = posFemalePhase1[Random.Range(0,posFemalePhase1.Length)];
        female[0, 2] = posFemalePhase2[Random.Range(0,posFemalePhase2.Length)];
        female[1, 0] = neutralFemalePhase0[Random.Range(0,neutralFemalePhase0.Length)];
        female[1, 1] = neutralFemalePhase1[Random.Range(0,neutralFemalePhase1.Length)];
        female[1, 2] = neutralFemalePhase2[Random.Range(0,neutralFemalePhase2.Length)];
        female[2, 0] = negFemalePhase0[Random.Range(0,negFemalePhase0.Length)];
        female[2, 1] = negFemalePhase1[Random.Range(0,negFemalePhase1.Length)];
        female[2, 2] = negFemalePhase2[Random.Range(0,negFemalePhase2.Length)];

        // Randomize the dialogue for the user
        you = new string[3, 3];//0 positive 1 neutral 2 negative || 0,1,2 stands for order of conversation
        you[0, 0] = posYouPhase0[Random.Range(0,posYouPhase0.Length)];
        you[0, 1] = posYouPhase1[Random.Range(0,posYouPhase1.Length)];
        you[0, 2] = posYouPhase2[Random.Range(0,posYouPhase2.Length)];
        you[1, 0] = neutralYouPhase0[Random.Range(0,neutralYouPhase0.Length)];
        you[1, 1] = neutralYouPhase1[Random.Range(0,neutralYouPhase1.Length)];
        you[1, 2] = neutralYouPhase2[Random.Range(0,neutralYouPhase2.Length)];
        you[2, 0] = negYouPhase0[Random.Range(0,negYouPhase0.Length)];
        you[2, 1] = negYouPhase1[Random.Range(0,negYouPhase1.Length)];
        you[2, 2] = negYouPhase2[Random.Range(0,negYouPhase2.Length)];
    }
}
