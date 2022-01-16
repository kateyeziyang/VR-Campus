using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persona : MonoBehaviour {
    /*
     * gen - Gender (Add more if needed)
     *     - 0 = Male
     *     - 1 = Female
     *     - 2 = Other
     * ori - Sexual Orientation (Add more if needed)
     *     - 0 = Heterosexual
     *     - 1 = Homosexual
     *     - 2 = Bisexual
     * eth - Ethnic (Add more if needed)
     *     - 0 = White
     *     - 1 = African American
     *     - 2 = Native American
     *     - 3 = Asian American
     */ 
    public int gen;
    public int ori;
    public int eth;

    private enum HandType
    {
        hand_0, hand_1, hand_2
        //Add more later
    }



    private HandType active_hands;
    private GameObject active_student_ID;

    public GameObject hand_left_0;
    public GameObject hand_right_0;
    public GameObject hand_left_1;
    public GameObject hand_right_1;
    public GameObject hand_left_2;
    public GameObject hand_right_2;
    // Add more hands later

    public GameObject student_ID_0;
    public GameObject student_ID_1;
    public GameObject student_ID_2;
    // Add more IDs later

    public GameObject Map;

    public bool checking_ID;
    public bool checking_Map;
    private bool pressed_lit;
    private bool pressed_lht;

    // Global variable used for checking if the dialogue needs to be updated based on the user's
    // persona
    public static bool personaChanged;

    // Number of NUC's/NPC's there are to talk to
    public static int num_npc = 3;

    // Number of NPC's dialogue that need to be updated
    public static int num_npc_update = 0;

    // Use this for initialization
    void Start () {
        gen = 0;
        ori = 0;
        eth = 0;
        Update_Active();
        checking_ID = false;
        checking_Map = false;
        pressed_lit = false;
        pressed_lht = false;

        if (CharacterSelection.personaNo == 0) {
            Persona_Change(0, 0, 1); // Lucas: Male, Straight, Black
        } else if (CharacterSelection.personaNo == 1) {
            Persona_Change(1, 2, 0); // Susan: Female, Bisexual, White
        } else if (CharacterSelection.personaNo == 2) {
            Persona_Change(0, 1, 0); // Greg: Male, Gay, White
        }
        personaChanged = false;
    }
    
    // Update is called once per frame
    void Update () {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.8 && !pressed_lit)
        {
            pressed_lit = true;
            checking_ID = !checking_ID;
          //  checking_Map = false;
            Set_Left_Hand(checking_ID);
            Set_Map(checking_Map);
            Set_ID(checking_ID);
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 0)
        {
            pressed_lit = false;
        }
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.8 && !pressed_lht)
        {
            pressed_lht = true;
            checking_Map = !checking_Map;
           // checking_ID = false;
            Set_Right_Hand(checking_Map);
            Set_ID(checking_ID);
            Set_Map(checking_Map);
        }
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) == 0)
        {
            pressed_lht = false;
        }
        if (OVRInput.GetDown(OVRInput.RawButton.Start))
        {
            SceneManager.LoadScene(sceneName: "Main hall");
        }

        
        /*if (OVRInput.GetDown(OVRInput.RawButton.X)) {
            Persona_Change(1, 1, 1);
            //Sample for persona change
        }
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            Persona_Change(0, 0, 0);
            //Sample for persona change
        }*/
    }

    void Update_Active()
    {
        //Add better alg logic later to fit the complex persona combinations
        if (gen == 0 && ori == 0 && eth == 1)
        {
            active_hands = HandType.hand_0;
            active_student_ID = student_ID_0;
        }
        else if (gen == 1 && ori == 2 && eth == 0)
        {
            active_hands = HandType.hand_1;
            active_student_ID = student_ID_1;
        }
        else
        {
            active_hands = HandType.hand_2;
            active_student_ID = student_ID_2;
        }
        Update_Model();
    }

    void Update_Model()
    {
        if (active_hands == HandType.hand_0)
        {
            hand_left_0.SetActive(true);
            hand_right_0.SetActive(true);
            hand_left_1.SetActive(false);
            hand_right_1.SetActive(false);
            hand_left_2.SetActive(false);
            hand_right_2.SetActive(false);
        }
        else if (active_hands == HandType.hand_1)
        {
            hand_left_0.SetActive(false);
            hand_right_0.SetActive(false);
            hand_left_1.SetActive(true);
            hand_right_1.SetActive(true);
            hand_left_2.SetActive(false);
            hand_right_2.SetActive(false);
        }
        else
        {
            hand_left_0.SetActive(false);
            hand_right_0.SetActive(false);
            hand_left_1.SetActive(false);
            hand_right_1.SetActive(false);
            hand_left_2.SetActive(true);
            hand_right_2.SetActive(true);
        }
        student_ID_0.SetActive(false);
        student_ID_1.SetActive(false);
        student_ID_2.SetActive(false);
        Map.SetActive(false);

    }
    //Use this function for persona change
    void Persona_Change(int new_gen, int new_ori, int new_eth)
    {
        Gen_Change(new_gen);
        Ori_Change(new_ori);
        Eth_Change(new_eth);
        Update_Active();
    }

    void Gen_Change(int n)
    {
        //If n = -1, does not change any value.
        if (n != -1)
        {
            gen = n;
            personaChanged = true;
            num_npc_update = 0;
        }
        //Add restriction according to final desicion of amount of genders.
    }
    void Ori_Change(int n)
    {
        //If n = -1, does not change any value.
        if (n != -1)
        {
            ori = n;
            personaChanged = true;
            num_npc_update = 0;
        }
        //Add restriction according to final desicion of amount of sexual orientations.
    }
    void Eth_Change(int n)
    {
        //If n = -1, does not change any value.
        if (n != -1)
        {
            eth = n;
            personaChanged = true;
            num_npc_update = 0;
        }
        //Add restriction according to final desicion of amount of ethnics.
    }

    //Use this function to read the current persona
    Vector3 Read_Persona()
    {
        //Read the curent persona, return a Vector3, where x = gen, y = ori, and z = eth
        Vector3 ret = new Vector3(gen, ori, eth); 
        return ret;
    }

    void Set_Left_Hand(bool n)
    {
        if (active_hands == HandType.hand_0)
        {
            hand_left_0.SetActive(!n);
        }
        if (active_hands == HandType.hand_1)
        {
            hand_left_1.SetActive(!n);
        }
        if (active_hands == HandType.hand_2)
        {
            hand_left_2.SetActive(!n);
        }
        //Add more later
    }
    void Set_Right_Hand(bool n)
    {
        if (active_hands == HandType.hand_0)
        {
            hand_right_0.SetActive(!n);
        }
        if (active_hands == HandType.hand_1)
        {
            hand_right_1.SetActive(!n);
        }
        if (active_hands == HandType.hand_2)
        {
            hand_right_2.SetActive(!n);
        }
        //Add more later
    }
    void Set_ID(bool n)
    {
        active_student_ID.SetActive(n);
    }

    void Set_Map(bool n)
    {
        Map.SetActive(n);
    }
}
