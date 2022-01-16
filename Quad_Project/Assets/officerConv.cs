using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class officerConv : MonoBehaviour {

    // Use this for initialization
    // Use this for initialization
    string[,] officer; // Contains the dialogue the NPC can say
    string[,] you; // Contains the dialogue the user can say
    int happiness = 1; // How npc feels about you: 0 = good; 1 = soso; 2 = bad
    int phase; // How far along the user is in the conversation
    public TextMeshProUGUI DiaTree; // Text in Unity to show the response options the user can pick
    public TextMeshProUGUI FeedBack; // Text in Unity to show instructions, and the response from the NPC
    public Animator animControl;
    OfficerCollider collider;

    bool canChoose = false; // Wheter or not the user is able to pick a response option
    public GameObject pad;

    // Officer's responses
    string[] posOfficerPhase0;
    string[] posOfficerPhase1;
    string[] posOfficerPhase2;
    string[] neutralOfficerPhase0;
    string[] neutralOfficerPhase1;
    string[] neutralOfficerPhase2;
    string[] negOfficerPhase0;
    string[] negOfficerPhase1;
    string[] negOfficerPhase2;

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
        phase = -1; // step of conversation.
        
        collider = GameObject.Find("Trigger2").GetComponent<OfficerCollider>();

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
            // Conversation branch - Officer is positive/neutral towards the user
            if (CharacterSelection.personaNo != 0)
            {
                if (Input.GetKeyDown("space")) //(OVRInput.GetDown(OVRInput.RawButton.X))
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
                        FeedBack.text = officer[happiness, 2];
                        phase++;
                    }
                    else if (phase > 2) // Conversation is over
                    {
                        DiaTree.text = "";
                        FeedBack.text = "The officer seems to be too busy to talk.";
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
                            FeedBack.text = "Can I help you?";
                        }
                        else
                        {
                            FeedBack.text = officer[happiness, phase];
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
                    FeedBack.text = officer[happiness, phase];
                    canChoose = false;
                }
                // User picked the second response option
                else if (Input.GetKeyDown(KeyCode.Alpha2) && canChoose == true) //(OVRInput.GetDown(OVRInput.RawButton.A) && canChoose == true)
                {
                    DiaTree.text = you[1, phase] + "\nPress Space to continue";
                    DiaTree.color = Color.white;
                    happiness = 1;
                    FeedBack.text = officer[happiness, phase];
                    canChoose = false;
                }
                // User picked the third response option
                else if (Input.GetKeyDown(KeyCode.Alpha3) && canChoose == true) //(OVRInput.GetDown(OVRInput.RawButton.B) && canChoose == true)
                {
                    DiaTree.text = you[2, phase] + "\nPress Space to continue";
                    DiaTree.color = Color.red;
                    happiness = 2;
                    FeedBack.text = officer[happiness, phase];
                    canChoose = false;
                }
            }
            // Conversation branch - Officer is negative towards the user
            else
            {
                if (Input.GetKeyDown("space")) //(OVRInput.GetDown(OVRInput.RawButton.X))
                {
                    if (happiness < 1) {
                        animControl.SetTrigger("StartTalk");
                    } else {
                        animControl.SetTrigger("Anger");
                    }
                    
                    DiaTree.color = Color.red;
                    if (phase == 2) // NPC's last response to the user
                    {
                        DiaTree.text = "";
                        FeedBack.text = officer[2, 2];
                        phase++;
                    }
                    else if (phase > 2) // Conversation is over
                    {
                        string[] officerNegEndConvo = new string[] {
                            "Back OFF!",
                            "You better step aside or else.",
                            "Watch it, scoundrel."
                        };
                        DiaTree.text = "";
                        FeedBack.text = officerNegEndConvo[Random.Range(0,officerNegEndConvo.Length)];
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
                            FeedBack.text = "You better not mess around here.";
                        }
                        else
                        {
                            // The officer is meant to always respond negatively to the player, no matter what they say
                            FeedBack.text = officer[2, phase]; 
                        }
                        phase++;
                        canChoose = true;
                    }
                }
                // User picked the first response option
                else if (Input.GetKeyDown(KeyCode.Alpha1) && canChoose == true) //(OVRInput.GetDown(OVRInput.RawButton.Y) && canChoose == true)
                {
                    DiaTree.text = you[0, phase] + "\nPress Space to continue";
                    DiaTree.color = Color.red;
                    FeedBack.text = officer[2, phase];
                    canChoose = false;
                }
                // User picked the second response option
                else if (Input.GetKeyDown(KeyCode.Alpha2) && canChoose == true) //(OVRInput.GetDown(OVRInput.RawButton.A) && canChoose == true)
                {
                    DiaTree.text = you[1, phase] + "\nPress Space to continue";
                    DiaTree.color = Color.red;
                    FeedBack.text = officer[2, phase];
                    canChoose = false;
                }
                // User picked the third response option
                else if (Input.GetKeyDown(KeyCode.Alpha3) && canChoose == true) //(OVRInput.GetDown(OVRInput.RawButton.B) && canChoose == true)
                {
                    DiaTree.text = you[2, phase] + "\nPress Space to continue";
                    DiaTree.color = Color.red;
                    FeedBack.text = officer[2, phase];
                    canChoose = false;
                }
            }
        } 
    }

    // Sets up the dialogue for both the user and npc
    void SetUpConvo() {
        // Set up the positive/neutral conversation path
        // i.e. The character's persona isn't the black man, Lucas
        if (CharacterSelection.personaNo != 0) { 
            // OFFICER RESPONSES
            // Phase 0 responses for the oofficer
            posOfficerPhase0 = new string[] {
                "Thank you.",
                "That's good to hear.",
                "I'm glad."
            };
            neutralOfficerPhase0 =  new string[] {
                "Alright.",
                "Okay.",
                "Great.",
                "Hmm."
            };
            negOfficerPhase0 = new string[] {
                "So you’re a troublemaker, huh?", 
                "Haha, that’s a funny joke.", 
            };
            // Phase 1 responses for the officer
            posOfficerPhase1 = new string[] {
                "Well, I can trust good people like you.",
                "You're too kind."
            };
            neutralOfficerPhase1 =  new string[] {
                "Indeed.",
                "I saw."
            };
            negOfficerPhase1 = new string[] {
                "Well then.", 
                "How dare you.", 
            };
            // Phase 2 responses for the officer
            posOfficerPhase2 = new string[] {
                "Stay safe!",
                "Have a nice day!" 
            };
            neutralOfficerPhase2 =  new string[] {
                "Bye.",
                "Goodbye."
            };
            negOfficerPhase2 = new string[] {
                "See you in jail!",
                "I'll be arresting you soon!"
            };

            // USER RESPONSES
            // Phase 0 response options for the user
            posYouPhase0 = new string[] {
                "Yes, by doing what you’ve been doing.", 
                "I’m just happy you’re keeping campus safe." 
            };
            neutralYouPhase0 = new string[] {
                "Nope.", 
                "No, just wanted to say hello."
            };
            negYouPhase0 = new string[] {
                "Yeah by quitting your job.", 
                "Yes, if you stop being corrupt.", 
                "Yup, if you stop lying."
            };
            // Phase 1 response options for the user
            posYouPhase1 = new string[] {
                "You're a good cop.",
                "I'm just happy you're keeping campus safe.",
                "This campus wouldn't be safe without you."
            };
            neutralYouPhase1 = new string[] {
                "Oh, a bird just flew by.",
                "Look! There's a squirrel.",
                "The grass sure is green today.",
                "The sky is so blue."
            };
            negYouPhase1 = new string[] {
                "Actually, I really hate cops.",
                "Shut up ya laker.",
                "You look stupid."
            };
            // Phase 2 response options for the user
            posYouPhase2 = new string[] {
                "Thank you for your service sir.",
                "I enjoyed talking with you.",
                "Good bye officer."
            };
            neutralYouPhase2 = new string[] {
                "Bye.",
                "See ya.",
                "Later days."
            };
            negYouPhase2 = new string[] {
                "Bye power tripping pig.",
                "Adios you filthy rat."
            };
        // Set up the negative conversation path
        // i.e. The character's persona is the black man, Lucas
        } else {
            // OFFICER RESPONSES
            // Phase 0 responses for the officer
            posOfficerPhase0 = new string[] {
                "Good. Know your place.", 
                "Best if you stay out of jail." 
            };
            neutralOfficerPhase0 =  new string[] {
                "Hello.", 
                "Move along."
            };
            negOfficerPhase0 = new string[] {
                "I can arrest you right now.", 
                "Watch your tone.", 
                "You're a liar."
            };
            // Phase 1 responses for the officer
            posOfficerPhase1 = new string[] {
                "Hmm." 
            };
            neutralOfficerPhase1 =  new string[] {
                "Hmm."
            };
            negOfficerPhase1 = new string[] {
                "You're really getting on my nerves.", 
                "This is why I hate your kind.", 
            };

            // Phase 2 responses for the officer
            posOfficerPhase2 = new string[] {
                "..." 
            };
            neutralOfficerPhase2 =  new string[] {
                "..."
            };
            negOfficerPhase2 = new string[] {
                "You just ruined my day you @!#$%^$@", 
                "Get out of here!",
                "You better move along troublemaker.",
                "Take yourself elsewhere, scoundrel."
            };

            // USER RESPONSES
            // Phase 0 response options for the user
            posYouPhase0 = new string[] {
                "I won't. You're a noble officer.",
                "I would never good sir!"
            };
            neutralYouPhase0 = new string[] {
                "Sorry sir.",
                "I won't sir."
            };
            negYouPhase0 = new string[] {
                "You're just scared!",
                "How dare you threaten me!",
                "You're a racist pig."
            };
            // Phase 1 response options for the user
            posYouPhase1 = new string[] {
                "Thank you for your advice sir.",
                "You're a good cop sir."
            };
            neutralYouPhase1 = new string[] {
                "I did nothing wrong.",
                "Excuse me?"
            };
            negYouPhase1 = new string[] {
                "This is why no one trusts the pigs.",
                "You're corrupt!"
            };
            // Phase 2 response optioons for the user
            posYouPhase2 = new string[] {
                "I'm sorry sir, won't happen again.",
                "Sorry to hear that sir."
            };
            neutralYouPhase2 = new string[] {
                "Okay.",
                "..."
            };
            negYouPhase2 = new string[] {
                "You're the worst.",
                "You're evil to the core."
            };

        }

        // Randomize the dialogue for the officer
        officer = new string[3, 3];//0 positive 1 neutral 2 negative || 0,1,2 stands for order of conversation
        officer[0, 0] = posOfficerPhase0[Random.Range(0,posOfficerPhase0.Length)];
        officer[0, 1] = posOfficerPhase1[Random.Range(0,posOfficerPhase1.Length)];
        officer[0, 2] = posOfficerPhase2[Random.Range(0,posOfficerPhase2.Length)];
        officer[1, 0] = neutralOfficerPhase0[Random.Range(0,neutralOfficerPhase0.Length)];
        officer[1, 1] = neutralOfficerPhase1[Random.Range(0,neutralOfficerPhase1.Length)];
        officer[1, 2] = neutralOfficerPhase2[Random.Range(0,neutralOfficerPhase2.Length)];
        officer[2, 0] = negOfficerPhase0[Random.Range(0,negOfficerPhase0.Length)];
        officer[2, 1] = negOfficerPhase1[Random.Range(0,negOfficerPhase1.Length)];
        officer[2, 2] = negOfficerPhase2[Random.Range(0,negOfficerPhase2.Length)];

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
