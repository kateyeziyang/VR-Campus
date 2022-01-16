using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {


    private int selectedIndex;
    private bool change;
    public static int personaNo = 0; // Currently, 0 = Lucas (young straight black man), 1 = Susan (old bisexual white woman), 2 = Greg (young gay white man)
    public static bool inPersonaChangingRoom = false;
    

    // Use this for initialization.
    [Header("List of characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterInfo;
    [SerializeField] private Image characterSplash;
    [SerializeField] private Image backgroundColor;
    [Header("PANEL")]
    [SerializeField]
    private GameObject confirmPanel;
    void Start () {
        UpdateCharacterSelectionUI();
        confirmPanel.SetActive(false);
        Debug.Log("active?" + confirmPanel.activeSelf);

    }
    
    // Update is called once per frame
    void Update () {
        // Only change personas is the user is inside the room to prevent the player from being able to change it on the quad
        if (inPersonaChangingRoom) {
            // User selecting the persona they picked
            if (/*OVRInput.GetDown(OVRInput.RawButton.A)*/(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && confirmPanel.activeSelf == false)
            {
                confirmPanel.SetActive(true);
                Debug.Log("select persona " + selectedIndex);
            }
            // User deselecting the persona they picked
            else if(/*OVRInput.GetDown(OVRInput.RawButton.B)*/Input.GetKeyDown(KeyCode.Escape) && confirmPanel.activeSelf == true){
                confirmPanel.SetActive(false);
            }
            // User confirming the persona they picked
            else if(/*OVRInput.GetDown(OVRInput.RawButton.A)*/(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && confirmPanel.activeSelf == true){
                confirmPanel.SetActive(false);
                Debug.Log("confirm persona " + selectedIndex);

                if (personaNo != selectedIndex) {
                    Persona.personaChanged = true;
                    Persona.num_npc_update = 0;
                }

                personaNo = selectedIndex;
                // SceneManager.LoadScene(sceneName: "quad environ");
            }

            // Controls for the user to look at the various personas they can pick
            // Vector2 xy = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            if (/*xy[0] > 0.9f*/(Input.GetKeyDown(KeyCode.Greater) || Input.GetKeyDown(KeyCode.Period)) && change == false)
            {
                selectedIndex++;
                // change = true;
                if (selectedIndex == characterList.Count)
                    selectedIndex = 0;
            }
            if (/*xy[0] <= -0.9f*/(Input.GetKeyDown(KeyCode.Less) || Input.GetKeyDown(KeyCode.Comma)) && change == false)
            {
                selectedIndex--;
                // change = true;
                if (selectedIndex < 0)
                    selectedIndex = characterList.Count - 1;
            }
            // if(xy[0] == 0)
            // {
            //     change = false;
            // }
            UpdateCharacterSelectionUI();
        }
    }

    // Updates the UI screen
    private void UpdateCharacterSelectionUI()
    {
        characterSplash.sprite = characterList[selectedIndex].splash;
        var temp = characterList[selectedIndex].characterInfo; 
        characterInfo.text = temp.Replace("\\n", "\n");
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterInfo;
        public Color characterColor;
    }
}
