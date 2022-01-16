using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allFly : MonoBehaviour
{
    GameObject[] myBirds;
	GameObject controller;
	public float flyInterval = 20f;
    // Start is called before the first frame update
    void Start()
    {
		controller = GameObject.Find("_livingBirdsController");

		myBirds = GameObject.Find("_livingBirdsController").GetComponent<lb_BirdController>().myBirds;
        InvokeRepeating("allToFly", 1f, flyInterval);
    }

	void allToFly()
	{
		controller.GetComponent<lb_BirdController>().AllFlee();
	}

    void callBirdToFly()
    {
		GameObject bird = null;
		int randomBirdIndex = Mathf.FloorToInt(Random.Range(0, myBirds.Length));
		int loopCheck = 0;
		//find a random bird that is active
		while (bird == null)
		{
			if (myBirds[randomBirdIndex].activeSelf == true)
			{
				bird = myBirds[randomBirdIndex];
				myBirds[randomBirdIndex].SendMessage("Flee");
				return;
			}
			randomBirdIndex = randomBirdIndex + 1 >= myBirds.Length ? 0 : randomBirdIndex + 1;
			loopCheck++;
			if (loopCheck >= myBirds.Length)
			{
				//all myBirds are not active
				return;
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
