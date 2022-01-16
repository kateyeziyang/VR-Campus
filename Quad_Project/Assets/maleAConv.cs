using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class maleAConv : MonoBehaviour {
    // Use this for initialization
    string[,] male; // Contains the dialogue the NPC can say
    string[,] you; // Contains the dialogue the user can say
    int happiness = 1; // How npc feels about you: 0 = good; 1 = soso; 2 = bad
    int phase; // How far along the user is in the conversation
    public TextMeshProUGUI DiaTree; // Text in Unity to show the response options the user can pick
    public TextMeshProUGUI FeedBack; // Text in Unity to show instructions, and the response from the NPC
    public Animator animControl;
    Colle collider;

    bool canChoose = false; // Wheter or not the user is able to pick a response option
    public GameObject pad;

    // Man's responses
    string[] posMalePhase0;
    string[] posMalePhase1;
    string[] posMalePhase2;
    string[] neutralMalePhase0;
    string[] neutralMalePhase1;
    string[] neutralMalePhase2;
    string[] negMalePhase0;
    string[] negMalePhase1;
    string[] negMalePhase2;

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
        collider = GameObject.Find("Trigger3").GetComponent<Colle>();

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
            // Conversation branch - The man is positive, neutral, and negatve towards the user
            // Currently, no difference in conversation based on the persona for the man
            if (Input.GetKeyDown("space"))//OVRInput.GetDown(OVRInput.RawButton.X))
            {
                if (happiness < 2) {
                    animControl.SetTrigger("StartTalk");
                } else {
                    animControl.SetTrigger("Anger");
                }
                
                DiaTree.color = Color.white;
                if (phase == 2) // NPC's last response to the user
                {
                    DiaTree.text = "";
                    FeedBack.text = male[happiness, 2];
                    phase++;
                }
                else if (phase > 2) // Conversation is over
                {
                    FeedBack.text = "He doesn't want to talk to you.";
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
                        FeedBack.text = "Yo, brother! Do you have any cash?";
                    }
                    else
                    {
                        FeedBack.text = male[happiness, phase];
                    }
                    phase++;
                    canChoose = true;
                }
            }
            // User picked the first response option
            else if (Input.GetKeyDown(KeyCode.Alpha1) && canChoose ==true )//OVRInput.GetDown(OVRInput.RawButton.Y) && canChoose == true)
            {
                DiaTree.text = you[0, phase] + "\nPress Space to continue";
                DiaTree.color = Color.green;
                happiness = 0;
                FeedBack.text = male[happiness, phase];
                canChoose = false;
            }
            // User picked the second response option
            else if (Input.GetKeyDown(KeyCode.Alpha2) && canChoose ==true)//OVRInput.GetDown(OVRInput.RawButton.A) && canChoose == true)
            {
                DiaTree.text = you[1, phase] + "\nPress Space to continue";
                DiaTree.color = Color.white;
                happiness = 1;
                FeedBack.text = male[happiness, phase];
                canChoose = false;
            }
            // User picked the third response option
            else if (Input.GetKeyDown(KeyCode.Alpha3) && canChoose==true)//OVRInput.GetDown(OVRInput.RawButton.B) && canChoose == true)
            {
                DiaTree.text = you[2, phase] + "\nPress Space to continue";
                DiaTree.color = Color.red;
                happiness = 2;
                FeedBack.text = male[happiness, phase];
                canChoose = false;
            }
        }
    }

    // Sets up the dialogue for both the user and npc
    void SetUpConvo() {
        // Set up the general conversation path
        // MAN RESPONSES
        // Phase 0 responses for the Male
        posMalePhase0 = new string[] {
            "Far out!",
            "Cool-city.",
            "Tight!"
        };
        neutralMalePhase0 =  new string[] {
            "It's casual.",
            "All good."
        };
        negMalePhase0 = new string[] {
            "Wow, no need to be a bogart.", 
            "I didn't ask for your opinion, only for your money.", 
        };
        // Phase 1 responses for the Male
        posMalePhase1 = new string[] {
            "Nah, I get paid lots but I spend it all on cassettes.",
            "Sound as a pound!"
        };
        neutralMalePhase1 =  new string[] {
            "Here's the skinny, for a shaggin' wagon.",
            "Buy the van down by the Union."
        };
        negMalePhase1 = new string[] {
            "Kiss off bastard.", 
            "Stop dripping in my Kool-Aid.",
            "Sit on it!" 
        };
        // Phase 2 responses for the Male
        posMalePhase2 = new string[] {
            "Thanks, you're far out man.",
            "And you my brother, are funky." 
        };
        neutralMalePhase2 =  new string[] {
            "Peace out home fry.",
            "Peace, love, and granola.",
            "Check ya later."
        };
        negMalePhase2 = new string[] {
            "And you're a chicken.",
            "Dream on, I'm a catch."
        };

        // USER RESPONSES
        // Phase 0 response options for the user
        posYouPhase0 = new string[] {
            "Sure, here you go.", 
            "Yeah, here." 
        };
        neutralYouPhase0 = new string[] {
            "I don't.", 
            "No."
        };
        negYouPhase0 = new string[] {
            "No, stop asking for money.", 
            "No, go get a job."
        };
        // Phase 1 response options for the user
        posYouPhase1 = new string[] {
            "Are times rough for you right now?",
            "Are you having a hard time?"
        };
        neutralYouPhase1 = new string[] {
            "What are you going to buy?",
            "Why did you need the money?"
        };
        negYouPhase1 = new string[] {
            "Are you a candyass?",
            "Are you a laker?",
            "Are you a space cadet?"
        };
        // Phase 2 response options for the user
        posYouPhase2 = new string[] {
            "You know, you're bomb.",
            "You know, you're great."
        };
        neutralYouPhase2 = new string[] {
            "I gotta go.",
            "See you.",
            "Bye."
        };
        negYouPhase2 = new string[] {
            "You're a real bummer.",
            "You're cheesier."
        };

        // Randomize the dialogue for the man
        male = new string[3, 3];//0 positive 1 neutral 2 negative || 0,1,2 stands for order of conversation
        male[0, 0] = posMalePhase0[Random.Range(0,posMalePhase0.Length)];
        male[0, 1] = posMalePhase1[Random.Range(0,posMalePhase1.Length)];
        male[0, 2] = posMalePhase2[Random.Range(0,posMalePhase2.Length)];
        male[1, 0] = neutralMalePhase0[Random.Range(0,neutralMalePhase0.Length)];
        male[1, 1] = neutralMalePhase1[Random.Range(0,neutralMalePhase1.Length)];
        male[1, 2] = neutralMalePhase2[Random.Range(0,neutralMalePhase2.Length)];
        male[2, 0] = negMalePhase0[Random.Range(0,negMalePhase0.Length)];
        male[2, 1] = negMalePhase1[Random.Range(0,negMalePhase1.Length)];
        male[2, 2] = negMalePhase2[Random.Range(0,negMalePhase2.Length)];

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
